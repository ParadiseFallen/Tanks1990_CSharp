using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Interfaces;

namespace Tanks1990.Application.Data.Graphic
{
    class GraphicManager
    {

        /// <summary>
        /// работает с конкретным провайдером, который дает словарь имя-текстура
        /// </summary>
        public IProvider<Dictionary<string, Texture>> myProvider;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="provider">Провайдер, с которым он будет работать, НЕ может быть NULL</param>
        public GraphicManager(IProvider<Dictionary<string, Texture>> provider)
        {
            myProvider = provider;
            Textures = provider.Get();
        }
        /// <summary>
        /// Просит провайдера сохранить картинку с именем
        /// </summary>
        /// <param name="img">Сама картинка</param>
        /// <param name="name">Имя картини</param>
        public void SaveImg(Image img,string name) {
            Dictionary<string, Texture> t = new Dictionary<string, Texture>();
            t.Add(name, new Texture(img));
            myProvider.Place(t);
        }
        /// <summary>
        /// Словарь текстур
        /// </summary>
        public Dictionary<string, Texture> Textures {get;private set;}

    }
}
