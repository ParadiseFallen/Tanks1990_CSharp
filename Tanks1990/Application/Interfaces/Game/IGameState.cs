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
        event Action<StateBuilder.StateID> ChangeState;
        void Resizing(object sender, SizeEventArgs arg);
        void HotSave();
        GraphicController GraphicController { get; set; }
        TGUI.Gui GUI { get; set; }
        bool DontUnloadFromMemory { get; set; }
    }
}
