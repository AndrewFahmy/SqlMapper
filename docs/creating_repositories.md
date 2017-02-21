
#Adding Repositories

Usually a repository represent a table structure from the database. To create a repository first we need to create it's [Plain Old CLR Object (POCO)](https://en.wikipedia.org/wiki/Plain_old_CLR_object) class. In the example below I'll use a simple user structure to clarify.

Example:
```
public class User
{
    public int? Id { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
```

To declare this `User` class in our context class.

```
public class Context : ContextBase<SqlConnection>
{
    public Context(string connectionString) : base(connectionString)
    {
    }


    public IRepository<User> Users { get; set; }
}
```
Please view [Creating a Context](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/context.md) for further understanding.

To See all available repository methods to use please see [Repository Level Methods](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/repository_methods.md)
