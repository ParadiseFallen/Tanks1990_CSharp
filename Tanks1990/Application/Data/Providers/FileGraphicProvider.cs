using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Interfaces;
using Tanks1990.Templates.Wraps;

namespace Tanks1990.Providers
{
    /// <summary>
    /// Work ONLY with files
    /// </summary>
    class FileGraphicProvider : IProvider<Dictionary<string,Texture>>
    {
        /// <summary>
        /// Working directory
        /// </summary>
        public string Link { get ; set ; }
        /// <summary>
        /// Filter wich approved files to load
        /// </summary>
        public Predicate<string> Filter { get; set; } = (string a) => { return true; };
        /// <summary>
        /// Get textures from directory which filter approved
        /// </summary>
        /// <returns> List of description</returns>
        public Dictionary<string, Texture> Get()
        {
            //create new dictionary of textures
            Dictionary<string, Texture> data = new Dictionary<string, Texture>();
            //load all filenames
            //List<string> fileNames =  Directory.GetFiles(Link).ToList();
            DirectoryInfo DI = new DirectoryInfo(Link);
            DI.GetFiles().ToList().ForEach(Console.WriteLine);
            //fileNames.ForEach(Console.WriteLine);
            //для каждого внути директории
            DI.GetFiles().ToList().ForEach(name=> {
                //если подходит для фильтра
                if (Filter(name.Name))
                    //добовляем в словарь
                    data.Add(name.Name, new Texture($"{Link}/{name}"));
            });
            return data;
        }
        /// <summary>
        /// Save list of textures with description
        /// </summary>
        /// <param name="data">List of Wrapped testures</param>
        public void Place(Dictionary<string, Texture> data)
        {
            //для каждого
            data.Keys.ToList().ForEach(i=> {
                //забираем данные о текстуре в картинку
                var t = data[i].CopyToImage();
                //сохраняем в директорию
                t.SaveToFile($"{Link}/{i}");
            });
        }
    }
}
