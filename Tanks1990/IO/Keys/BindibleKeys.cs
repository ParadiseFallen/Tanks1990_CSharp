using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.IO.BindableIODevice.Key
{
    /// <summary>
    /// Bindible key
    /// </summary>
    class BindibleKey
    {
        /// <summary>
        /// Description of key
        /// </summary>
        public string Description{ get; set; }
        /// <summary>
        /// What key to do when trigered
        /// </summary>
        public event Action Trigered;
        /// <summary>
        /// Function of activation key
        /// </summary>
        public Func<object, Queue<KeyEventArgs>, KeyEventArgs,bool> Triger { get; internal set; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ActivationFunction">Function of activation</param>
        /// <param name="Fucntion">Action</param>
        /// <param name="Description">Description/param>
        public BindibleKey(Func<object, Queue<KeyEventArgs>, KeyEventArgs, bool> ActivationFunction = null, Action Fucntion = null,string Description = "NULL")
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
