# CollectionJson.NET

This library provides support for using the [Collection+JSON] (http://amundsen.com/media-types/collection/) hypermedia mediatype authored by [Mike Amundsen] (http://twitter.com/mamund).

## Features

* A set of models for fully representing a Collection+Json document.
* Support for extensions
* CollectionJsonFormatter which handles Collection+Json representations.
* CollectionJsonController which is a drop-in API controller that is designed to make it easy to support Collection+Json. It wires up the formatter for you / removes a lot of cruft.
* A set of adapter contracts for reading and writing Collection+json documents.
* Easy to test / IoC friendly
* A light sample.
 
This documentation is a work in progress. You can check the samples directory for a basic CRUD controller and to see how to wire things up.

## Nuget packages

CollectionJson ships with several nuget packages that are factored for client and server scenarios.

* [CollectionJson] (https://www.nuget.org/packages/CollectionJson) - Object Model for working with CJ documents. Also contains helpers.
* [CollectionJson.Client] (https://www.nuget.org/packages/CollectionJson.Client) - Contains formatter for creating/consuming CJ documents with `HttpClient` or ASP.NET Web API.
* [CollectionJson.Server] (https://www.nuget.org/packages/CollectionJson.Server) - Contains controllers for implementing the CJ protocol in ASP.NET Web API.

## Returning a read document from a server
To create a new read document instantiate a `Collection` instance. The `CollectionJsonFormatter` will write this out to the CollectionJson format.

```csharp
var document = new ReadDocumnent();
var collection = new Collection { Version = "1.0", Href = new Uri(_requestUri, "/friends/") };
document.Collection = collection;

collection.Links.Add(new Link { Rel = "Feed", Href = new Uri(_requestUri, "/friends/rss") });

foreach (var friend in friends)
{
    var item = new Item { Href = new Uri(_requestUri, "/friends/" + friend.Id) };
    item.Data.Add(new Data { Name = "full-name", Value = friend.FullName, Prompt = "Full Name" });
    item.Data.Add(new Data { Name = "email", Value = friend.Email, Prompt = "Email" });
    item.Data.Add(new Data{ Name = "short-name", Value = friend.ShortName, Prompt = "Short Name"});
    item.Links.Add(new Link { Rel = "blog", Href = friend.Blog, Prompt = "Blog" });
    item.Links.Add(new Link { Rel = "avatar", Href = friend.Avatar, Prompt = "Avatar", Render = "Image" });
    collection.Items.Add(item);
}

var query = new Query { Rel = "search", Href = new Uri(_requestUri, "/friends"), Prompt = "Search" };
query.Data.Add(new Data { Name = "name", Prompt="Value to match against the Full Name" });
collection.Queries.Add(query);

var data = collection.Template.Data;
data.Add(new Data { Name = "name", Prompt = "Full Name" });
data.Add(new Data { Name = "email", Prompt = "Email" });
data.Add(new Data { Name = "blog", Prompt = "Blog" });
data.Add(new Data { Name = "avatar", Prompt = "Avatar" });
```

## Using extensions
The `Collection`, `Data`, `Item`, `Link` and `Query` classes are all extensible to allow for using CollectionJson extensions. There are three different methods for working with extensions. 

### Using `Extensions`
Extensions can be set by using the `Extensions` method which returns a dynamic object. Below is an example setting the Model extension*.
```csharp
var item = new Item { Href = new Uri(_requestUri, "/friends/" + friend.Id) };
item.Extensions().Model = "friend";
```

*Note: `Extensions` is a method rather than a property to avoid from being serialized, and to make it compatible with multiple serializers.

### Casting to Dynamic
Each of the aforementioned classes can be cast directly to `dynamic`.
```csharp
var item = new Item { Href = new Uri(_requestUri, "/friends/" + friend.Id) };
dynamic dItem = item;
dItem.Model="friend";
```


### Using SetValue
Extensions can be set by calling the SetValue method.
```csharp
var item = new Item { Href = new Uri(_requestUri, "/friends/" + friend.Id) };
item.SetValue("Model", "friend");
```

### Using GetValue
Extensions can be retrieved by calling the GetValue method
```csharp
var model = item.GetValue<string>("Model");
```

## API
### IReadDocument / ReadDocument
A CollectionJson server returns an object implementing IReadDocument. The `CollectionJsonFormatter` casts the model to  this interface to write out the payload. The concrete ReadDocument class implements this interface.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Collection  | Sets the Collection (`Collection`) 

### WriteDocument / IWriteDocument
A CollectionJson server receives an object implementing IWriteDocument. The `CollectionJsonFormatter' casts the model to this interface to read in the template. The concrete WriteDocument class implements this interface.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Template    | Contains the write template sent from the client

### Collection
The collection contains all the details for the CollectionJson document

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Version     | Sets the CollectionJson version 
Links       | Contains the top level links (`Link`) for the collection
Items       | Contains the collection of items (`Item`)
Queries     | Contains the collection of queries (`Query`)
Template    | Contains the write template (`Template`) to be retuned to the client

### Item
CollectionJson documents contain one or more items which are represented with the `Item` class.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Rel         | The link relation
Href        | Url for dreferencing the item
Rt          | Describes the item
Data        | Contains the data elements for the item.
Links       | Contains the links for the item

### Link
The Link object is used for embedding links with the document

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Rel         | The link relation
Href        | Url for dreferencing the link
Prompt      | Contains a human readable description for the link
Render      | How the link should be rendered, should be "image" or "link"

### Query
Queries are sent to the client which it can use to search against data.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Rel         | The link relation
Href        | Url for dreferencing the link
Prompt      | Contains a human readable description for the link
Rt          | Describes the return value of the query
Data        | Contains the data elements which the client will use to perform the query

### Template
Templates are sent to the client to instruct it as to which data elements it should send in order to create or update the collection.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Data        | Contains the data elements for the template


### CollectionJsonController_Of_T
This controller is a drop in component that one can derive from to implement the Collection+Json CRUD protocol. It constrains to strictly returning and accepting the correct message formats based on the spec. It also handles concerns like status codes, auto-generating the location header etc.

Member      | Description
----------- | -----------------------------------------------------------------------------------------------
Create      | Handles creation of a new item.
Read        | Handles reading a single or multiple items
Update      | Handles updating an existing itemm.
Delete      | Removes an existing item.

Below is a sample controller implementation

```csharp
public class FriendsController : CollectionJsonController<Friend>
{
    private IFriendRepository repo;

    public FriendsController(IFriendRepository repo, ICollectionJsonDocumentWriter<Friend> writer, ICollectionJsonDocumentReader<Friend> reader)
        :base(writer, reader)
    {
        this.repo = repo;
    }

    protected override int Create(IWriteDocument writeDocument, HttpResponseMessage response)
    {
        var friend = Reader.Read(writeDocument);
        return repo.Add(friend);
    }

    protected override IReadDocument Read(HttpResponseMessage response)
    {
        var readDoc = Writer.Write(repo.GetAll());
        return readDoc;
    }

    protected override IReadDocument Read(int id, HttpResponseMessage response)
    {
        return Writer.Write(repo.Get(id));
    }
    
    //custom search method   
    public HttpResponseMessage Get(string name)
    {
        var friends = repo.GetAll().Where(f => f.FullName.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1);
        var readDocument = Writer.Write(friends);
        return readDocument.ToHttpResponseMessage();
    }

    protected override IReadDocument Update(int id, IWriteDocument writeDocument, HttpResponseMessage response)
    {
        var friend = Reader.Read(writeDocument);
        friend.Id = id;
        repo.Update(friend);
        return Writer.Write(friend);
    }

    protected override void Delete(int id, HttpResponseMessage response)
    {
        repo.Remove(id);
    }
}
``` 

An implementer overrides methods from the base. The controller abstracts away the CJ format, which is handled via a set of adapters.

### ICollectionJsonDocumentReader

The reader is responsible for taking a Collection+JSON write template and convering it into "some" model. The model can be anything from a primitive like a string to a complex object.

```csharp
public class FriendDocumentReader : ICollectionJsonDocumentReader<Friend>
{
    public Friend Read(WriteDocument document)
    {
        var template = document.Template;
        var friend = new Friend();
        friend.FullName = template.Data.GetDataByName("name").Value;
        friend.Email = template.Data.GetDataByName("email").Value;
        friend.Blog = new Uri(template.Data.GetDataByName("blog").Value);
        friend.Avatar = new Uri(template.Data.GetDataByName("avatar").Value);
        return friend;
    }
}
```

### ICollectionJsonDocumentWriter

The writer is responsible for taking the model and writing it out as Collection+Json Document.

```csharp
public class FriendDocumentWriter : ICollectionJsonDocumentWriter<Friend>
{
    private readonly Uri _requestUri;

    public FriendDocumentWriter(HttpRequestMessage request)
    {
        _requestUri = request.RequestUri;
    }

    public IReadDocument Write(IEnumerable<Friend> friends)
    {
        //create the document from the friends collection
        var document = ...
        return document;
    }
}
```
