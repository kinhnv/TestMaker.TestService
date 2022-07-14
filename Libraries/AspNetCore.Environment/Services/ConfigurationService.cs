using AspNetCore.Environment.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace AspNetCore.Environment.Services
{
    public class ConfigurationService : IConfigurationService
    {
        IConfigurationBuilder _configurationBuilder;

        public ConfigurationService(IConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder;
        }

        public void AddSource(AdditionalConfigurationSource additionalConfigurationSource)
        {
            if (additionalConfigurationSource.Type == "JsonFile")
            {
                try
                {
                    _configurationBuilder.Sources.Insert(GetIndexOfSource(additionalConfigurationSource), new JsonConfigurationSource
                    {
                        FileProvider = new PhysicalFileProvider(additionalConfigurationSource.PhysicalFileProviderRoot),
                        Path = additionalConfigurationSource.FilePath,
                        Optional = additionalConfigurationSource.Optional,
                        ReloadOnChange = additionalConfigurationSource.ReloadOnChange,
                        ReloadDelay = additionalConfigurationSource.ReloadDelay
                    });
                }
                catch (System.Exception)
                {
                    // log hear
                }
            }
        }

        private int GetIndexOfSource(AdditionalConfigurationSource additionalConfigurationSource)
        {
            int index = _configurationBuilder.Sources.Count - 1;
            if (additionalConfigurationSource.Type == "JsonFile")
            {
                var source = _configurationBuilder.Sources.LastOrDefault(x => x is JsonConfigurationSource);
                if (source != null)
                {
                    index = _configurationBuilder.Sources.IndexOf(source) + 1;
                }
            }
            return index;
        }
    }
}
