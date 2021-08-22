using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GameController;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private GameObject[] lives;

        private void OnEnable()
        {
            ShowAllLives();
            //GameManager.Instance.StartGame();
        }

        public void ShowAllLives()
        {
            for (int i = 0; i < lives.Length; i++)
                lives[i].SetActive(true);
        }

        public void UpdatePoints(int points)
        {
            scoreText.text = points.ToString();
        }

        public void UpdateLives(int lives)
        {
            for (int i = 0; i < this.lives.Length; i++)
                if (i >= lives)
                    this.lives[i].SetActive(false);
        }


    }

}
