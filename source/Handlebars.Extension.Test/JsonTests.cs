using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using HandlebarsDotNet.Extension.Json;
using Xunit;

#if !noJsonSupport

#endif

namespace HandlebarsDotNet.Extension.Test
{
    public class JsonTests
    {
        public class EnvGenerator : IEnumerable<object[]>
        {
            private readonly List<IHandlebars> _data = new List<IHandlebars>
            {
                Handlebars.Create(new HandlebarsConfiguration().UseJson())
            };

            public IEnumerator<object[]> GetEnumerator() => _data.Select(o => new object[] { o }).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(EnvGenerator))]
		public void JsonTestIfTruthy(IHandlebars handlebars)
		{
			var model = JsonDocument.Parse("{\"truthy\":true}");

			var source = "{{#if truthy}}{{truthy}}{{/if}}";
            
			var template = handlebars.Compile(source);

			var output = template(model);

			Assert.Equal("True", output);
		}
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void JsonTestIfFalsy(IHandlebars handlebars)
        {
            var model = JsonDocument.Parse("{\"falsy\":false}");

            var source = "{{#if (not falsy)}}{{falsy}}{{/if}}";

            handlebars.RegisterHelper("not", (context, arguments) => !arguments.At<bool>(0));
            var template = handlebars.Compile(source);

            var output = template(model);

            Assert.Equal("False", output);
        }

        [Theory]
        [ClassData(typeof(EnvGenerator))]
		public void JsonTestIfFalsyMissingField(IHandlebars handlebars)
		{
			var model = JsonDocument.Parse("{\"myfield\":\"test1\"}");

			var source = "{{myfield}}{{#if mymissingfield}}{{mymissingfield}}{{/if}}";

			var template = handlebars.Compile(source);

			var output = template(model);

			Assert.Equal("test1", output);
		}

