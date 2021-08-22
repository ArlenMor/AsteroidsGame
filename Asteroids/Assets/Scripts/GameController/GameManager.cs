using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UI;

namespace GameController
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get { return instance; }
        }

        [SerializeField]
        private LevelController levelController;
        [SerializeField]
        private UIManager uiManager;

        [SerializeField]
        private GameObject gameHolder;

        private Player.PlayerController playerController;
        private Weapon.Fire playerFire;

        [SerializeField]
        private int startingLives = 3;

        private enum Control
        {
            KeyPlusMouse,
            Key
        };

        private Control _currentControl;

        public int Lives
        {
            get;
            private set;
        }

        public int Points
        {
            get;
            private set;
        }

        public int Level
        {
            get { return levelController.Level; }
        }
        private void Awake()
        {
            #region - Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            #endregion

            gameHolder.SetActive(false);

            playerController = gameHolder.transform.GetComponentInChildren<Player.PlayerController>();
            playerFire = gameHolder.transform.GetComponentInChildren<Weapon.Fire>();
            playerFire.OnlyKey = false;
            playerController.OnlyKey = false;

            _currentControl = Control.KeyPlusMouse;

            levelController.EventPoints += OnLevelPoints;
            levelController.EventPlayerDied += OnLevelLives;

            uiManager.EventPausedPlay += OnPausedPlay;
            uiManager.EventUnpausedPlay += OnUnpausedPlay;
            uiManager.EventNewGame += OnNewGame;
            uiManager.EventChangeControl += OnChangeControl;
        }

        private void Start()
        {
            uiManager.CreateTitleScreen(true);
        }

        public void ResetGame()
        {
            Points = 0;
            Lives = startingLives;
            levelController.Reset();

            uiManager.ShowAllLivesInGameScreen();
            uiManager.UpdateLives(Lives);
            uiManager.UpdatePoints(Points);
        }
        public void StartGame()
        {
            gameHolder.SetActive(true);

            ResetGame();

            levelController.SpawnPlayer();
            levelController.StartLevel();
        }

        private void OnLevelPoints(int points)
        {
            Points += points;
            uiManager.UpdatePoints(Points);
        }

        private void OnLevelLives()
        {
            Lives -= 1;
            if(Lives >= 0)
            {
                uiManager.UpdateLives(Lives);
                levelController.SpawnPlayer();
            }else
            {
                uiManager.ShowScoreScreen();
            }
        }

        private void OnPausedPlay()
        {
            Time.timeScale = 0;
        }

        private void OnUnpausedPlay()
        {
            Time.timeScale = 1;
        }

        private void OnNewGame()
        {
            StartGame();
        }

        private void OnChangeControl()
        {
            if(_currentControl == Control.KeyPlusMouse)
            {
                playerController.OnlyKey = true;
                playerFire.OnlyKey = true;
                _currentControl = Control.Key;
                uiManager.ChangeTextInTitleScreen("Key");
            }
            else
            {
                playerController.OnlyKey = false;
                playerFire.OnlyKey = false;
                _currentControl = Control.KeyPlusMouse;
                uiManager.ChangeTextInTitleScreen("Mouse + Key");
            }
            
        }
    }
}

