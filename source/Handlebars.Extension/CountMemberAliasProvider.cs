using System;
using System.Text.Json;
using HandlebarsDotNet.Compiler.Structure.Path;

namespace HandlebarsDotNet.Extension.Json
{
    public class CountMemberAliasProvider : IMemberAliasProvider
    {
        public bool TryGetMemberByAlias(object instance, Type targetType, ChainSegment memberAlias, out object? value)
        {
            if (!EqualsIgnoreCase("count", memberAlias) && !EqualsIgnoreCase("length", memberAlias))
            {
                value = null;
                return false;
            }
            
            if (!(instance is JsonElement element && element.ValueKind == JsonValueKind.Array))
            {
                value = null;
                return false;
            }

            value = element.GetArrayLength();
            return true;

            static bool EqualsIgnoreCase(string a, ChainSegment b)
            {
                return string.Equals(a, b.TrimmedValue, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}