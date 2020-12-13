# Handlebars.Extension.Json


#### [![CI](https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/workflows/CI/badge.svg)](https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/actions?query=workflow%3ACI) [![Nuget](https://img.shields.io/nuget/vpre/Handlebars.Net.Extension.Json)](https://www.nuget.org/packages/Handlebars.Net.Extension.Json/) [![performance](https://img.shields.io/badge/benchmark-statistics-blue)](http://handlebars-net.github.io/Handlebars.Net.Extension.Json/dev/bench/)

---

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=alert_status)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=security_rating)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json)

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=bugs)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json) [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=code_smells)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Handlebars-Net_Handlebars.Net.Extension.Json&metric=coverage)](https://sonarcloud.io/dashboard?id=Handlebars-Net_Handlebars.Net.Extension.Json) 

---
 
[![GitHub issues questions](https://img.shields.io/github/issues/handlebars-net/Handlebars.Net.Extension.Json/question)](https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/labels/question) 
[![GitHub issues help wanted](https://img.shields.io/github/issues/handlebars-net/Handlebars.Net.Extension.Json/help%20wanted?color=green&label=help%20wanted)](https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/labels/help%20wanted)

---

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

