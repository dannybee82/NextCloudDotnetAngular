using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Settings
{
    
    public class ServiceSettings : IServiceSettings
    {
        public string NextCloudServer { get; set; } = string.Empty;

        public string NextCloudBasePath {  get; set; } = string.Empty;

        public string NextCloudStripPart { get; set; } = string.Empty;
    }

}