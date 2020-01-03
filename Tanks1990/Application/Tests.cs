#define DEBUG
using System;
using System.Collections.Generic;
using Input.BindableIODevice.Controller;
using Input.KeyInterpretator;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Tanks1990.Application.Data.Graphic;
using Tanks1990.Application.Data.Providers;
using Tanks1990.Providers;
using static Input.KeyInterpretator.KeyInterpretator;

#if DEBUG
namespace Tanks1990.Application.DEBUG
{
    class Tests
    {

        public void Run()
        {


            KeyInterpretator.Instance.Provider = new SampleKeyFileProvider() { Link = "test.ly" };


            //keyboard
            BindableInputDevice keyboard = new BindableInputDevice();
            keyboard.KeyAdded += KeyInterpretator.Instance.RegisterSample;


            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "test");
            KeyInterpretator.Instance.RegisterAction("RenderWindow.Close()", window.Close);

            window.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };
            window.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };

            Console.WriteLine(window.GetViewport(window.GetView()));


            //connect keyboard
            window.KeyPressed += keyboard.Update;


            //keyboard.AddKey(new BindibleKey("F5", (sender, history, key) => { return key.Code == Keyboard.Key.F5; }, () => { sprite.Texture = sprite.Texture == graphicManager.Textures["LowPolyBackground.png"] ? graphicManager.Textures["LowPolyBackground2.png"] : graphicManager.Textures["LowPolyBackground.png"]; }));



            //load only afetr all registration
            KeyInterpretator.Instance.LoadSamples(LoadMode.PROVIDER);
            KeyInterpretator.Instance.LoadLayout(keyboard);





            //mock.MyGravity = new PhysicCaster2D();
            //mock.MyGravity.Power = 1;
            //mock.MyGravity.Range = new Vector2d(1, 1);

            TGUI.Gui gui = new TGUI.Gui(window);
            var box = new TGUI.TextBox() { Position = new Vector2f(100, 100) };
            box.Focused += (object sender, EventArgs arg) => { Console.WriteLine($"Focused! \t{sender}\n{arg}"); };
            gui.Add(box, "TextBox");
            TGUI.Grid grid = new TGUI.Grid();


            //добавить всем блокировку и разблокировку клавиатуры
            gui.GetWidgets().ForEach(i =>
            {
                i.Focused += (object sender, EventArgs args) =>
                {
                    keyboard.LockKeys((string name)=> {
                        return true;
                    });

                };
                i.Unfocused += (object sender2, EventArgs args2) =>
                {
                    keyboard.UnlockKeys((string name) => {
                        return true;
                    });
                };
            });

            Clock clock = new Clock();
            do
            {
                window.DispatchEvents();
                window.Clear();




                gui.Draw();



                window.Display();


            } while (window.IsOpen);

            //keyboard.SaveConfiguration("D:/Layout.lyt");
            KeyInterpretator.Instance.SaveSamples(LoadMode.PROVIDER);

#if STOP_AT_END
            Console.WriteLine("Program done");
            Console.ReadKey();
#endif
        }

    }
}

#endif
