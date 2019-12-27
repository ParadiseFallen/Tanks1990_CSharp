//#define SAVE_KEYBOARD_TEST
//#define LOAD_KEYBOARD_TEST
#define STOP_AT_END

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.BindableIODevice.Key;
using Tanks1990.Input.KeyInterpretator;


namespace Tanks1990
{
    class Program
    {
        static void Main(string[] args)
        {
            //keyboard
            BindableInputDevice keyboard = new BindableInputDevice();
            keyboard.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;

            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "test");
            KeyInterpretator.GetInstance().RegisterAction("RenderWindow.Close()", window.Close);

            window.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };

            window.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };

            //connect keyboard
            window.KeyPressed += keyboard.Update;
            int a = 0;



            keyboard.AddKey(new BindibleKey("F5", (sender, history, key) => { return key.Code == Keyboard.Key.F5; }, () => { Console.WriteLine($"FuckYou"); }));


            //load conf
            //keyboard.LoadConfiguration();

            //load only afetr all registration
            KeyInterpretator.GetInstance().LoadFromFileSamples("Test");
            KeyInterpretator.GetInstance().LoadLayout(keyboard);
            do
            {
                window.DispatchEvents();
                window.Clear();

                //window.Draw(sprite);


                window.Display();


            } while (window.IsOpen);

            //keyboard.SaveConfiguration("D:/Layout.lyt");
            KeyInterpretator.GetInstance().SaveToFileSamples("Test");

#if STOP_AT_END
            Console.WriteLine("Program done");
            Console.ReadKey();
#endif
        }

    }
}
