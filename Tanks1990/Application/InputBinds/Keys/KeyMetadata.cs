using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Input.BindableIODevice.Key.Serialization
{
    [Serializable]
    class KeyMetadata
    {
        public KeyMetadata(List<string> ActionF = null)
        {
            this.ActionF = ActionF;
            if (ActionF == null) ActionF = new List<string>();
        }
        public string Description { get; set; }
        public List<string> ActionF { get; set; }
        public string Triger { get; set; }
    }
}
