#define DEBUG
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Tanks1990.Application;
using Tanks1990.Application.Data.Graphic;
using Tanks1990.Application.Data.Providers;
using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.BindableIODevice.Key;
using Tanks1990.Input.KeyInterpretator;
using Tanks1990.Providers;
using static Tanks1990.Input.KeyInterpretator.KeyInterpretator;

#if DEBUG
namespace Tanks1990.Application.DEBUG
{
    class Tests
    {

        public void Run() {

            GraphicManager graphicManager = new GraphicManager(new FileGraphicProvider() { Link = "C:/Users/Paradise/Desktop/Tanks1990_CSharp/Tanks1990/Application/Resources", Filter = ((string name)=>{return name.Contains(".png")|| name.Contains(".jpg"); }) });
            KeyInterpretator.GetInstance().Provider = new SampleKeyFileProvider() { Link = "test.ly" };


            //keyboard
            BindableInputDevice keyboard = new BindableInputDevice();
            keyboard.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;


            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "test");
            KeyInterpretator.GetInstance().RegisterAction("RenderWindow.Close()", window.Close);

            window.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };
            window.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };

            //connect keyboard
            window.KeyPressed += keyboard.Update;

            Sprite sprite = new Sprite(graphicManager.Textures["LowPolyBackground.png"]);

            keyboard.AddKey(new BindibleKey("F5", (sender, history, key) => { return key.Code == Keyboard.Key.F5; }, ()=> { sprite.Texture = sprite.Texture == graphicManager.Textures["LowPolyBackground.png"] ? graphicManager.Textures["LowPolyBackground2.png"] : graphicManager.Textures["LowPolyBackground.png"]; }));


            //load conf
            //keyboard.LoadConfiguration();

            //load only afetr all registration
            KeyInterpretator.GetInstance().LoadSamples(LoadMode.PROVIDER);
            KeyInterpretator.GetInstance().LoadLayout(keyboard);
            do
            {
                window.DispatchEvents();
                window.Clear();

                window.Draw(sprite);


                window.Display();


            } while (window.IsOpen);

            //keyboard.SaveConfiguration("D:/Layout.lyt");
            KeyInterpretator.GetInstance().SaveSamples(LoadMode.PROVIDER);

#if STOP_AT_END
            Console.WriteLine("Program done");
            Console.ReadKey();
#endif
        }

    }
}

#endif
