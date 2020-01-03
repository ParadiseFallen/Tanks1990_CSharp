using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Data.GraphicMng.GrpahicController;
using Tanks1990.Application.Game.Physic;
using Tanks1990.Application.Game.States;

namespace Tanks1990.Application.Interfaces
{
    interface IGameState : IUpdatebleTime,Drawable
    {
        /// <summary>
        /// Нужго ли еще доставлять данные после получения из фабрики
        /// </summary>
        bool Initialized { get; set; }
        /// <summary>
        /// Подписываеться из App 
        /// </summary>
        event Action<StateBuilder.StateID> ChangeState;
        /// <summary>
        /// Сохранение горячего состояния
        /// </summary>
        void Save();
        /// <summary>
        /// Восстановление 
        /// </summary>
        void Load();
        /// <summary>
        /// поставщик графики
        /// </summary>
        GraphicController GraphicController { get; set; }
        /// <summary>
        /// Интерфейс состояния
        /// </summary>
        TGUI.Gui GUI { get; set; }
        /// <summary>
        /// Не выгружать из памяти
        /// </summary>
        bool DontUnloadFromMemory { get; set; }
    }
}
