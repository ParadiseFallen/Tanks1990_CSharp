using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.Logic;
using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.KeyInterpretator;

namespace Tanks1990.Application.Game
{
    class Game : IDisposable
    {
        //window
        RenderWindow renderWindow;
        //input device
        BindableInputDevice inputDevice;
        //current state of game
        IGameState currentState;
        public void Dispose()
        {
            //unload all resources!
        }

        //ctor. init resources
        public Game(RenderWindow renderWindow,BindableInputDevice inputDevice)
        {
            this.renderWindow = renderWindow;
            this.inputDevice = inputDevice;
            BasicSetup();

            //load resources
        }

        //main game loop
        public void Run()
        {
            Clock timeUpdate = new Clock();
            //main game loop
            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();
                renderWindow.Clear();

                currentState.Update(timeUpdate.Restart());
                renderWindow.Draw(currentState);

                renderWindow.Display();
            }
        }

        #region Startup
        private void BasicSetup() { 
            renderWindow.SetTitle("Game: Tanks1990");
            inputDevice.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;



        }
        #endregion
    }
}
