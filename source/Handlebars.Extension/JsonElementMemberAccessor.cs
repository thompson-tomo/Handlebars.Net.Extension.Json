using System.Linq;
using System.Text.Json;
using HandlebarsDotNet.MemberAccessors;
using HandlebarsDotNet.PathStructure;

namespace HandlebarsDotNet.Extension.Json
{
    internal class JsonElementMemberAccessor : IMemberAccessor
    {
        private readonly CountMemberAliasProvider _aliasProvider = new CountMemberAliasProvider();
        
        public bool TryGetValue(object instance, ChainSegment memberName, out object? value)
        {
            var element = (JsonElement) instance;
            
            if(element.ValueKind == JsonValueKind.Object && element.TryGetProperty(memberName.TrimmedValue, out var property))
            {
                value = Utils.ExtractProperty(property);
                return true;
            }

            if (element.ValueKind == JsonValueKind.Array && int.TryParse(memberName, out var index))
            {
                var indexedElement = element.EnumerateArray().Skip(index).FirstOrDefault();

                if (indexedElement.ValueKind == JsonValueKind.Undefined)
                {
                    value = null;
                    return false;
                }

                value = Utils.ExtractProperty(indexedElement);
                return true;
            }

            if (_aliasProvider.TryGetMemberByAlias(element, typeof(JsonElement), memberName, out value))
            {
                return true;
            }

            value = null;
            return false;
        }
    }
}