using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using HandlebarsDotNet.Collections;
using HandlebarsDotNet.Compiler;
using HandlebarsDotNet.Compiler.Structure.Path;
using HandlebarsDotNet.Iterators;
using HandlebarsDotNet.Runtime;
using HandlebarsDotNet.ValueProviders;

namespace HandlebarsDotNet.Extension.Json
{
    public class JsonElementIterator : IIterator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Iterate(
            in EncodedTextWriter writer, 
            BindingContext context, 
            ChainSegment[] blockParamsVariables, 
            object input,
            TemplateDelegate template, 
            TemplateDelegate ifEmpty
        )
        {
            Iterate(writer, context, blockParamsVariables, (JsonElement) input, template, ifEmpty);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Iterate(
            in EncodedTextWriter writer, 
            BindingContext context, 
            ChainSegment[] blockParamsVariables, 
            JsonElement input,
            TemplateDelegate template, 
            TemplateDelegate ifEmpty
        )
        {
            switch (input.ValueKind)
            {
                case JsonValueKind.Object:
                    IterateObject(writer, context, blockParamsVariables, input, template, ifEmpty);
                    break;
                case JsonValueKind.Array:
                    IterateArray(writer, context, blockParamsVariables, input, template, ifEmpty);
                    break;
                
                default:
                    Throw.ArgumentOutOfRangeException();
                    break;
            }
        }

        private static void IterateObject(
            in EncodedTextWriter writer, 
            BindingContext context, 
            ChainSegment[] blockParamsVariables, 
            JsonElement target,
            TemplateDelegate template, 
            TemplateDelegate ifEmpty
        )
        {
            using var innerContext = context.CreateFrame();
            var iterator = new ObjectIteratorValues(innerContext);
            var blockParamsValues = new BlockParamsValues(innerContext, blockParamsVariables);
            
            blockParamsValues.CreateProperty(0, out var _0);
            blockParamsValues.CreateProperty(1, out var _1);

            var enumerator = new ExtendedEnumerator<JsonProperty>(target.EnumerateObject());

            iterator.First = BoxedValues.True;
            iterator.Last = BoxedValues.False;

            int index = 0;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                
                var currentValue = current.Value;
                iterator.Key = currentValue.Name;
                
                if (index == 1) iterator.First = BoxedValues.False;
                if (current.IsLast) iterator.Last = BoxedValues.True;
                
                iterator.Index = BoxedValues.Int(index);
                
                object resolvedValue = currentValue.Value;
                
                blockParamsValues[_0] = resolvedValue;
                blockParamsValues[_1] = currentValue.Name;
                
                iterator.Value = resolvedValue;
                innerContext.Value = resolvedValue;

                template(writer, innerContext);

                ++index;
            }
            
            if (index == 0)
            {
                innerContext.Value = context.Value;
                ifEmpty(writer, innerContext);
            }
        } 
        
        private static void IterateArray(
            in EncodedTextWriter writer, 
            BindingContext context, 
            ChainSegment[] blockParamsVariables, 
            JsonElement target,
            TemplateDelegate template, 
            TemplateDelegate ifEmpty
        )
        {
            using var innerContext = context.CreateFrame();
            var iterator = new IteratorValues(innerContext);
            var blockParamsValues = new BlockParamsValues(innerContext, blockParamsVariables);

            blockParamsValues.CreateProperty(0, out var _0);
            blockParamsValues.CreateProperty(1, out var _1);
            
            iterator.First = BoxedValues.True;
            iterator.Last = BoxedValues.False;

            var count = target.GetArrayLength();
            var enumerator = target.EnumerateArray();

            var index = 0;
            var lastIndex = count - 1;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                var objectIndex = BoxedValues.Int(index);

                if (index == 1) iterator.First = BoxedValues.False;
                if (index == lastIndex) iterator.Last = BoxedValues.True;

                iterator.Index = objectIndex;

                object resolvedValue = value;

                blockParamsValues[_0] = resolvedValue;
                blockParamsValues[_1] = objectIndex;

                iterator.Value = resolvedValue;
                innerContext.Value = resolvedValue;

                template(writer, innerContext);

                ++index;
            }

            if (index == 0)
            {
                innerContext.Value = context.Value;
                ifEmpty(writer, innerContext);
            }
        }
        
        private static class Throw
        {
            public static void ArgumentOutOfRangeException() => throw new ArgumentOutOfRangeException();
        }
    }
}