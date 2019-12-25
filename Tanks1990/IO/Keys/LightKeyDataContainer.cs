using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.IO.BindableIODevice.Key.Serialization
{
    [Serializable]
    class LightKeyDataContainer
    {
        public string Description { get; set; }
        public string ActionF { get; set; }
        public string Triger { get; set; }
    }
}
