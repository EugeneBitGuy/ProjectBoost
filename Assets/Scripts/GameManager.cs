using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public UIManager uiManager;

        private int _gameManagerLevelSp = 0;
        private int _gameManagerGameSp = 0;
        public static int LevelSp
        {
            get => Instance._gameManagerLevelSp;
            set => Instance._gameManagerLevelSp = value < 0 ? 0 : value;
        }

        public static int GameSp
        {
            get => Instance._gameManagerGameSp;
            private set => Instance._gameManagerGameSp = value < 0 ? 0 : value;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);
        }

        private void Start()
        {
            HandleStartUp();
        }

        private void HandleStartUp()
        {
            if (PlayerPrefs.HasKey("GameSp"))
                GameSp = PlayerPrefs.GetInt("GameSp");
            if (PlayerPrefs.HasKey("LevelSp"))
                LevelSp = PlayerPrefs.GetInt("LevelSp");
            
            uiManager.UpdateUI();
        }

        private void OnApplicationQuit()
        {
            SaveGameSp();
            ResetSp();
        }

        public static void AddSp(int spAmount)
        {
            LevelSp += spAmount;
            
            Instance.uiManager.UpdateUI();
        }

        private static void ResetSp()
        {
            PlayerPrefs.SetInt("LevelSp", 0);
            LevelSp = 0;
        }

        public void LevelCleared()
        {
            SaveGameSp();
            
            //todo UI show clear level
            
        }

        private void SaveGameSp()
        {
            GameSp += LevelSp;
            
            PlayerPrefs.SetInt("GameSp", GameSp);
        }

        public void RestartLevel()
        {
            ResetSp();
            
            //todo UI show restart options
        }
        
        
    }
}