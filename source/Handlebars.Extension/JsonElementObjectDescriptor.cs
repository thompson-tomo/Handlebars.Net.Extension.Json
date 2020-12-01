using System;
using System.Collections;
using System.Text.Json;
using HandlebarsDotNet.ObjectDescriptors;

namespace HandlebarsDotNet.Extension.Json
{
    internal class JsonElementObjectDescriptor : IObjectDescriptorProvider
    {
        private static readonly Type Type = typeof(JsonElement);
        
        private static readonly JsonElementMemberAccessor JsonElementMemberAccessor = new JsonElementMemberAccessor();
        
        private readonly ObjectDescriptor _descriptor = new ObjectDescriptor(
            Type, 
            JsonElementMemberAccessor, 
            (descriptor, o) => GetEnumerator(o), 
            self => new JsonElementIterator()
        );

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
            var document = (JsonElement) instance;
            return Utils.GetEnumerator(document);
        }
    }
}