# CollectionJson

This library provides support for ASP.NET Web API using the [Collection+JSON] (http://amundsen.com/media-types/collection/) hypermedia mediatype authored by [Mike Amundsen] (http://twitter.com/mamund).

## Features

* A set of models for fully representing a Collection+Json document.
* CollectionJsonFormatter which handles Collection+Json representations.
* CollectionJsonController which is a drop-in API controller that is designed to make it easy to support Collection+Json. It wires up the formatter for you / removes a lot of cruft.
* A set of adapter contracts for reading and writing Collection+json documents.
* Easy to test / IoC friendly
* A light sample.
 
This documentation is a work in progress. You can check the samples directory for a basic CRUD controller and to see how to wire things up.

## Nuget packages

CollectionJson ships with several nuget packages that are factored for client and server scenarios.

* [WebApiContrib.CollectionJson] (https://www.nuget.org/packages/WebApiContrib.CollectionJson) - Object Model for working with CJ documents. Also contains helpers.
* [WebApiContrib.Formatting.CollectionJson.Client] (https://www.nuget.org/packages/WebApiContrib.Formatting.CollectionJson.Client) - Contains formatter for creating/consuming CJ documents with `HttpClient` or ASP.NET Web API.
* [WebApiContrib.Formatting.CollectionJson.Server] (https://www.nuget.org/packages/WebApiContrib.Formatting.CollectionJson.Server) - Contains controllers for implementing the CJ protocol in ASP.NET Web API.
* [WebApiContrib.Formatting.CollectionJson] (https://www.nuget.org/packages.WebApiContrib.CollectionJson) - Meta package for backward compatability, pulls in WebApiContrib.Formatting.CollectioJson.Server.

## IReadDocument and Collection
This interfaces corresponds to the message format Collection+Json defines for a returning Collection+Json results.

```csharp
public interface IReadDocument
{
    Collection Collection { get; }
}

public class Collection
{
    public Collection()
    {
        Links = new List<Link>();
        Items = new List<Item>();
        Queries = new List<Query>();
        Template = new Template();
    }

    public string Version { get; set; }
    public Uri Href { get; set; }
    public IList<Link> Links { get; private set; }
    public IList<Item> Items { get; private set; }
    public IList<Query> Queries { get; private set; }
    public Template Template { get; private set; }
}
```

## IWriteDocument and Template
This interface corresponds to the message format Collection+Json defines for creating / updating items.

```csharp
public interface IWriteDocument
{
    Template Template { get; }
}

public class Template
{
    public Template()
    {
        Data = new List<Data>();
    }

    public IList<Data> Data { get; set; }
}
```

## CollectionJsonController
This controller is a drop in component that one can dervice from to implement Collection+Json. It implements strictly returning and accepting the correct message formats based on the spec. It also handles concerns like status codes, auto-generating the location header etc.

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

# ICollectionJsonDocumentReader

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

# ICollectionJsonDocumentWriter

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
        var document = new ReadDocument();
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
        return document;
    }
}
```
