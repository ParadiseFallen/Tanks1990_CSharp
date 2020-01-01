using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Game.States
{
    class StateBuilder
    {
        public enum StateID { MainMenu };

        static private Dictionary<StateID, IGameState> HotStates = new Dictionary<StateID, IGameState>();

        static public IGameState GetState(StateID ID) {
            IGameState stateToReturn = null;

            if (HotStates.TryGetValue(ID,out stateToReturn)) return stateToReturn;

            switch (ID)
            {
                case StateID.MainMenu:
                    stateToReturn = new MainMenuState();
                    break;
                default:
                    throw new Exception("Wrong ID!");
            }

            if (stateToReturn.DontUnloadFromMemory)
                    HotStates.Add(ID, stateToReturn);

            return stateToReturn;
        }

    }
}
