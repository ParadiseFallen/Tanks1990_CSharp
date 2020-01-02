using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Interfaces;

namespace Tanks1990.Application.Data.Graphic
{
    /// <summary>
    /// Single container of graphic data
    /// </summary>
    class GraphicManager
    {
        #region Instancing
        /// <summary>
        /// Instance
        /// </summary>
        private static GraphicManager _instance;
        public static GraphicManager Instance { get {
                if (_instance is null)
                {
                    _instance = new GraphicManager();
                }
                return _instance;
            } }
        #endregion

        /// <summary>
        /// работает с конкретным провайдером, который дает словарь имя-текстура
        /// </summary>

        #region Ctors
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="provider">Провайдер, с которым он будет работать, НЕ может быть NULL</param>
        private GraphicManager(){
            Textures = new Dictionary<string, Texture>();
            Drawbales = new Dictionary<string, Drawable>();
        }
        #endregion

        /// <summary>
        /// Load textures.(provider)
        /// </summary>
        public void Load() { 
            Textures = Provider.Get();
        }
        /// <summary>
        /// Просит провайдера сохранить картинку с именем
        /// </summary>
        /// <param name="img">Сама картинка</param>
        /// <param name="name">Имя картини</param>
        public void SaveImg(Image img,string name) {
            Dictionary<string, Texture> t = new Dictionary<string, Texture>();
            t.Add(name, new Texture(img));
            Provider.Place(t);
        }

        #region DATA
        /// <summary>
        /// Словарь текстур
        /// </summary>
        public Dictionary<string, Texture> Textures {get;private set;}
        /// <summary>
        /// All sprites, animations,tilesets,etc
        /// </summary>
        public Dictionary<string, Drawable> Drawbales { get; set; }
        /// <summary>
        /// Texture provider
        /// </summary>
        public IProvider<Dictionary<string, Texture>> Provider { get; set; }

        #endregion
    }
}
