window.BENCHMARK_DATA = {
  "lastUpdate": 1607882947482,
  "repoUrl": "https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json",
  "entries": {
    "Benchmark.Net.Extension Benchmark": [
      {
        "commit": {
          "author": {
            "email": "zjklee@gmail.com",
            "name": "Oleh Formaniuk",
            "username": "zjklee"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "aa3af69c0fecdde0f09fca31ad9295e8406f8026",
          "message": "Merge pull request #1 from Handlebars-Net/feature/implementation\n\nActual Json extension implementation",
          "timestamp": "2020-12-01T15:18:50-08:00",
          "tree_id": "ea3cc7c2461bb4c76698163ac8f7b8212c201910",
          "url": "https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/commit/aa3af69c0fecdde0f09fca31ad9295e8406f8026"
        },
        "date": 1606864888121,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 2)",
            "value": 23725.280116154598,
            "unit": "ns",
            "range": "± 215.25936020690273"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 5)",
            "value": 177962.18061174665,
            "unit": "ns",
            "range": "± 1975.91715967917"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 10)",
            "value": 1050731.924609375,
            "unit": "ns",
            "range": "± 38195.64519327103"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.EndToEnd.Default(N: 5, DataType: \"json\")",
            "value": 238592.9444986979,
            "unit": "ns",
            "range": "± 3970.1233971379856"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "zjklee@gmail.com",
            "name": "Oleh Formaniuk",
            "username": "zjklee"
          },
          "committer": {
            "email": "zjklee@gmail.com",
            "name": "Oleh Formaniuk",
            "username": "zjklee"
          },
          "distinct": true,
          "id": "a2f3b7e36c8a460d58f7236e9f29eb48e75e8ab5",
          "message": "Update to Handlebars 2.0.3",
          "timestamp": "2020-12-13T20:06:13+02:00",
          "tree_id": "8533744a6411aed6de4a1b0f4061b8e2283d6621",
          "url": "https://github.com/Handlebars-Net/Handlebars.Net.Extension.Json/commit/a2f3b7e36c8a460d58f7236e9f29eb48e75e8ab5"
        },
        "date": 1607882946673,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 2)",
            "value": 25086.656773158484,
            "unit": "ns",
            "range": "± 20.067860825782052"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 5)",
            "value": 187221.8720327524,
            "unit": "ns",
            "range": "± 243.79791488640112"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.Json.SystemTextJson(N: 10)",
            "value": 1168575.481608073,
            "unit": "ns",
            "range": "± 1076.3365526946975"
          },
          {
            "name": "HandlebarsNet.Extension.Benchmark.EndToEnd.Default(N: 5, DataType: \"json\")",
            "value": 315997.07346754806,
            "unit": "ns",
            "range": "± 397.6919028629119"
          }
        ]
      }
    ]
  }
}