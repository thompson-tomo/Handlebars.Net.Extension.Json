using System.Text.Json;
using HandlebarsDotNet.MemberAccessors;
using HandlebarsDotNet.PathStructure;

namespace HandlebarsDotNet.Extension.Json
{
    internal class JsonDocumentMemberAccessor : IMemberAccessor
    {
        private readonly JsonElementMemberAccessor _jsonElementMemberAccessor = new JsonElementMemberAccessor();
        
        public bool TryGetValue(object instance, ChainSegment memberName, out object? value)
        {
            var document = (JsonDocument) instance;
            return _jsonElementMemberAccessor.TryGetValue(document.RootElement, memberName, out value);
        }
    }
}