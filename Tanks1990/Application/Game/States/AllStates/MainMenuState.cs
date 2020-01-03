using SFML.Graphics;
using SFML.System;
using System;
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
        public bool Initialized { get; set; } = false;

        public event Action<StateBuilder.StateID> ChangeState;
        public MainMenuState()
        {
            DontUnloadFromMemory = true;
            GraphicController = new GraphicController();
            GraphicController.Layers.Add(new Layer() { LayerDescription ="Background"});
            GraphicController.Layers[0].Deepths = 0;
            GraphicController.Layers[0].Drawables.Add(new Sprite(GraphicManager.Instance.Textures["MAIN_MENU_BG.jpg"]));


            /////GUI setups
            GUI = new Gui();
            //create panel
            var lable = new Label("Version: 0.1 pre-alpha") { Position = new Vector2f(20, 20), TextSize = 20 };
            lable.SetPosition(new Layout2d("100%-w","100%-h"));
            GUI.Add(lable);


            var Mute = new BitmapButton();

            GUI.Add(lable);
            

            var vl = new VerticalLayout();



            var renderer = new ButtonRenderer();
            renderer.BackgroundColor = Color.Transparent;
            renderer.BackgroundColorDown = Color.Transparent;
            renderer.TextColorHover = Color.Cyan;
            renderer.TextColor = Color.White;
            var btn = new Button("test");
            btn.TextSize = 80;
            
            btn.SetRenderer(renderer.Data);

            vl.Add(btn);
            vl.Add(new Button("Play") { TextSize = 80});
            vl.Add(new Button("Settings") { TextSize = 80 });
            vl.Add(new Button("About") { TextSize = 80 });
            vl.Add(new Button("Exit") { TextSize = 80 });
            vl.SetPosition(new Layout2d("5%","&.h*0.6"));
            vl.SetSize(new Layout2d("&.w*0.2", "&.h*0.3"));
            //panel.Renderer.BackgroundColor = Color.Transparent;

            GUI.Add(vl);
            //var vl = new VerticalLayout();

            //vl.SetPosition(new Layout2d("&.width-width + width/20", "&.height+40% - height"));
            //vl.SetSize(new Layout2d("&.width-width", " &.h"));

            //GUI.Add(vl);

            //GUI.Add(new Button() { Position = new Vector2f(100, 200), Text = "PLAY", TextSize = 40 }) ;
            //GUI.Add(new Button() { Position = new Vector2f(100, 240), Text = "Settings", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 280), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 320), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 360), Text = "Reserved", TextSize = 40 });

            //var t = new Button() { Position = new Vector2f(100, 700), Text = "Exit", TextSize = 40 };
            ////t.SetPosition(new Layout2d("&.width / 6", "&.height - height-10%"));
            //t.Clicked += (object sender, SignalArgsVector2f arg) => { KeyInterpretator.GetInstance().GetAction("RenderWindow.Close()")(); };
            //vl.Add(t);


        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            GraphicController.Sort();
            GraphicController.Draw(target, states);
            GUI.Draw();
        }

        public void Update(Time time)
        {


        }

        public void Save()
        {
            //stop music
        }
        public void Load()
        {
            //resume music
        }

    }
}
