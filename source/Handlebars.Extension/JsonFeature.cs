using HandlebarsDotNet.ObjectDescriptors;

namespace HandlebarsDotNet.Extension.Json
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonFeatureExtensions
    {
        /// <summary>
        /// Adds <see cref="IObjectDescriptorProvider"/>s required to support <c>System.Text.Json</c>. 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static HandlebarsConfiguration UseJson(this HandlebarsConfiguration configuration)
        {
            var providers = configuration.CompileTimeConfiguration.ObjectDescriptorProviders;
            
            providers.Add(new JsonDocumentObjectDescriptor());
            providers.Add(new JsonElementObjectDescriptor());
            
            configuration.AliasProviders.Add(new CountMemberAliasProvider());

            return configuration;
        }
    }
}