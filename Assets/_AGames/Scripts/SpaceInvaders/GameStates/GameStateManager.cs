using AGames.StateMachine;
using System;
using Zenject;

namespace AGames.SpaceInvaders
{
    public enum GameStateCommand
    {
        // the enums has the same integers as the trigger parameters
        InitializationCompleted = 0,
        StartGameSession = 1,
        GoToMainMenu = 2,
        GameOver = 3,
        WinGame = 4
    }

    public class GameStateManager : SimpleStateMachine, IInitializable, IDisposable
    {
        // trigger parameter IDs
        private const int INITIALIZATION_COMPLETED_TRIGGER_PARAM = 0;
        private const int START_GAME_TRIGGER_PARAM = 1;
        private const int GO_TO_MAIN_MENU_TRIGGER_PARAM = 2;
        private const int GAME_OVER_TRIGGER_PARAM = 3;

        public enum GameState
        {
            Init, MainMenu, InGame, GameOver, WinGame
        }

        public GameStateManager (InitGameState initGameState, 
                                    MainMenuGameState mainMenuGameState, 
                                    InGameState inGameState, 
                                    GameOverState gameOverState)
        {
            // define state machine
            AddState(GameState.Init, initGameState);
            AddState(GameState.MainMenu, mainMenuGameState);
            AddState(GameState.InGame, inGameState);
            AddState(GameState.GameOver, gameOverState);

            // define transitions
            AddTransition(GameState.Init, GameState.MainMenu, new TriggerCondition(INITIALIZATION_COMPLETED_TRIGGER_PARAM));
            AddTransition(GameState.MainMenu, GameState.InGame, new TriggerCondition(START_GAME_TRIGGER_PARAM));
            AddTransition(GameState.InGame, GameState.GameOver, new TriggerCondition(GAME_OVER_TRIGGER_PARAM));
                // to main menu
            AddTransition(GameState.InGame, GameState.MainMenu, new TriggerCondition(GO_TO_MAIN_MENU_TRIGGER_PARAM));
            AddTransition(GameState.GameOver, GameState.MainMenu, new TriggerCondition(GO_TO_MAIN_MENU_TRIGGER_PARAM));
            AddTransition(GameState.WinGame, GameState.MainMenu, new TriggerCondition(GO_TO_MAIN_MENU_TRIGGER_PARAM));

            // register trigger parameters
            RegisterTriggerParameter(INITIALIZATION_COMPLETED_TRIGGER_PARAM);
            RegisterTriggerParameter(START_GAME_TRIGGER_PARAM);
            RegisterTriggerParameter(GO_TO_MAIN_MENU_TRIGGER_PARAM);
            RegisterTriggerParameter(GAME_OVER_TRIGGER_PARAM);
        }

        public void Initialize()
        {
            // observe 
            GameMessage.StateCommand.AddObserver<GameStateCommand>(OnGameStateCommandReceived);
            // start state machine
            StartStateMachine();
        }

        public void Dispose()
        {
            GameMessage.StateCommand.RemoveObserver<GameStateCommand>(OnGameStateCommandReceived);
        }

        private void OnGameStateCommandReceived(GameStateCommand command) => SetTrigger((int)command);
        
    }
}