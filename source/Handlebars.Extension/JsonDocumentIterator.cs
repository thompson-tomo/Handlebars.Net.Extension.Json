using System.Text.Json;
using HandlebarsDotNet.Compiler;
using HandlebarsDotNet.Iterators;
using HandlebarsDotNet.PathStructure;

namespace HandlebarsDotNet.Extension.Json
{
    public class JsonDocumentIterator : IIterator
    {
        public void Iterate(
            in EncodedTextWriter writer, 
            BindingContext context, 
            ChainSegment[] blockParamsVariables, 
            object input,
            TemplateDelegate template, 
            TemplateDelegate ifEmpty
        )
        {
            var target = (JsonDocument) input;
            JsonElementIterator.Iterate(writer, context, blockParamsVariables, target.RootElement, template, ifEmpty);
        }
    }
}