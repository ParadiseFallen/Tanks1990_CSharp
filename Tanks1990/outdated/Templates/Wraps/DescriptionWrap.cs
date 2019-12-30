using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Wrappers
/// </summary>
namespace Tanks1990.Templates.Wraps
{
    /// <summary>
    /// Wrap T data and add description
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    class DescriptionWrap<T>
    {
        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Description of data
        /// </summary>
        public string Description { get; set; }
    }
}
