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

## CollectionJsonController
This controller is a drop in component that one can dervice from to implement Collection+Json. It implements strictly returning and accepting the correct message formats based on the spec. It also handles concerns like status codes, auto-generating the location header etc.

```csharp
public class FriendsController : CollectionJsonController<Friend>
{
    private IFriendRepository repo;

    public FriendsController(IFriendRepository repo, ICollectionJsonDocumentWriter<Friend> writer, ICollectionJsonDocumentReader<Friend> reader)
        :base(builder, transformer)
    {
        this.repo = repo;
    }

    protected override int Create(Friend friend, HttpResponseMessage response)
    {
        return repo.Add(friend);
    }

    protected override IEnumerable<Friend> Read(HttpResponseMessage response)
    {
        return repo.GetAll();
    }

    protected override Friend Read(int id, HttpResponseMessage response)
    {
        return repo.Get(id);
    }

    protected override Friend Update(int id, Friend friend, HttpResponseMessage response)
    {
        friend.Id = id;
        repo.Update(friend);
        return friend;
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
    public ReadDocument Write(IEnumerable<Friend> friends)
    {
        var document = new ReadDocument();
        var collection = new Collection { Version = "1.0", Href = new Uri("http://example.org/friends/") };
        document.Collection = collection;

        collection.Links.Add(new Link { Rel = "Feed", Href = new Uri("http://example.org/friends/rss") });

        foreach (var friend in friends)
        {
            var item = new Item { Href = new Uri("http://example.org/friends/" + friend.ShortName) };
            item.Data.Add(new Data { Name = "full-name", Value = friend.FullName, Prompt = "Full Name" });
            item.Data.Add(new Data { Name = "email", Value = friend.Email, Prompt = "Email" });
            item.Links.Add(new Link { Rel = "blog", Href = friend.Blog, Prompt = "Blog" });
            item.Links.Add(new Link { Rel = "avatar", Href = friend.Avatar, Prompt = "Avatar", Render = "Image" });
            collection.Items.Add(item);
        }

        var query = new Query { Rel = "search", Href = new Uri("http://example.org/friends/search"), Prompt = "Search" };
        query.Data.Add(new Data { Name = "name" });
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