        [Theory]
        [ClassData(typeof(EnvGenerator))]
		public void JsonTestIfFalsyValue(IHandlebars handlebars)
		{
			var model = JsonDocument.Parse("{\"myfield\":\"test1\",\"falsy\":null}");

			var source = "{{myfield}}{{#if falsy}}{{falsy}}{{/if}}";

			var template = handlebars.Compile(source);

			var output = template(model);

			Assert.Equal("test1", output);
		}

        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ArrayIterator(IHandlebars handlebars)
        {
            var model = JsonDocument.Parse("[{\"Key\": \"Key1\", \"Value\": \"Val1\"},{\"Key\": \"Key2\", \"Value\": \"Val2\"}]");

            var source = "{{#each this}}{{Key}}{{Value}}{{/each}}";

            var template = handlebars.Compile(source);

            var output = template(model);

            Assert.Equal("Key1Val1Key2Val2", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ArrayIteratorProperties(IHandlebars handlebars)
        {
            var model = JsonDocument.Parse("[{\"Key\": \"Key1\", \"Value\": \"Val1\"},{\"Key\": \"Key2\", \"Value\": \"Val2\"}]");

            var source = "{{#each this}}{{@index}}-{{@first}}-{{@last}}-{{@value.Key}}-{{@value.Value}};{{/each}}";

            var template = handlebars.Compile(source);

            var output = template(model);

            Assert.Equal("0-True-False-Key1-Val1;1-False-True-Key2-Val2;", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ArrayCount(IHandlebars handlebars)
        {
	        var model = JsonDocument.Parse("[{\"Key\": \"Key1\", \"Value\": \"Val1\"},{\"Key\": \"Key2\", \"Value\": \"Val2\"}]");

	        var source = "{{this.Count}} = {{this.Length}}";

	        var template = handlebars.Compile(source);

	        var output = template(model);

	        Assert.Equal("2 = 2", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ObjectIterator(IHandlebars handlebars){
	        var model = JsonDocument.Parse("{\"Key1\": \"Val1\", \"Key2\": \"Val2\"}");

	        var source = "{{#each this}}{{@key}}{{@value}}{{/each}}";

	        var template = handlebars.Compile(source);

	        var output = template(model);

	        Assert.Equal("Key1Val1Key2Val2", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ObjectIteratorProperties(IHandlebars handlebars){
            var model = JsonDocument.Parse("{\"Key1\": \"Val1\", \"Key2\": \"Val2\"}");

            var source = "{{#each this}}{{@index}}-{{@first}}-{{@last}}-{{@key}}-{{@value}};{{/each}}";

            var template = handlebars.Compile(source);

            var output = template(model);

            Assert.Equal("0-True--Key1-Val1;1-False--Key2-Val2;", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void ObjectIteratorPropertiesWithLast(IHandlebars handlebars){
            var model = JsonDocument.Parse("{\"Key1\": \"Val1\", \"Key2\": \"Val2\"}");

            var source = "{{#each this}}{{@index}}-{{@first}}-{{@last}}-{{@key}}-{{@value}};{{/each}}";

            handlebars.Configuration.Compatibility.SupportLastInObjectIterations = true;
            var template = handlebars.Compile(source);

            var output = template(model);

            Assert.Equal("0-True-False-Key1-Val1;1-False-True-Key2-Val2;", output);
        }
        
        [Theory]
        [ClassData(typeof(EnvGenerator))]
        public void WithParentIndexJsonNet(IHandlebars handlebars)
        {
            var source = @"
                {{#each level1}}
                    id={{id}}
                    index=[{{@../../index}}:{{@../index}}:{{@index}}]
                    first=[{{@../../first}}:{{@../first}}:{{@first}}]
                    last=[{{@../../last}}:{{@../last}}:{{@last}}]
                    {{#each level2}}
                        id={{id}}
                        index=[{{@../../index}}:{{@../index}}:{{@index}}]
                        first=[{{@../../first}}:{{@../first}}:{{@first}}]
                        last=[{{@../../last}}:{{@../last}}:{{@last}}]
                        {{#each level3}}
                            id={{id}}
                            index=[{{@../../index}}:{{@../index}}:{{@index}}]
                            first=[{{@../../first}}:{{@../first}}:{{@first}}]
                            last=[{{@../../last}}:{{@../last}}:{{@last}}]
                        {{/each}}
                    {{/each}}    
                {{/each}}";
            
            var template = handlebars.Compile( source );
            var data = new
                {
                    level1 = new[]{
                        new {
                            id = "0",
                            level2 = new[]{
                                new {
                                    id = "0-0",
                                    level3 = new[]{
                                        new { id = "0-0-0" },
                                        new { id = "0-0-1" }
                                    }
                                },
                                new {
                                    id = "0-1",
                                    level3 = new[]{
                                        new { id = "0-1-0" },
                                        new { id = "0-1-1" }
                                    }
                                }
                            }
                        },
                        new {
                            id = "1",
                            level2 = new[]{
                                new {
                                    id = "1-0",
                                    level3 = new[]{
                                        new { id = "1-0-0" },
                                        new { id = "1-0-1" }
                                    }
                                },
                                new {
                                    id = "1-1",
                                    level3 = new[]{
                                        new { id = "1-1-0" },
                                        new { id = "1-1-1" }
                                    }
                                }
                            }
                        }
                    }
            };

            var json = JsonDocument.Parse(JsonSerializer.Serialize(data));
            
            var result = template(json);

            const string expected = @"
                            id=0
                            index=[::0]
                            first=[::True]
                            last=[::False]
                                id=0-0
                                index=[:0:0]
                                first=[:True:True]
                                last=[:False:False]
                                    id=0-0-0
                                    index=[0:0:0]
                                    first=[True:True:True]
                                    last=[False:False:False]
                                    id=0-0-1
                                    index=[0:0:1]
                                    first=[True:True:False]
                                    last=[False:False:True]
                                id=0-1
                                index=[:0:1]
                                first=[:True:False]
                                last=[:False:True]
                                    id=0-1-0
                                    index=[0:1:0]
                                    first=[True:False:True]
                                    last=[False:True:False]
                                    id=0-1-1
                                    index=[0:1:1]
                                    first=[True:False:False]
                                    last=[False:True:True]
                            id=1
                            index=[::1]
                            first=[::False]
                            last=[::True]
                                id=1-0
                                index=[:1:0]
                                first=[:False:True]
                                last=[:True:False]
                                    id=1-0-0
                                    index=[1:0:0]
                                    first=[False:True:True]
                                    last=[True:False:False]
                                    id=1-0-1
                                    index=[1:0:1]
                                    first=[False:True:False]
                                    last=[True:False:True]
                                id=1-1
                                index=[:1:1]
                                first=[:False:False]
                                last=[:True:True]
                                    id=1-1-0
                                    index=[1:1:0]
                                    first=[False:False:True]
                                    last=[True:True:False]
                                    id=1-1-1
                                    index=[1:1:1]
                                    first=[False:False:False]
                                    last=[True:True:True]";
            
            Func<string, string> makeFlat = text => text.Replace( " ", "" ).Replace( "\n", "" ).Replace( "\r", "" );

            Assert.Equal( makeFlat( expected ), makeFlat( result ) );
        }
    }
}

