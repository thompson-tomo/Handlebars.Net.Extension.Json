Handlebars.Extension.Json [![Nuget](https://img.shields.io/nuget/vpre/Handlebars.Net.Extension.Json)](https://www.nuget.org/packages/Handlebars.Net.Extension.Json/)
--

## Purpose

Adds [`System.Text.Json.JsonDocument`](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsondocument) support to [Handlebars.Net](https://github.com/Handlebars-Net/Handlebars.Net).

### Install
```cmd
dotnet add package Handlebars.Net.Extension.Json
```

### Usage
```c#
var handlebars = Handlebars.Create();
handlebars.Configuration.UseJson();
```

### Example
```c#
[Fact]
public void JsonTestObjects()
{
    var model = JsonDocument.Parse("{\"Key1\": \"Val1\", \"Key2\": \"Val2\"}");

    var source = "{{#each this}}{{@key}}{{@value}}{{/each}}";

    var handlebars = Handlebars.Create();
    handlebars.Configuration.UseJson();

    var template = handlebars.Compile(source);

    var output = template(model);

    Assert.Equal("Key1Val1Key2Val2", output);
}
```

### History
- Inspired by [rexm/Handlebars.Net/issues/304](https://github.com/rexm/Handlebars.Net/issues/304)

