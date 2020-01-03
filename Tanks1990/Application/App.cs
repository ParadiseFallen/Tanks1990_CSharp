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
using Tanks1990.Application.Wraps;

namespace Tanks1990.Application
{
    /// <summary>
    /// Переименовать в App. Состояния будут кидать события а приложение обрабатывать, например SendMSG() для сервера, а если модуль сети не подключен то уведомить об этом
    /// </summary>
    class App : IDisposable
    {
        #region Data
        //window container
        private RenderWindowWrap windowContainer = new RenderWindowWrap();
        //input device
        private BindableInputDevice inputDevice;
        //current state of game
        private IGameState currentState;
        private View Camera;
        private Time lU = new Time();
        public delegate void ExeptionCollector(Exception ex);

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
        public App()
        {
            //создание окна
            windowContainer.ChangeWindow(new VideoMode(1280, 720), "Tanks 1990", Styles.Default);
            //устанавливаем ограничение фпс
            windowContainer.Window.SetFramerateLimit(60);
            //создаем клавиатуру
            inputDevice = new BindableInputDevice();
            //Подписываем клавиатуру на нажатия клавиш
            windowContainer.Subscribe((RenderWindow window) => { window.KeyPressed += inputDevice.Update; });
            //загрузка стандартной расскладки клавиатруы
            KeyInterpretator.Instance.LoadLayout(inputDevice);

            inputDevice.KeyAdded += KeyInterpretator.Instance.RegisterSample;

            /* private void BaseWindowLinks()
       {
           //registration func

           //linking
           //windowContainer.Resized += (object sender, SizeEventArgs arg) => { (sender as RenderWindow).SetView(new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height))); };
       }
            */



            ////INIT
            ////SETUP
            ///init window and input. ex collector
            //this.windowContainer = windowContainer;
            //Перемасштабируем все окно что бы текстуры были как надо
            windowContainer.Window.Size = new Vector2u(1280, 720);

            inputDevice.HistorySize = 5;
            Camera = windowContainer.Window.GetView();
            ///init main gui
            SetupDevGUI();
            ///setup resizing
            ///DONT RESCALE GUI. Rescale game entityes /////||||\\\\\ = new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height));
            windowContainer.Subscribe( (RenderWindow Window)=> { Window.Resized += (object sender, SizeEventArgs arg) => { Camera = new View(new Vector2f(arg.Width / 2f, arg.Height / 2f), new Vector2f(arg.Width, arg.Height)); }; });
            windowContainer.Subscribe( (RenderWindow Window)=> { Window.Resized += (object sender, SizeEventArgs arg) => { DevGUI.View = /*(sender as RenderWindow).GetView()*/Camera; }; });
            // windowContainer.Resized += (object sender, SizeEventArgs arg) => { currentState.GraphicController.ResizeAll((sender as RenderWindow).GetView().Size); };


            GraphicManager.Instance.Provider = new FileGraphicProvider() { Link = "../../Application/.res/.textures", Filter = (string t) => { return t.Contains(".jpg") || t.Contains(".png"); } };
            GraphicManager.Instance.Load();

            ChangeState(StateBuilder.StateID.MainMenu);
            //load resources
        }

        private void test() {
            Console.WriteLine(VideoMode.FullscreenModes.Length);
            foreach (var item in VideoMode.FullscreenModes)
            {
                Console.WriteLine(item);
            }
        }

        //main game loop
        public void Run()
        {
            test();


            Clock FPS_Clock = new Clock();
            Clock LogicClock = new Clock();
            // Sprite s = new Sprite(GraphicManager.Instance.Textures["GROUND.jpg"]);
            ///while window open
            while (windowContainer.Window.IsOpen)
            {
                try
                {
                    ///update myself
                    Update(FPS_Clock.Restart());
                    ///update events from window
                    windowContainer.Window.DispatchEvents();
                    ///start displaying. Clear window
                    windowContainer.Window.Clear();
                    ///Update state before draw
                    currentState.Update(LogicClock.Restart());
                    ///draw state
                    windowContainer.Window.Draw(currentState);
                    ////////////////////////////////////////////////////
                    //windowContainer.Draw(s);
                    ////////////////////////////////////////////////////
                    ///if we have gui draw it
                    DevGUI?.Draw();
                    ///display resault
                    windowContainer.Window.Display();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            ///disposing timers
            FPS_Clock.Dispose();
            LogicClock.Dispose();
            ///dispose gui if have gui
            DevGUI?.Dispose();
        }
        /// <summary>
        /// Изменить активное состояние
        /// </summary>
        /// <param name="newStateID">ID нового состояния</param>
        private void ChangeState(StateBuilder.StateID newStateID)
        {
            if (currentState != null)
                if (currentState.DontUnloadFromMemory) currentState?.Save();

            currentState = StateBuilder.GetState(newStateID);
            if (currentState.Initialized)
            {
                return;
            }
            currentState.Initialized = true;
            //windowContainer.Resized += (object sender, SizeEventArgs arg) => { currentState.GUI.View = Camera; };
            windowContainer.Subscribe((RenderWindow window)=> { currentState.GUI.Target = window; });
            currentState.GUI.Target = windowContainer.Window;
            currentState.ChangeState += ChangeState;
        }

        /// <summary>
        /// Обновляет прослойку игры, которая оперирует состояниями
        /// </summary>
        /// <param name="time">время</param>
        private void Update(Time time)
        {
            if (lU.AsSeconds() > 1)
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
        private void SetupDevGUI()
        {
            DevGUI = new TGUI.Gui();
            windowContainer.Subscribe((RenderWindow window)=> { DevGUI.Target = window; });

            DevGUI.Add(new TGUI.Label(), "FPS_COUNTER_Laybel");
            DevGUI.Add(new TGUI.Label() { Position = new Vector2f(0, 20), AutoSize = true }, "POSITION_Laybel"); ;

            windowContainer.Subscribe((RenderWindow Window)=> { Window.MouseMoved += (object send, MouseMoveEventArgs pos) => { (DevGUI.Get("POSITION_Laybel") as TGUI.Label).Text = $"POS: X:{pos.X}|Y:{pos.Y}"; }; });


            ///менбю разработчика
            inputDevice.AddKey(new BindibleKey("Pressed_Alt_Enter", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) =>
            {
                return arg.Code == Keyboard.Key.Enter &&
                history.ToList().Find(i => { return i.Code == Keyboard.Key.LAlt; }) != null;
            },
                () => { DevGUI.GetWidgets().ForEach(i => i.Visible = !i.Visible); inputDevice.ClearHistory(); }));



            inputDevice.AddKey(new BindibleKey("Pressed_S", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) => { return arg.Code == Keyboard.Key.S; }, () => { Camera.Move(new Vector2f(100, 100)); windowContainer.Window.SetView(Camera); }));

            ///для тестов пересоздания окна
            inputDevice.AddKey(new BindibleKey("Pressed_F1", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) => { return arg.Code == Keyboard.Key.F1; }, () =>
            {
                windowContainer.ChangeWindow(VideoMode.FullscreenModes[0], "Tanks 1990", Styles.Resize);
            }));

            inputDevice.AddKey(new BindibleKey("Pressed_F2", (object sender, Queue<KeyEventArgs> history, KeyEventArgs arg) => { return arg.Code == Keyboard.Key.F2; }, () =>
            {
                windowContainer.ChangeWindow(new VideoMode(1280, 720), "Tanks 1990", Styles.Default);
            }));
        }

        #endregion
    }
}
