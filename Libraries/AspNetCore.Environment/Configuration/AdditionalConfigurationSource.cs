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
            FullPath = string.Empty;
            Optional = true;
            ReloadOnChange = true;
            ReloadDelay = 250;
        }
        public string Type { get; set; }

        public string FullPath { get; set; }

        public bool Optional { get; set; }

        public bool ReloadOnChange { get; set; }

        public int ReloadDelay { get; set; }

        public string PhysicalFileProviderRoot
        {
            get
            {
                if (string.IsNullOrEmpty(FullPath))
                    return string.Empty;
                var index = FullPath.LastIndexOf('/');
                return FullPath.Substring(0, index);
            }
        }

        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(FullPath))
                    return string.Empty;
                var index = FullPath.LastIndexOf('/') + 1;
                return FullPath.Substring(index);
            }
        }
    }
}
