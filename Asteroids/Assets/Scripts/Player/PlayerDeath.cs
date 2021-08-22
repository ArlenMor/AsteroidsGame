using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public event System.Action EventDie;

        [SerializeField]
        private float duration = 1f;
        public void Die()
        {
            Invoke(nameof(DieComplete), duration);
        }

        private void DieComplete()
        {
            if (EventDie != null)
                EventDie();
        }
    }
}

