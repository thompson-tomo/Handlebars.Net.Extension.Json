using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using HandlebarsDotNet.Compiler.Structure.Path;
using HandlebarsDotNet.Runtime;

namespace HandlebarsDotNet.Extension.Json
{
    internal static class Utils
    {
        public static IEnumerable GetEnumerator(JsonElement document)
        {
            return document.ValueKind switch
            {
                JsonValueKind.Object => EnumerateObject(),
                JsonValueKind.Array => EnumerateArray(),
                _ => throw new ArgumentOutOfRangeException()
            };

            IEnumerable<KeyValuePair<object, object?>> EnumerateObject()
            {
                foreach (var property in document.EnumerateObject())
                {
                    yield return new KeyValuePair<object, object?>(property.Name, ExtractProperty(property.Value));
                }
            }

            IEnumerable<object?> EnumerateArray()
            {
                foreach (var property in document.EnumerateArray())
                {
                    yield return ExtractProperty(property);
                }
            }
        }

        private static object? ExtractProperty(JsonElement property)
        {
            switch (property.ValueKind)
            {
                case JsonValueKind.Object:
                case JsonValueKind.Array:
                    return property;

                case JsonValueKind.String when property.TryGetDateTime(out var v):
                    return v;
                
                case JsonValueKind.String when property.TryGetGuid(out var v):
                    return v;
                
                case JsonValueKind.String when property.TryGetDateTimeOffset(out var v):
                    return v;
                
                case JsonValueKind.String:
                    return property.GetString();

                case JsonValueKind.Number when property.TryGetInt32(out var v):
                    return v;
                
                case JsonValueKind.Number when property.TryGetInt64(out var v):
                    return v;
                
                case JsonValueKind.Number when property.TryGetDecimal(out var v):
                    return v;
                
                case JsonValueKind.Number:
                    return property.GetDouble();

                case JsonValueKind.True:
                    return BoxedValues.True;

                case JsonValueKind.False:
                    return BoxedValues.False;

                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    return null;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool TryGetValue(JsonElement document, ChainSegment memberName, out object? value)
        {
            if (document.TryGetProperty(memberName.TrimmedValue, out var property))
            {
                value = ExtractProperty(property);
                return true;
            }

            value = null;
            return false;
        }
    }
}