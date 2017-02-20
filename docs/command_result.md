
#The Command Result Class

The `CommandResult` class is a holder used by the context to temporarily hold retrieved data of a **single row** before mapping also it's used as a return value from the context method `GetData`. This class contains 3 properties

1. Columns:        a `Dictionary<string, object>` which holds all retrieved columns and their respective values.
2. Parameters:     all parameters that were passed at execution (after updating the output parameters' values if any).
3. ResultSetIndex: it's a Zero-based index of the row's result set.
