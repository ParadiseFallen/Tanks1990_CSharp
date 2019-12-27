﻿#define NON_WORK
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Tanks1990.Input.BindableIODevice.Key;
using Tanks1990.Input.BindableIODevice.Key.Serialization;
using Tanks1990.Input.KeyInterpretator;

namespace Tanks1990.Input.BindableIODevice.Controller
{
    /// <summary>
    ///Bindeble keyboard 
    /// </summary>
    class BindableInputDevice
    {

        //*////////////////TODO Make Keys Private

        /*DATA*/
        ///// <summary>
        ///// List of bindible keys
        ///// </summary>
        //public List<BindibleKey> Keys { get; private set; }
        ///// <summary>
        ///// length of history
        ///// </summary>
        //public int HistorySize { get; set; }
        ///// <summary>
        ///// history of pressed keys
        ///// </summary>
        //private Queue<KeyEventArgs> History = new Queue<KeyEventArgs>();


        #region Data
        /*DATA*/
        /// <summary>
        /// List of bindible keys
        /// </summary>
        private List<BindibleKey> Keys { get; set; }
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
        /// Get list of Keys
        /// </summary>
        /// <returns>List<BindibleKey></returns>
        public List<BindibleKey> UnsafeGetKeys() { return Keys; }

        #endregion
        #region Events
        /// <summary>
        /// Action called when key added, you can Connect KeyInterpretator.RegisterSample(), 
        /// Action will called only when you send metadata
        /// </summary>
        public event Action<LightKeyDataContainer> KeyAdded;
        #endregion

        #region Work with keys

        /// <summary>
        /// Add new key to list, if you send metadata KeyAdded will be invoked
        /// </summary>
        /// <param name="key">Bindible key</param>
        /// <param name="metadata">Info of key in LightKeyDataContainer</param>
        public void AddKey(BindibleKey key, LightKeyDataContainer metadata = null)
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
        public BindibleKey GetKey(string KeyDescription)
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
        #endregion

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="Keys">Layout|List<BindibleKey> keys|DEFAULT = null</param>
        /// <param name="HistorySize">How many data to remember|int HistorySize|DEFAULT = 0</param>
        public BindableInputDevice(List<BindibleKey> Keys = null, int HistorySize = 0)
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


        
        ///// <summary>
        ///// Try to load confuguration
        ///// </summary>
        ///// <param name="path">if clear load default</param>
        //public void LoadConfiguration(string path = "DEFAULT")
        //{
        //    var instance = KeyInterpretator.KeyInterpretator.GetInstance();
        //    if (path=="DEFAULT")
        //    {
        //        instance.LoadDeafultSamples();
        //    }
        //    else
        //    {
        //        instance.LoadFromFileSamples(path);
        //    }
        //    this.Keys.AddRange(instance.LoadLayout());
        //}
        ///// <summary>
        ///// save layout in file
        ///// </summary>
        ///// <param name="path">target file</param>
        //public void SaveConfiguration(string path)
        //{
        //   KeyInterpretator.KeyInterpretator.GetInstance().SaveToFileSamples(path);
        //}


    }
}