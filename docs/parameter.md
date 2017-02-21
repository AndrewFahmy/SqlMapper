
#The Parameter Class

The parameter class is used to pass parameters to and from the mapper. It's declaration is simple.


Example:

```
var parameter = new CommandParameter("Parameter_Name", Parameter_Value, Parameter_Direction);
```

##Constructor Parameters
 * Parameter Name:           string - Name of the parameter
 * Parameter Value:          object - It's value
 * Parameter Direction:      ParameterDirection - An enum representaion for the direction of the parameter like Input or
                                                  Output (the default value is Input)
