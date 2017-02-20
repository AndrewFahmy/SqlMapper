
#Creating a Context

A context is the main unit of the mapper it's job is to initialize the repositories specified inside it. You can create a context by inheriting from `ContextBase<ConnectionType>` class. The `ConnectionType` is any connection class that implements the abstract class `System.Data.Common.DbConnection`. In this example I'll use the `SqlConnection` class to connect to a SQL Server database

Example:
```
public class Context : ContextBase<SqlConnection>
{
    public Context(string connectionString) : base(connectionString)
    {
    }
}
```

Note: you can also hard code the connection string in the context class and remove the `connectionString` variable all together

Example:
```
public class Context : ContextBase<SqlConnection>
{
    public Context() : base("Your_Connection_String")
    {
    }
}
```
