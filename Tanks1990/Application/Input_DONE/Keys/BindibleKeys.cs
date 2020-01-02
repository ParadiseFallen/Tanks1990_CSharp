using Input.Interfaces;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Input.Key
{
    /// <summary>
    /// Bindible key
    /// </summary>
    class BindibleKey : IBindebleKey<KeyEventArgs>
    {
        #region Data
        public string Description { get ; set ; }
        public bool Locked { get; set; }
        public Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> Triger { get ; set ; }
        public event Action Trigered;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ActivationFunction">Function of activation</param>
        /// <param name="Fucntion">Action</param>
        /// <param name="Description">Description/param>
        public BindibleKey(string Description,Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> ActivationFunction = null, Action Fucntion = null)
        {
            this.Triger = ActivationFunction;
            this.Trigered += Fucntion;
            this.Description = Description;
        }

        /// <summary>
        /// Try to trigger
        /// </summary>
        /// <param name="sender">Who is sender</param>
        /// <param name="arg">Key</param>
        /// <param name="history">History of pressed keys</param>
        public void Update(object sender ,KeyEventArgs arg ,Queue<KeyEventArgs> history ) {
            if (Locked) return;
           if (Triger.Invoke(sender, history,arg)) Trigered.Invoke();
        }
        /// <summary>
        /// ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"KEY: {Description}\n\tTriger on: {Triger}. Invoke -> {Trigered}";
        }
    }
}
