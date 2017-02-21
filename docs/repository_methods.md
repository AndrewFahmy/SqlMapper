
#Repository Level Methods
Repositories have more specific methods than [Context Level Methods](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/context_methods.md). Those methods are:

1. Save
2. Delete
3. GetSingle
4. Search

**Note**: I'll use the `Users` repository we created [before](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/creating_repositories.md) in all future examples.

##Save Method
Used to save data into database whether **Insert** or **Update**. 

Example:
```
var ctx = new Context();

ctx.Users.Save("Query_Or_Proc", Command_Type, new User
{
    Id = 0,
    FirstName = "",
    LastName = "",
    UserName = ""
});
```

##Delete Method
This method is different from `Save` method since usually in delete statements you don't need to send all columns just the id. That's why this method doesn't require a model but takes an array of parameters.

Example:
```
ctx.Users.Delete("Query_Or_Proc", Command_Type,
                new CommandParameter("@Id", 15),
                new CommandParameter("@Unique", ""));
```


**Note**: Both `Save` and `Delete` methods return **boolean** value indicating whether the action was successful or not.

##GetSingle Method
This method returns only the first row from the first result set after mapping it to the provided model.

Example:
```
var data = ctx.Users.GetSingle("Query_Or_ProcName", Command_Type,
                new CommandParameter("Parameter_Name", Parameter_Value));
```

##Search Method
This method is the same as `GetSingle` method but returns multiple rows after mapping them to a list of the provided model.

Example:
```
var data = ctx.Users.Search("Query_Or_ProcName", Command_Type,
                new CommandParameter("Parameter_Name", Parameter_Value));
```
