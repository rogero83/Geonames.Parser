# Geonames.Parser

Add the parser and your custom data processor to the dependency injection container in your application setup:

```csharp
builder.Services.AddScoped<IDataProcessor, MyCustomDataProcessor>();
builder.Services.AddScoped<IGeonamesParser, GeonamesParser>();
```

```csharp
public class MyCustomDataProcessor : IDataProcessor
{
}
```