using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.Interfaces
{
    interface IBindebleKey<T>
    {
        /// <summary>
        /// Description of key
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// What key to do when trigered
        /// </summary>
        event Action Trigered;
        /// <summary>
        /// Function of activation key
        /// </summary>
        Func<object, Queue<T>, T, bool> Triger { get; set; }
        void Update(object sender, T arg, Queue<T> history);
        bool Locked { get; set; }

    }
}
