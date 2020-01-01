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

        public bool DontUnloadFromMemory { get; set; }

        public MainMenuState()
        {
            DontUnloadFromMemory = true;

            gui = new TGUI.Gui();


            var window = new TGUI.ChildWindow();
            var window2 = new TGUI.ChildWindow();
            window.Add(window2);

            gui.Add(window, "test");
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            gui.Target = target as RenderWindow;



            gui.Draw();
        }


        public void Update(Time time)
        {


        }
    }
}
