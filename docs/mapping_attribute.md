# Attributes
There are 2 types of attributes to be used

**Note**: As usual I'll use the `Users` model example that was created in [Repositories Documentation](https://github.com/AndrewFahmy/SqlMapper/blob/master/docs/creating_repositories.md).

## Primary Key Attribute
An attribute to specify which property in a model is the primary key to be used in grouping duplicated rows. You can only use the primary key attribute on only **one property** for each model.

Example:
```csharp
[PrimaryKey]
public int? Id { get; set; }
```

## Mapping Attribute
This attribute is used to provide the mapper with extra information about a property. Unlike the `PrimaryKey` attribute this one can be used on all properties of a model. The information that can be provided are:

### ColumnName Parameter
This parameter is used when the name of the property is different from the name of the column.

Example:
```csharp
[Mapping(ColumnName = "CurrentUserName")] //This is the column name in the database
public string UserName { get; set; }
```

### ExcludeFrom Parameter
By using this parameter you can exclude the property from **Create, Update or Delete (CRUD)**, **Select**, **All** or **None** (which is the default).

Examples:
```csharp
[Mapping(ExcludeFrom = ExcludeTypes.Crud)] //Excluding this property from Create, Update or Delete mapping operations
public string UserName { get; set; }
```

```csharp
[Mapping(ExcludeFrom = ExcludeTypes.Select)] //Excluding this property from Select mapping operations
public string UserName { get; set; }
```

```csharp
[Mapping(ExcludeFrom = ExcludeTypes.All)] //Excluding this property from All mapping operations
public string UserName { get; set; }
```

```csharp
[Mapping(ExcludeFrom = ExcludeTypes.None)] //Default value which doesn't exclude the property from any operation
public string UserName { get; set; }
```

### GroupBy Parameter
This paramter is to override the `PrimaryKey` attribute and group duplicated row using a different property.

Example:
```csharp
[Mapping(GroupBy = "FirstName")] //Groupping duplicate rows by the property you specify instead on primary key property
public string UserName { get; set; }
```

### IsOutput Parameter
This parameter is used to indicate that the property value is not from a column but from an output parameter.

Example:
```csharp
[Mapping(IsOutput = true)] //indicating that this property value will come from an output parameter
public string UserName { get; set; }
```

### ParameterName Parameter
This parameter is used to specify the parameter name if it's different from the property name.

Example:
```csharp
[Mapping(ParameterName = "@UN")] //in case the parameter name is different from the property name
public string UserName { get; set; }
```

### ResultSetIndex Parameter
This parameter is used to specify the property's value in a certain result set.

Example:
```csharp
[Mapping(ResultSetIndex = 1)] //to indicate that this property value is in a specific result set
public List<LoginHistory> LoginHistory { get; set; }
```
