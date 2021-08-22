using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public event System.Action EventNewGame;
        public event System.Action EventChangeControl;

        public event System.Action EventPausedPlay;
        public event System.Action EventUnpausedPlay;

        [SerializeField]
        private GameScreen gameScreen;

        [SerializeField]
        private TitleScreen titleScreen;

        [SerializeField]
        private ScoreScreen scoreScreen;

        private enum state{
            TITLESCREEN,
            GAMESCREEN,
            SCORESCREEN
        };

        private state _currentState;


        private GameObject current;

        private bool isFirstGame = true;

        private void Awake()
        {
            titleScreen.EventCreateNewGame += OnCreateNewGame;
            titleScreen.EventChangeControl += OnChangeControl;
            scoreScreen.EventComplete += OnScoreScreenComplete;

            _currentState = state.TITLESCREEN;
        }

        private void Update()
        {
            if(_currentState == state.GAMESCREEN)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _currentState = state.TITLESCREEN;
                    ShowTitleScreen();
                    EventPausedPlay();
                }
            }else if(_currentState == state.TITLESCREEN)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    _currentState = state.GAMESCREEN;
                    HideTitleScreen();
                    EventUnpausedPlay();
                }
            }
            
        }

        public void CreateTitleScreen(bool firstTime = false)
        {
            if(firstTime)
            {
                current = titleScreen.gameObject;
                current.SetActive(true);
            }
            else
                TransitionScreen(titleScreen.gameObject);
        }

        public void ShowTitleScreen()
        {
            titleScreen.gameObject.SetActive(true);
        }

        public void HideTitleScreen()
        {
            titleScreen.gameObject.SetActive(false);
        }

        public void ShowGameScreen(bool continuePlay = false)
        {
            /*if(continuePlay)
            {
                current = titleScreen.gameObject;
                current.SetActive(true);
            }else*/
                TransitionScreen(gameScreen.gameObject);

        }

        public void ShowScoreScreen()
        {
            TransitionScreen(scoreScreen.gameObject);
        }

        public void UpdatePoints(int points)
        {
            gameScreen.UpdatePoints(points);
        }

        public void UpdateLives(int lives)
        {
            gameScreen.UpdateLives(lives);
        }

        public void ShowAllLivesInGameScreen()
        {
            gameScreen.ShowAllLives();
        }

        public void ChangeTextInTitleScreen(string text)
        {
            titleScreen.ChangeControlText(text);
        }
        
        private void TransitionScreen(GameObject screen)
        {
            screen.SetActive(true);

            if (current != null)
                current.SetActive(false);

            current = screen;
        }


        private void OnCreateNewGame()
        {
            EventNewGame();
            _currentState = state.GAMESCREEN;

            if (isFirstGame)
            {
                ShowGameScreen();
                isFirstGame = false;
            }
            else
            {
                HideTitleScreen();
                EventUnpausedPlay();
            }
        }

        private void OnChangeControl() => EventChangeControl();

        private void OnScoreScreenComplete()
        {
            isFirstGame = true;
            TransitionScreen(titleScreen.gameObject);
            
        }
    }

}
