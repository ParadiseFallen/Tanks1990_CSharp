#define NON_WORK
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Tanks1990.IO.BindableIODevice.Key;
using Tanks1990.IO.KeyInterpretator;

namespace Tanks1990.IO.BindableIODevice.Controller
{
    /// <summary>
    ///Bindeble keyboard 
    /// </summary>
    class BindableInputDevice
    {

        //*////////////////TODO Make Keys Private

        /*DATA*/
        /// <summary>
        /// List of bindible keys
        /// </summary>
        public List<BindibleKey> Keys { get; private set; }
        /// <summary>
        /// length of history
        /// </summary>
        public int HistorySize { get; set; }
        /// <summary>
        /// history of pressed keys
        /// </summary>
        private Queue<KeyEventArgs> History = new Queue<KeyEventArgs>();
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Bind action to key by key description,
        /// key must be already created
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="action">Bindeble action</param>
        public void BindToKey(string KeyDescription, Action action) {
            var key = GetKey(KeyDescription);
            if (key is null)
            {
                throw new Exception($"Cant find key with description {KeyDescription}");
            }
            key.Trigered += action;
        }
        /// <summary>
        /// Change triger on key by descr
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        /// <param name="Triger">New key triger</param>
        public void ChangeTriger(string KeyDescription, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> Triger) {
            GetKey(KeyDescription).Triger = Triger;
        }
        /// <summary>
        /// Create key with description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        public void CreateKeyWithDescription(string KeyDescription) {
            if (GetKey(KeyDescription) is null)
            {
                Keys.Add(new BindibleKey(null,null, KeyDescription));
            }
        }

        /// <summary>
        /// find key by description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        /// <returns></returns>
        public BindibleKey GetKey(string KeyDescription) {
            return Keys.Find(i => i.Description == KeyDescription);
        }
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="Keys">Layout|List<BindibleKey> keys|DEFAULT = null</param>
        /// <param name="HistorySize">How many data to remember|int HistorySize|DEFAULT = 0</param>
        public BindableInputDevice(List<BindibleKey> Keys = null, int HistorySize  = 0)
        {
            this.Keys = Keys;
            this.HistorySize = HistorySize;
            //check null
            if (Keys is null) this.Keys = new List<BindibleKey>();
        }
        /// <summary>
        /// update history of keys
        /// </summary>
        /// <param name="e">KeyData</param>
        private void UpdateHistory(KeyEventArgs e) {
            History.Enqueue(e);
            if (History.Count > HistorySize) History.Dequeue();
        }
        /// <summary>
        /// Send all keys KeyEventArgs
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">key</param>
        public void Update(object sender, KeyEventArgs e)
        {
            if (HistorySize != 0) UpdateHistory(e);
            Keys.ForEach(i=>i.Update(sender,e, History));
        }

        /// <summary>
        /// Try to load confuguration
        /// </summary>
        /// <param name="path">if clear load default</param>
        public void LoadConfiguration(string path = "DEFAULT")
        {
            var instance = KeyInterpretator.KeyInterpretator.GetInstance();
            if (path=="DEFAULT")
            {
                instance.LoadDeafultSamples();
            }
            else
            {
                instance.LoadFromFileSamples(path);
            }
            this.Keys.AddRange(instance.LoadLayout());
        }
        /// <summary>
        /// save layout in file
        /// </summary>
        /// <param name="path">target file</param>
        public void SaveConfiguration(string path)
        {
           KeyInterpretator.KeyInterpretator.GetInstance().SaveToFileSamples(path);
        }
        /////
        /////////////TODO////////////////////////////////////////////////////////////////////
        ////Fix serialization Сделать словарь по функциям типа string/action
#if !NON_WORK
        public bool SaveConfiguration(string path) {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream FS = new FileStream($"{path}.kb", FileMode.OpenOrCreate);
                binaryFormatter.Serialize(FS, this);
                FS.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public bool LoadConfiguration(string path)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream FS = new FileStream($"{path}.kb", FileMode.OpenOrCreate);
                var t = (binaryFormatter.Deserialize(FS) as BindebleKeyboard);
                Keys = t.Keys;
                HistorySize = t.HistorySize;
                FS.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
#endif

    }
}
