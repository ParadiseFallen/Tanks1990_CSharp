using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Input.BindableIODevice.Key;
using Tanks1990.Input.BindableIODevice.Key.Serialization;

namespace Tanks1990.Input.KeyInterpretator
{
    class KeyInterpretator 
    {
        public enum Mode{FILE,DEFAULT,ADD,REPLACE}


        #region Instancing
        static private KeyInterpretator Instance;
        static public KeyInterpretator GetInstance()
        {
            if (Instance == null) Instance = new KeyInterpretator();
            return Instance;
        }
        #endregion

        #region Data
        public Dictionary<string, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool>> KeyActivationFunctionsDictionary { get; set; }
        public Dictionary<string, Action> KeyActionsDictionary { get; set; }
        //приложение может сохранить примеры клавишь, потом воссоздать, надо сделать возможность добавления клавиш без сериализации
        private List<LightKeyDataContainer> Samples;
        #endregion

        public KeyInterpretator()
        {
            KeyActivationFunctionsDictionary = new Dictionary<string, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool>>();
            KeyActionsDictionary = new Dictionary<string, Action>();
            Samples = new List<LightKeyDataContainer>();
            //RegisterDefaultTrigers();
        }


        #region Registering
        /*
         RULES:
         One action - one description
         one triger - one description

            one key - one descr
            many Actions, many trigers
             */
        #region Registering
        public void RegisterAction(string key, Action action)
        {
            if (KeyActionsDictionary.Keys.ToList().Contains(key)) throw new Exception($"Key {key} already registered like {action}");
            if (KeyActionsDictionary.Values.ToList().Contains(action)) throw new Exception($"Action {action} already registered like {key}");
            KeyActionsDictionary.Add(key, action);
        }
        public void RegisterTriger(string key, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> triger)
        {
            if (KeyActivationFunctionsDictionary.Keys.ToList().Contains(key)) throw new Exception($"Key {key} already registered like {triger}");
            if (KeyActivationFunctionsDictionary.Values.ToList().Contains(triger)) throw new Exception($"Triger {triger} already registered like {key}");
            KeyActivationFunctionsDictionary.Add(key, triger);
        }
        public void RegisterSample(LightKeyDataContainer value)
        {
            if (Samples.Find(i => i.Description == value.Description) != null) throw new Exception($"Key {value.Description} already registered");
            if (!CheckForExist(KeyActionsDictionary.Keys.ToList(), value.ActionF)) throw new Exception($"Some actions dont registered");
            Samples.Add(value);
        }
        #endregion
        #region Unregistering
        public int UnregisterSample(string Description)
        {
            return Samples.RemoveAll(i => i.Description == Description);
        }
        public bool UnregisterAction(string Key)
        {
            return KeyActionsDictionary.Remove(Key);
        }
        public bool UnregisterTriger(string Key)
        {
            return KeyActivationFunctionsDictionary.Remove(Key);
        }
        #endregion
        #region GetData
        public LightKeyDataContainer GetSample(string Description)
        {
            return Samples.Find(i => i.Description == Description);
        }
        public Action GetAction(string Key)
        {
            return KeyActionsDictionary[Key];
        }
        public Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> GetTriger(string Key)
        {
            return KeyActivationFunctionsDictionary[Key];
        }
        #endregion

        #endregion

        #region Tools
        private bool CheckForExist<T>(List<T> dictionary, List<T> values)
        {
            foreach (var item in values)
            {
                if (!dictionary.Contains(item))
                    return false;
            }
            return true;
        }
        #endregion


        private List<BindibleKey> GetLayoutFromSamples()
        {
            List<BindibleKey> keys = new List<BindibleKey>();
            foreach (var item in Samples)
            {
                var t = new BindibleKey(item.Description, KeyActivationFunctionsDictionary[item.Triger], null);
                //add all actions
                item.ActionF.ForEach(i => { t.Trigered += KeyActionsDictionary[i]; });
                keys.Add(t);
            }
            return keys;
        }
        private void LoadDeafultSamples()
        {
            //default layout
            RegisterDefaultTrigers();
            RegisterDefaultSamples();
        }

        #region Default values
        private void RegisterDefaultSamples()
        {
            RegisterSample(new LightKeyDataContainer(new List<string>() { "RenderWindow.Close()" }) { Description = "Escape", Triger = "Pressed_Escape" });
        }
        private void RegisterDefaultTrigers()
        {
            RegisterTriger("Pressed_Escape", ((sender, history, e) => { return e.Code == Keyboard.Key.Escape; }));
        }
        #endregion

        #region Save-load
        public bool LoadFromFileSamples(string Path = "DEFAULT")
        {
            if (Path == "DEFAULT") return false;
            FileStream fs = new FileStream($"{ Path }.ly", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
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
            finally { 
                fs.Close();
            }
        }
        public void SaveToFileSamples(string Path = "DEFAULT")
        {
            if (Path == "DEFAULT") return;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream($"{Path}.ly", FileMode.OpenOrCreate);
            binaryFormatter.Serialize(fs, Samples);
            fs.Close();
        }
        #endregion


        public void LoadLayout(BindableIODevice.Controller.BindableInputDevice device,string Path = "DEFAULT", Mode mode = Mode.ADD) {
            var keys = device.UnsafeGetKeys();
            //load samples from file
            if (!LoadFromFileSamples(Path)&&Samples.Count<=0) LoadDeafultSamples();
            switch (mode)
            {
                case Mode.ADD:
                    keys.AddRange(GetLayoutFromSamples());
                    break;
                case Mode.REPLACE:
                    keys = GetLayoutFromSamples();
                    break;
                default:
                    throw new Exception("Wrong mode! You can use ADD|REPLACE");
            }
        }


    }
}
