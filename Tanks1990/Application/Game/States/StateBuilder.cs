using SFML.Graphics;
using System;
using System.Collections.Generic;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Application.Game.States
{
    static class StateBuilder
    {
        public enum StateID { MainMenu };

        static private Dictionary<StateID, IGameState> HotStates = new Dictionary<StateID, IGameState>();

        /// <summary>
        /// Jist return state by ID
        /// </summary>
        /// <param name="ID">State id</param>
        /// <returns>IGameState</returns>
        static public IGameState GetState(StateID ID) {
            IGameState stateToReturn = null;

            if (HotStates.TryGetValue(ID, out stateToReturn)) { stateToReturn.Load(); return stateToReturn; } 

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
