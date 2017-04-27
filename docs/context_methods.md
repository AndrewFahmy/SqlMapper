# Context Level Methods

The context class has some general methods. Those methods are:
 1. GetData
 2. GetScalar
 2. Execute


## GetData Method
The `GetData` method fetches data and returns row by row in the form of `CommandResult` class (Please see [The Command Result Class](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/command_result.md) for more clarification).

Example:
```csharp
var ctx = new Context();

foreach (CommandResult row in ctx.GetData("Query_Or_StoredProcedureName", Command_Type, 
                                          new CommandParameter("Parameter_Name", Parameter_Value)))
{
                
}
```

Note: the `GetData` function uses [yield return](https://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx) to increase performance, So the above example is the best practice for it's use.


## GetScalar Method
The `GetScalar` method gets the first column from the first row and returns the value. This is a generic function which takes the type need to cast the value with before it's return.

Example:
```csharp
string value = ctx.GetScalar<string>("Query_Or_ProcName", Command_Type, Parameters_If_Any);
```

## Execute Method
The `Execute` method just executes a query and returns true if one or more row were affected

Example:
```csharp
bool result = ctx.Execute("Query_Or_ProcName", Command_Type, Parameters_If_Any);
```
