using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Environment.Configuration
{
    public class AdditionalConfigurationSource
    {
        public AdditionalConfigurationSource()
        {
            Type = string.Empty;
            Path = string.Empty;
            Optional = true;
            ReloadOnChange = true;
            ReloadDelay = 250;
        }
        public string Type { get; set; }

        public string Path { get; set; }

        public bool Optional { get; set; }

        public bool ReloadOnChange { get; set; }

        public int ReloadDelay { get; set; }

        public string PhysicalFileProviderRoot
        {
            get
            {
                if (Path.Contains('/'))
                {
                    if (string.IsNullOrEmpty(Path))
                        return string.Empty;
                    var index = Path.LastIndexOf('/');
                    return Path.Substring(0, index);
                }
                else
                {
                    return Directory.GetCurrentDirectory();
                }
            }
        }

        public string FilePath
        {
            get
            {
                if (Path.Contains('/'))
                {
                    if (string.IsNullOrEmpty(Path))
                        return string.Empty;
                    var index = Path.LastIndexOf('/') + 1;
                    return Path.Substring(index);
                }
                else
                {
                    return Path;
                }
            }
        }
    }
}
