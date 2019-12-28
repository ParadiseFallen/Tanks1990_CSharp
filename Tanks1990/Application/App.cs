using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.KeyInterpretator;

namespace Tanks1990.Application
{
    class App
    {
        #region Data
        RenderWindow renderWindow;
        BindableInputDevice keyboard;
        //res mng
        #endregion

        #region Main
        public App()
        {
            renderWindow = new RenderWindow(VideoMode.DesktopMode, "App");
            BaseWindowLinks();
            keyboard = new BindableInputDevice();
            //create window. create app res

        }

        public void Run() {
            //Не обязательно конкретно эта игра. Game доставлено окно
            using (Game.Game game = new Game.Game(renderWindow, keyboard)) {
                game.Run();
            }
        }
        #endregion

        #region Startup
        private void BaseWindowLinks()
        {
            //registration func
            KeyInterpretator.GetInstance().RegisterAction("RenderWindow.Close()", renderWindow.Close);


            //linking
            renderWindow.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };
            renderWindow.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };
        }
        #endregion

    }
}
