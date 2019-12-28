using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.KeyInterpretator;

namespace Tanks1990.Application.Game
{
    class Game : IDisposable
    {
        RenderWindow renderWindow;
        BindableInputDevice inputDevice;
        //unload all resources!

        public void Dispose()
        {

        }

        //ctor. init resources
        public Game(RenderWindow renderWindow,BindableInputDevice inputDevice)
        {
            this.renderWindow = renderWindow;
            renderWindow.SetTitle("Game: Tanks1990");
            this.inputDevice = inputDevice;
            inputDevice.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;


            //load resources
        }

        //main game loop
        public void Run()
        {
            //main game loop
            while (renderWindow.IsOpen)
            {


            }
        }
    }
}
