using System.Text.Json;
using HandlebarsDotNet.Compiler.Structure.Path;
using HandlebarsDotNet.MemberAccessors;

namespace HandlebarsDotNet.Extension.Json
{
    internal class JsonElementMemberAccessor : IMemberAccessor
    {
        private readonly CountMemberAliasProvider _aliasProvider = new CountMemberAliasProvider();
        
        public bool TryGetValue(object instance, ChainSegment memberName, out object? value)
        {
            var element = (JsonElement) instance;

            if(element.ValueKind == JsonValueKind.Object && Utils.TryGetValue(element, memberName, out value))
            {
                return true;
            }

            if (_aliasProvider.TryGetMemberByAlias(instance, typeof(JsonElement), memberName, out value))
            {
                return true;
            }

            value = null;
            return false;
        }
    }
}