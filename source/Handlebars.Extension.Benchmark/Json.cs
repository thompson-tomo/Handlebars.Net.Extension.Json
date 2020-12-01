using System.Collections.Generic;
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

            _json = JsonSerializer.Serialize(new { level1 = ObjectLevel1Generator()});

            {
                var handlebars = Handlebars.Create();
                handlebars.Configuration.UseJson();

                using var reader = new StringReader(template);
                _systemJson = handlebars.Compile(reader);
            }

            List<object> ObjectLevel1Generator()
            {
                var level = new List<object>();
                for (int i = 0; i < N; i++)
                {
                    level.Add(new
                    {
                        id = $"{i}",
                        level2 = ObjectLevel2Generator(i)
                    });
                }

                return level;
            }
            
            List<object> ObjectLevel2Generator(int id1)
            {
                var level = new List<object>();
                for (int i = 0; i < N; i++)
                {
                    level.Add(new
                    {
                        id = $"{id1}-{i}",
                        level3 = ObjectLevel3Generator(id1, i)
                    });
                }

                return level;
            }
            
            List<object> ObjectLevel3Generator(int id1, int id2)
            {
                var level = new List<object>();
                for (int i = 0; i < N; i++)
                {
                    level.Add(new
                    {
                        id = $"{id1}-{id2}-{i}"
                    });
                }

                return level;
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