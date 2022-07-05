using AspNetCore.Environment.Configuration;

namespace AspNetCore.Environment.Services
{
    public interface IConfigurationService
    {
        void AddSource(AdditionalConfigurationSource additionalConfigurationSource);
    }
}
