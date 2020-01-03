using Input.KeyInterpretator;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Wraps
{
    class RenderWindowWrap
    {
        public event Action<RenderWindow> WindowChanged;
        private RenderWindow window;
        public RenderWindow Window
        {
            get
            {
                if (window is null) window = new RenderWindow(new SFML.Window.VideoMode(1920, 1080), "TitleWindow", SFML.Window.Styles.Default);
                return window;
            }
            set
            {
                this.window = value;
                WindowChanged?.Invoke(window);
            }
        }
        public void ChangeWindow(SFML.Window.VideoMode videoMode, string title = "Null", SFML.Window.Styles style = SFML.Window.Styles.Default)
        {
            Window.Close();
            Window = new RenderWindow(new SFML.Window.VideoMode(1920, 1080), title, style);
            Window.Size = new SFML.System.Vector2u(videoMode.Width, videoMode.Height);

            window.Closed += (sender, e) => { (sender as RenderWindow)?.Close(); };

            if (KeyInterpretator.Instance.KeyActionsDictionary.ContainsKey("RenderWindow.Close()"))
                KeyInterpretator.Instance.UnregisterAction("RenderWindow.Close()");
            KeyInterpretator.Instance.RegisterAction("RenderWindow.Close()", window.Close);
        }
        public void Subscribe(Action<RenderWindow> subscribe)
        {
            WindowChanged += subscribe;
            subscribe(window);
        }
    }
}
