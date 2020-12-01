window.BENCHMARK_DATA = {
  "lastUpdate": 1606864888929,
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
      }
    ]
  }
}