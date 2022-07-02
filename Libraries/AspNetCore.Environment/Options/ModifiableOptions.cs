using AspNetCore.Environment.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Environment.Options
{
    public class ModifiableOptions<TOptions> : IModifiableOptions<TOptions> where TOptions : class
    {
        private readonly IOptions<TOptions> _options;
        private readonly IHttpContextAccessor _httpContext;
        private const string PREFIX_SETTINGS_PARAM = "settings.";

        public ModifiableOptions(IOptions<TOptions> options, IHttpContextAccessor httpContext)
        {
            _options = options;
            _httpContext = httpContext;
        }

        public TOptions Value
        {
            get
            {
                if (!string.IsNullOrEmpty(OptionsName) &&
                    _httpContext.HttpContext != null && 
                    _httpContext.HttpContext.Request.Query.TryGetValue($"{PREFIX_SETTINGS_PARAM}{OptionsName}", out var settingsAsString) &&
                    !string.IsNullOrEmpty(settingsAsString))
                {
                    return JsonConvert.DeserializeObject<TOptions>(settingsAsString) ?? _options.Value;
                }
                return _options.Value;
            }
        }

        public string OptionsName
        {
            get
            {
                var result = typeof(TOptions).CustomAttributes.Where(a => a.AttributeType == typeof(RequestQueryKeyAttribute)).Select(x => x.ConstructorArguments.First()).First().Value;

                return result != null ? (string)result : string.Empty;
            }
        }
    }
}
