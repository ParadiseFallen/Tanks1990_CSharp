using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.IO.BindableIODevice.Key;
using Tanks1990.IO.BindableIODevice.Key.Serialization;

namespace Tanks1990.IO.KeyInterpretator
{
    class KeyInterpretator : IDisposable
    {
        public enum Mode{FILE,DEFAULT}
        static private KeyInterpretator Instance;
        static public KeyInterpretator GetInstance()
        {
            if (Instance == null) Instance = new KeyInterpretator();
            return Instance;
        }
        public void Dispose()
        {
            ((IDisposable)Instance).Dispose();
        }

        
        public Dictionary<string, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool>> KeyActivationFunctionsDictionary { get; set; }
        public Dictionary<string, Action> KeyActionsDictionary { get; set; }
        private List<LightKeyDataContainer> Samples;

        public KeyInterpretator()
        {
            KeyActivationFunctionsDictionary = new Dictionary<string, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool>>();
            KeyActionsDictionary = new Dictionary<string, Action>();
            Samples = new List<LightKeyDataContainer>();
        }
        
        public List<BindibleKey> LoadLayout() {
            List<BindibleKey> keys = new List<BindibleKey>();
            foreach (var item in Samples)
            {
                keys.Add(new BindibleKey(KeyActivationFunctionsDictionary[item.Triger], KeyActionsDictionary[item.ActionF], item.Description));
            }
            return keys;
        }
        public void LoadDeafultSamples() {
            Samples.Add(new LightKeyDataContainer() {Description = "Escape", ActionF = "RenderWindow.Close()", Triger = "Pressed_Escape" });
        }
        public bool LoadFromFileSamples(string Path)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
                //deserialize
                Samples = binaryFormatter.Deserialize(fs) as List<LightKeyDataContainer>;
                fs.Close();
                if (Samples.Count > 0)
                    return true;
                LoadDeafultSamples();
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SaveToFileSamples(string Path) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
            binaryFormatter.Serialize(fs, Samples);
        }
    }
}
