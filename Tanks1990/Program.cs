//#define TEST
#define WINDOWS
using SFML.Graphics;
using TGUI;

namespace Tanks1990
{
    class Program
    {
        static void Main(string[] args)
        {


            //RenderWindow windowContainer = new RenderWindow(SFML.Window.VideoMode.DesktopMode, "Test");

            //TGUI.Gui gui = new TGUI.Gui();


            //var vl = new VerticalLayout();

            //vl.SetPosition(new Layout2d("&.width-width + width/20", "&.height+40% - height"));
            //vl.SetSize(new Layout2d("&.width-width", " &.h"));

            //gui.Add(vl);

            //gui.Target = (windowContainer as RenderTarget) as RenderWindow;

            ////as RenderTarget
            //return;

            using (Application.App app = new Application.App())
            {
                app.Run();
            }

        }

    }
}
