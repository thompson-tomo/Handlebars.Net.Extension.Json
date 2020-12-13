using System.IO;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using HandlebarsDotNet;
using HandlebarsDotNet.Extension.Json;

namespace HandlebarsNet.Extension.Benchmark
{
    public class Json
    {
        private string _json;
        private HandlebarsTemplate<TextWriter, object, object> _systemJson;

        [Params(2, 5, 10)]
        public int N { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            const string template = @"
                {{#each level1}}
                    id={{id}}
                    {{#each level2}}
                        id={{id}}
                        {{#each level3}}
                            id={{id}}
                        {{/each}}
                    {{/each}}    
                {{/each}}";

            _json = JsonSerializer.Serialize(new { level1 = new Utils(N).ObjectLevel1Generator()});

            {
                var handlebars = Handlebars.Create();
                handlebars.Configuration.UseJson();

                using var reader = new StringReader(template);
                _systemJson = handlebars.Compile(reader);
            }
        }
        
        [Benchmark]
        public void SystemTextJson()
        {
            var document = JsonDocument.Parse(_json);
            _systemJson(TextWriter.Null, document);
        }
    }
}