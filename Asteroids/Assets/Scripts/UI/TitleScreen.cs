using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using GameController;

namespace UI
{
    public class TitleScreen : MonoBehaviour
    {
        public event System.Action EventCreateNewGame;
        public event System.Action EventChangeControl;

        [SerializeField]
        private Button _buttonStartNewGame;
        [SerializeField]
        private Button _control;
        [SerializeField]
        private Button _exit;

        private bool isComplete;

        private void Awake()
        {
            _buttonStartNewGame.gameObject.SetActive(false);
            _control.gameObject.SetActive(false);
            _exit.gameObject.SetActive(false);

            _buttonStartNewGame.onClick.AddListener(OnStartNewGame);
            _control.onClick.AddListener(OnSwitchControl);
            _exit.onClick.AddListener(OnExit);
        }

        private void OnEnable()
        {
            isComplete = false;


            _buttonStartNewGame.gameObject.SetActive(true);
            _control.gameObject.SetActive(true);
            _exit.gameObject.SetActive(true);

            
        }

        public void ChangeControlText(string text)
        {
            _control.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

        private void OnStartNewGame()
        {
            if(!isComplete)
                CreateNewGame();
        }
        
        private void OnContinuePlay()
        {
            if (!isComplete)
                CreateNewGame();
        }

        private void OnSwitchControl()
        {
            EventChangeControl?.Invoke();
        }

        private void OnExit()
        {
            Application.Quit();
        }

        private void CreateNewGame()
        {
            isComplete = true;

            EventCreateNewGame?.Invoke();
        }

        
    }

}
