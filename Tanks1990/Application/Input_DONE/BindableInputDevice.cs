#define NON_WORK
using Input.Interfaces;
using Input.Key;
using Input.Key.Serialization;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Input.BindableIODevice.Controller
{
    

    /// <summary>
    ///Bindeble keyboard 
    /// </summary>
    class BindableInputDevice : IBindebleInputDevice<KeyEventArgs>
    {
        #region Data
        /// <summary>
        /// List of bindible keys
        /// </summary>
        private List<IBindebleKey<KeyEventArgs>> Keys { get; set; }
        /// <summary>
        /// length of history
        /// </summary>
        public int HistorySize { get; set; }
        /// <summary>
        /// history of pressed keys
        /// </summary>
        private Queue<KeyEventArgs> History = new Queue<KeyEventArgs>();
        /// <summary>
        /// UNSAFE! get list of Keys
        /// </summary>
        /// <returns>List<BindibleKey></returns>
        public List<IBindebleKey<KeyEventArgs>> UnsafeGetKeys() { return Keys; }
        #endregion
        #region Events
        /// <summary>
        /// Action called when key added, you can Connect KeyInterpretator.RegisterSample(), 
        /// Action will called only when you send metadata
        /// </summary>
        public event Action<KeyMetadata> KeyAdded;
        #endregion
        #region Work with keys
        /// <summary>
        /// Add new key to list, if you send metadata KeyAdded will be invoked
        /// </summary>
        /// <param name="key">Bindible key</param>
        /// <param name="metadata">Info of key in KeyMetadata</param>
        public void AddKey(IBindebleKey<KeyEventArgs> key, KeyMetadata metadata = null)
        {
            if (Keys.Find(i => i.Description == key.Description) != null) throw new Exception("Key already exist!");
            Keys.Add(key);
            if (metadata != null)
                KeyAdded.Invoke(metadata);
        }
        /// <summary>
        /// Bind action to key by key description,
        /// key must be already created
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="action">Bindeble action</param>
        public void BindToKey(string KeyDescription, Action action)
        {
            var key = GetKey(KeyDescription);
            if (key == null)
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
        public void ChangeTriger(string KeyDescription, Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> Triger)
        {
            GetKey(KeyDescription).Triger = Triger;
        }
        /// <summary>
        /// Create key with description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        public void CreateKeyWithDescription(string KeyDescription)
        {
            if (GetKey(KeyDescription) is null)
            {
                Keys.Add(new BindibleKey(KeyDescription, null, null));
            }
        }
        /// <summary>
        /// find key by description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        /// <returns></returns>
        public IBindebleKey<KeyEventArgs> GetKey(string KeyDescription)
        {
            return Keys.Find(i => i.Description == KeyDescription);
        }
        /// <summary>
        /// Remove all key with descr
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <returns></returns>
        public int DeleteKey(string KeyDescription)
        {
            return Keys.RemoveAll(i => i.Description == KeyDescription);
        }
        /// <summary>
        /// Lock all keys with descr
        /// </summary>
        /// <param name="description">list of descr keys</param>
        public void LockKeys(Predicate<String> Filter) {
            Keys.ForEach(
                i => {
                    if (Filter(i.Description)) i.Locked = true;
                }
                );
        }
        public void UnlockKeys(Predicate<String> Filter) {
            Keys.ForEach(
                    i => {
                        if (Filter(i.Description)) i.Locked = false;
                    }
                    );
        }
        #endregion

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="Keys">Layout|List<BindibleKey> keys|DEFAULT = null</param>
        /// <param name="HistorySize">How many data to remember|int HistorySize|DEFAULT = 0</param>
        public BindableInputDevice(List<IBindebleKey<KeyEventArgs>> Keys = null, int HistorySize = 0)
        {
            this.Keys = Keys;
            this.HistorySize = HistorySize;
            //check null
            if (Keys is null) this.Keys = new List<IBindebleKey<KeyEventArgs>>();
        }

        /// <summary>
        /// update history of keys
        /// </summary>
        /// <param name="e">KeyData</param>
        private void UpdateHistory(KeyEventArgs e)
        {
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
        public void ClearHistory() {
            History.Clear();
        }
    }
}
