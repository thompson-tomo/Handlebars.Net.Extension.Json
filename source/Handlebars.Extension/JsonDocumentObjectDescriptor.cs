using System;
using System.Collections;
using System.Text.Json;
using HandlebarsDotNet.ObjectDescriptors;

namespace HandlebarsDotNet.Extension.Json
{
    internal class JsonDocumentObjectDescriptor : IObjectDescriptorProvider
    {
        private static readonly Type Type = typeof(JsonDocument);
        
        private readonly ObjectDescriptor _descriptor = new ObjectDescriptor(
            Type, 
            JsonDocumentMemberAccessor, 
            (descriptor, instance) => GetEnumerator(instance), 
            self => new JsonDocumentIterator()
        );

        private static readonly JsonDocumentMemberAccessor JsonDocumentMemberAccessor = new JsonDocumentMemberAccessor();

        public bool TryGetDescriptor(Type type, out ObjectDescriptor value)
        {
            if (Type != type)
            {
                value = ObjectDescriptor.Empty;
                return false;
            }

            value = _descriptor;
            return true;
        }
        
        private static IEnumerable GetEnumerator(object instance)
        {
            var json = (JsonDocument) instance;
            return Utils.GetEnumerator(json.RootElement);
        }
    }
}