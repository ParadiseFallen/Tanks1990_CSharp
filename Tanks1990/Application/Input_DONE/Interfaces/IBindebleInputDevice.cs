using Input.Key.Serialization;
using System;
using System.Collections.Generic;

namespace Input.Interfaces
{
    /// <summary>
    /// Interface for custom bindible input device
    /// </summary>
    /// <typeparam name="T">IBindebleKey<T></typeparam>
    interface IBindebleInputDevice<T>
    {
        /// <summary>
        /// Action called when key added
        /// </summary>
        event Action<KeyMetadata> KeyAdded;
        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="sender">Caller of Update</param>
        /// <param name="e">Argument type</param>
        /// 
        void Update(object sender, T e);
        /// <summary>
        /// Добавить новую IBindebleKey<T> 
        /// </summary>
        /// <param name="key">IBindebleKey<T> object</param>
        /// <param name="metadata">Description of object optionaly</param>
        void AddKey(IBindebleKey<T> key, KeyMetadata metadata = null);
        /// <summary>
        /// Bind new action to key
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="action">Action</param>
        void BindToKey(string KeyDescription, Action action);
        /// <summary>
        /// Change key triger
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="Triger">New triger</param>
        void ChangeTriger(string KeyDescription, Func<object, Queue<T>, T, bool> Triger);
        /// <summary>
        /// Create empty key with description
        /// </summary>
        /// <param name="KeyDescription">description</param>
        void CreateKeyWithDescription(string KeyDescription);
        /// <summary>
        /// Get key by description
        /// </summary>
        /// <param name="KeyDescription">Description</param>
        /// <returns></returns>
        IBindebleKey<T> GetKey(string KeyDescription);
        /// <summary>
        /// Delete keys with description
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <returns>Count if deleted keys</returns>
        int DeleteKey(string KeyDescription);
        /// <summary>
        /// Lock keys(dont handle events)
        /// </summary>
        /// <param name="description">List of descriptions</param>
        void LockKeys(Predicate<String> Filter);
        /// <summary>
        /// Unlock by description (Handle events again)
        /// </summary>
        /// <param name="description">List of description</param>
        void UnlockKeys(Predicate<String> Filter);

    }
}
