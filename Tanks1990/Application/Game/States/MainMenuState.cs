using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Tanks1990.Application.Game.States
{
    class MainMenuState : IGameState
    {
        private TGUI.Gui gui;
        public Enum State { get; set; }

        public void Draw(RenderTarget target, RenderStates states)
        {
            gui.Draw();
        }

        public void Update(Time time)
        {

        }
    }
}
