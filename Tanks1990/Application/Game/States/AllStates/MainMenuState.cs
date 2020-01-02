using Input.KeyInterpretator;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using Tanks1990.Application.Data.Graphic;
using Tanks1990.Application.Data.GraphicMng.GrpahicController;
using Tanks1990.Application.Interfaces;
using TGUI;

namespace Tanks1990.Application.Game.States
{
    class MainMenuState : IGameState
    {
        public bool DontUnloadFromMemory { get; set; }
        public Gui GUI { get; set; }
        public GraphicController GraphicController { get; set; }

        public event Action<StateBuilder.StateID> ChangeState;
        public MainMenuState()
        {
            DontUnloadFromMemory = true;
            GraphicController = new GraphicController();
            GraphicController.Layers.Add(new Layer());
            GraphicController.Layers[0].Deepths = 0;
            GraphicController.Layers[0].Drawables.Add(new Sprite(GraphicManager.Instance.Textures["MAIN_MENU_BG.jpg"]));

            GUI = new Gui();

            var vl = new VerticalLayout();
            vl.SetPosition(new Layout2d("&.width-width + width/20", "&.height+40% - height"));

            GUI.Add(vl);
            
            //GUI.Add(new Button() { Position = new Vector2f(100, 200), Text = "PLAY", TextSize = 40 }) ;
            //GUI.Add(new Button() { Position = new Vector2f(100, 240), Text = "Settings", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 280), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 320), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 360), Text = "Reserved", TextSize = 40 });

            var t = new Button() { Position = new Vector2f(100, 700), Text = "Exit", TextSize = 40 };
            //t.SetPosition(new Layout2d("&.width / 6", "&.height - height-10%"));
            t.Clicked += (object sender, SignalArgsVector2f arg) => { KeyInterpretator.GetInstance().GetAction("RenderWindow.Close()")(); };
            vl.Add(t);

           // vl.SetSize(new Layout2d("max(width, 100)", "&.h"));

        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            GUI.Target = target as RenderWindow;
            GraphicController.Sort();
            GraphicController.Draw(target, states);
            GUI.Draw();
        }

        public void Update(Time time)
        {


        }

        public void HotSave()
        {
            //stop music
        }

        public void Resizing(object sender, SizeEventArgs arg)
        {

        }
    }
}
