using Input.BindableIODevice.Controller;
using Input.KeyInterpretator;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Tanks1990.Application
{
    class AppForWindows
    {
        #region Data
        RenderWindow renderWindow;
        BindableInputDevice keyboard;
        //res mng
        #endregion

        #region Main
            /// <summary>
            /// Приложение, предоставляет клавиатуру, окно, собтирает ошибки
            /// </summary>
        public AppForWindows()
        {
            renderWindow = new RenderWindow(new VideoMode(1920,1080), "AppForWindows");
            BaseWindowLinks();
            renderWindow.SetFramerateLimit(60);

            keyboard = new BindableInputDevice();

            renderWindow.KeyPressed += keyboard.Update;

            KeyInterpretator.GetInstance().LoadLayout(keyboard);
            //create window. create app res
        }

        private void ExeptionColector(Exception ex) {
            Console.WriteLine(ex);
        }

        public void Run() {
            //Не обязательно конкретно эта игра. Game доставлено окно
            using (Game.Game game = new Game.Game(renderWindow, keyboard, ExeptionColector)) {
                game.Run();
            }
            renderWindow.Dispose();
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
