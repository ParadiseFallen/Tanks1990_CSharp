//using Input.BindableIODevice.Controller;
//using Input.KeyInterpretator;
//using SFML.Graphics;
//using SFML.System;
//using SFML.Window;
//using System;

//namespace Tanks1990.Application
//{
//    class AppForWindows
//    {
//        #region Data
//        RenderWindow renderWindow;
//        BindableInputDevice keyboard;
//        //res mng
//        #endregion

//        #region Main
//            /// <summary>
//            /// Приложение, предоставляет клавиатуру, окно, собтирает ошибки
//            /// </summary>
//        public AppForWindows()
//        {
//            ////Перенесити в констркуктор App
//            renderWindow = new RenderWindow(new VideoMode(1920,1080), "AppForWindows",Styles.Fullscreen);
//            BaseWindowLinks();
//            renderWindow.SetFramerateLimit(60);

//            keyboard = new BindableInputDevice();

//            renderWindow.KeyPressed += keyboard.Update;

//            KeyInterpretator.Instance.LoadLayout(keyboard);
//            //create window. create app res
//        }

//        private void ExeptionColector(Exception ex) {
//            Console.WriteLine(ex);
//        }

//        public void Run() {
//            //Не обязательно конкретно эта игра. App доставлено окно
//            using (App.App game = new App.App(renderWindow, keyboard, ExeptionColector)) {
//                game.Run();
//            }
//            renderWindow.Dispose();
//        }
//        #endregion

//        #region Startup
//        private void BaseWindowLinks()
//        {
//            //registration func
//            KeyInterpretator.Instance.RegisterAction("RenderWindow.Close()", renderWindow.Close);
//            //linking
//            renderWindow.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };
//            //windowContainer.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };
//        }
//        #endregion

//    }
//}
