#Context Level Methods

The context class has some general methods. Those methods are:
 1. GetData
 2. GetScalar
 3. CommitChanges
 4. RollbackChanges


##GetData Method
The `GetData` method fetches data and returns row by row in the form of `CommandResult` class (Please see [The Command Result Class](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/command_result.md) for more clarification).

Example:
```
var ctx = new Context();

foreach (CommandResult row in ctx.GetData("Query_Or_StoredProcedureName", Command_Type, 
                                          new CommandParameter("Parameter_Name", Parameter_Value)))
{
                
}
```

Note: the `GetData` function uses [yield return](https://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx) to increase performance, So the above example is the best practice for it's use.


##GetScalar Method
The `GetScalar` method gets the first column from the first row and returns the value. This is a generic function which takes the type need to cast the value with before it's return.

Example:
```
string value = ctx.GetScalar<string>("Query_Or_ProcName", Command_Type, Parameters_If_Any);
```



##CommitChanges and RollbackChanges Methods
By default the all **Create Update and Delete (CRUD)** operations are executed with a transaction. The transaction & it's connection isn't closed until one of these methods is call whether `CommitChanges` to save the changes or `RollbackChanges` to cancel them.
