# AspNetCoreDateAndTimeOnly.Json
Add DateOnly and TimeOnly support to AspNetCore and additional functionality for working with System.Text.Json

## Installing
You can also install via the .NET CLI with the following command:
```
dotnet add package AspNetCoreDateAndTimeOnly.Json
```
If you're using Visual Studio you can also install via the built in NuGet package manager.

## Parse Object to Json
```csharp
string stringjson = objectdata.ToJSON();
```

## Parse string Json to Object
```csharp
MyObject objectdata = stringjson.GetJSON<MyObject>();
```

## Clone Object
```csharp
MyObject newobjectdata = objectdata.Clone<MyObject>();
```

## .Net 6
In net 6 you have to add converters with the `AddDateAndTimeJsonConverters` extension in `AddJsonOptions` of the MvcBuilder.

```csharp
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.AddDateAndTimeJsonConverters();
        });
```
## .Net 7
You can use the `DynamicContractResolver` class to serialize or deserialize JSON conversions in a custom way. 
```csharp
var settings = new JsonSerializerOptions
        {
            TypeInfoResolver = new DynamicContractResolver(properties, ignoreProperties, ignoreNull),
        };
```
Where.
`properties` is an array with the properties of the object you want to serialize.
`ignoreProperties` es un array con las propiedades del objecto que quiere ignorar.
`ignoreNull` is a boolean value to indicate whether to ignore null properties (also ignores empty IEnumerable).
