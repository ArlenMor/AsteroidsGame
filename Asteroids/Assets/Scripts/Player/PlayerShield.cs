using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShield : MonoBehaviour
    {
        [SerializeField]
        private GameObject shield;

        [SerializeField]
        private float shieldDuration = 3.0f;

        private bool isInvinceble;

        public bool IsInvinceble { get => isInvinceble; set => isInvinceble = value; }

        public void ActivateShield()
        {
            shield.SetActive(true);
            SetInvincible(true);

            CancelInvoke("DisableGameObject");
            Invoke("DisableShield", shieldDuration);
        }

        private void SetInvincible(bool value)
        {
            IsInvinceble = value;
        }

        private void DisableShield()
        {
            SetInvincible(false);
            Invoke("DisableGameObject", 1f);
        }

        private void DisableGameObject()
        {
            shield.SetActive(false);
        }
    }
}
