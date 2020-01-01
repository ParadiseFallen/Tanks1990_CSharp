using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.States;
//using Tanks1990.Application.Game.Logic;
using Tanks1990.Input.BindableIODevice.Controller;
using Tanks1990.Input.BindableIODevice.Key;
using Tanks1990.Input.KeyInterpretator;

namespace Tanks1990.Application.Game
{
    class Game : IDisposable
    {
        //window
        RenderWindow renderWindow;
        //input device
        BindableInputDevice inputDevice;
        //current state of game
        IGameState currentState;

        public delegate void ExeptionCollector(Exception ex);
        private ExeptionCollector exeptionCollector;
        /// <summary>
        /// local gui. Must be in all states etc
        /// </summary>
        private TGUI.Gui DevGUI;
        public void Dispose()
        {
            //unload all resources!
        }

        //ctor. init resources
        public Game(RenderWindow renderWindow,BindableInputDevice inputDevice, ExeptionCollector collector= null)
        {
            this.renderWindow = renderWindow;
            this.inputDevice = inputDevice;
            exeptionCollector = collector;
            BasicSetup();

            currentState = StateBuilder.GetState(StateBuilder.StateID.MainMenu);


            SetupDevGUI();
            //load resources
        }
        private void SetupDevGUI() {
            DevGUI = new TGUI.Gui();
            DevGUI.Target = renderWindow;

            var vl = new TGUI.VerticalLayout();
            DevGUI.Add(vl);
            vl.Size = new Vector2f(80,30);
            var b1 = new TGUI.Button() ;
            vl.Add(b1, "FPS_COUNTER_btn");
            vl.Add(new TGUI.Button(), "WINDOW_HANDLE_btn");

            inputDevice.AddKey(new BindibleKey("Pressed_Tilda", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) => { return arg.Code == Keyboard.Key.Tilde; }, () =>{ DevGUI.GetWidgets().ForEach(i => i.Visible = !i.Visible) ; }));
        }

        //main game loop
        public void Run()
        {
            Clock timeUpdate = new Clock();
            //main game loop
            Time time;
            while (renderWindow.IsOpen)
            {
                try
                {
                    time = timeUpdate.Restart();
                    Update(time);

                    renderWindow.DispatchEvents();
                    renderWindow.Clear();


                    currentState.Update(time);
                    renderWindow.Draw(currentState);

                    DevGUI?.Draw();

                    renderWindow.Display();

                }
                catch (Exception ex)
                {
                    exeptionCollector(ex);
                }
            }
            timeUpdate.Dispose();
            DevGUI.Dispose();
        }

        private void Update(Time time) {

            (DevGUI.Get("FPS_COUNTER_btn") as TGUI.Button).Text = $"FPS:{Math.Round((1f/time.AsSeconds())).ToString()}";
            (DevGUI.Get("WINDOW_HANDLE_btn") as TGUI.Button).Text = $"Adress:{renderWindow.SystemHandle}";



        }

        #region Startup
        private void BasicSetup() { 
            renderWindow.SetTitle("Game: Tanks1990");
            inputDevice.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;



        }
        #endregion
    }
}
