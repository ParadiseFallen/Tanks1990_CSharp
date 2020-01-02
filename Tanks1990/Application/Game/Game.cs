using Input.BindableIODevice.Controller;
using Input.Key;
using Input.KeyInterpretator;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Tanks1990.Application.Data.Graphic;
using Tanks1990.Application.Game.States;
using Tanks1990.Application.Interfaces;
using Tanks1990.Providers;

namespace Tanks1990.Application.Game
{
    class Game : IDisposable
    {
        #region Data
        //window
        private RenderWindow renderWindow;
        //input device
        private BindableInputDevice inputDevice;
        //current state of game
        private IGameState currentState;
        
        private Time lU = new Time();

        public delegate void ExeptionCollector(Exception ex);
        private ExeptionCollector exeptionCollector;

        /// <summary>
        /// local gui. Must be in all states etc
        /// </summary>
        private TGUI.Gui DevGUI;
        #endregion

        public void Dispose()
        {
            //unload all resources!
        }

        //ctor. init resources
        public Game(RenderWindow renderWindow,BindableInputDevice inputDevice, ExeptionCollector collector= null)
        {
            ////INIT
            ////SETUP
            ///init window and input. ex collector
            this.renderWindow = renderWindow;
            //rescaling
            renderWindow.Size = new Vector2u(1280,720);
            this.inputDevice = inputDevice;
            inputDevice.HistorySize = 5;
            exeptionCollector = collector;
            ///init main gui
            SetupDevGUI();
            ///setup some links
            BasicSetup();
            ///setup resizing
            renderWindow.Resized += (object sender, SizeEventArgs arg) => { DevGUI.View = (sender as RenderWindow).GetView(); };
            renderWindow.Resized += (object sender, SizeEventArgs arg) => { currentState.GraphicController.ResizeAll((sender as RenderWindow).GetView().Size); };


            GraphicManager.Instance.Provider = new FileGraphicProvider() { Link = "../../Application/.res/.textures", Filter = (string t)=> { return t.Contains(".jpg") || t.Contains(".png"); } };
            GraphicManager.Instance.Load();

            ChangeState(StateBuilder.StateID.MainMenu);
            //load resources
        }
        
        //main game loop
        public void Run()
        {
            Clock FPS_Clock = new Clock();
            Clock LogicClock = new Clock();

            ///while window open
            while (renderWindow.IsOpen)
            {
                try
                {
                    ///update myself
                    Update(FPS_Clock.Restart());
                    ///update events from window
                    renderWindow.DispatchEvents();
                    ///start displaying. Clear window
                    renderWindow.Clear();
                    ///Update state before draw
                    currentState.Update(LogicClock.Restart());
                    ///draw state
                    renderWindow.Draw(currentState);
                    ///if we have gui draw it
                    DevGUI?.Draw();
                    ///display resault
                    renderWindow.Display();
                }
                catch (Exception ex)
                {
                    exeptionCollector(ex);
                }
            }
            ///disposing timers
            FPS_Clock.Dispose();
            LogicClock.Dispose();
            ///dispose gui if have gui
            DevGUI?.Dispose();
        }

        private void ChangeState(StateBuilder.StateID newStateID) {
                if (currentState != null)
                    if (currentState.DontUnloadFromMemory) currentState?.HotSave();

            currentState = StateBuilder.GetState(newStateID);
            currentState.ChangeState += ChangeState;
        }

        /// <summary>
        /// Обновляет прослойку игры, которая оперирует состояниями
        /// </summary>
        /// <param name="time">время</param>
        private void Update(Time time) {
            if (lU.AsSeconds()>1)
            {
                (DevGUI.Get("FPS_COUNTER_Laybel") as TGUI.Label).Text = $"FPS:{Math.Round((1f / time.AsSeconds())).ToString()}";
                lU = new Time();
            }
            else
            {
                lU += time;
            }
                
        }


        #region Startup
        private void BasicSetup() { 
            renderWindow.SetTitle("Game: Tanks1990");
            inputDevice.KeyAdded += KeyInterpretator.GetInstance().RegisterSample;


        }

        private void SetupDevGUI()
        {
            DevGUI = new TGUI.Gui
            {
                Target = renderWindow
            };

            DevGUI.Add(new TGUI.Label(), "FPS_COUNTER_Laybel");
            DevGUI.Add(new TGUI.Label() {Position = new Vector2f(0,20) ,AutoSize = true }, "POSITION_Laybel"); ;

            renderWindow.MouseMoved += (object send, MouseMoveEventArgs pos) => { (DevGUI.Get("POSITION_Laybel") as TGUI.Label).Text = $"POS: X:{pos.X}|Y:{pos.Y}"; };



            inputDevice.AddKey(new BindibleKey("Pressed_Alt_Enter", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) => {  
                return arg.Code == Keyboard.Key.Enter &&
                history.ToList().Find(i=> { return i.Code == Keyboard.Key.LAlt; })!=null; 
            }, 
                
                () => {  DevGUI.GetWidgets().ForEach(i => i.Visible = !i.Visible); inputDevice.ClearHistory(); }));
        }

        #endregion
    }
}
