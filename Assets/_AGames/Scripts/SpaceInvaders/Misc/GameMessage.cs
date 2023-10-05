using System;

namespace AGames.SpaceInvaders
{
    public static class GameMessage
    {
        // Game State Machine Command message
        public const int StateCommand = 0;//<GameStateCommand>

        // Input messages
        public static class Input
        {
            public const int Left = 50; //<void>
            public const int Right = 51; //<void>
            public const int Fire = 52; //<void>
        }

        // Gameplay messages
        public static class Gameplay
        {
            public const int EnemyKilled = 100; //<EnemyMono>
            public const int AllEnemiesKilled = 101; //<void>
            public const int EnemyLimit = 102; //<EnemyWaveLimiterMono.Type>
        }

        // UI messages
        public static class Ui
        {
            public const int ShowMainMenu = 200; //<void>
            public const int HideMainMenu = 201; //<void>
            public const int ShowResults = 202; //<void>
            public const int HideResults = 203; //<void>

            public const int PlayerLivesUpdated = 210; //<lives:int>
            public const int PlayerScoreUpdated = 211; //<lives:int>
            public const int EnemyWavesUpdated = 212; //<waves:int>
            public const int HighScoresUpdated = 213; //<highScores:List<ScoreEntry>>
            public const int InGamePopup = 214; //<messgage:string>
        }

        // Extensions
        public static void Post(this int message) => MessageKit.post(message);
        public static void Post<T>(this int message, T payload) => MessageKit<T>.post(message, payload);
        public static void Post<T,U>(this int message, T payload1, U payload2) => MessageKit<T,U>.post(message, payload1, payload2);
        public static void AddObserver(this int message, Action action) => MessageKit.addObserver(message, action);
        public static void AddObserver<T>(this int message, Action<T> action) => MessageKit<T>.addObserver(message, action);
        public static void AddObserver<T,U>(this int message, Action<T,U> action) => MessageKit<T,U>.addObserver(message, action);
        public static void RemoveObserver(this int message, Action action) => MessageKit.removeObserver(message, action);
        public static void RemoveObserver<T>(this int message, Action<T> action) => MessageKit<T>.removeObserver(message, action);
        public static void RemoveObserver<T, U>(this int message, Action<T, U> action) => MessageKit<T, U>.addObserver(message, action);

    }
}