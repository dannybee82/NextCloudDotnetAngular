using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Settings
{
    public interface IServiceSettings
    {
        string NextCloudServer { get; set; }

        string NextCloudBasePath { get; set; }

        string NextCloudStripPart { get; set; }
    }

}
