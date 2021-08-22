using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CommonEnemies
{
    public class Health : MonoBehaviour
    {
        public event Action<int> EventHealthChange;

        [SerializeField]
        private int health = 1;
        public int healthValue { get => health; set => health = value; }

        public void IncreaseHealth(int value)
        {
            healthValue += value;
            //DispatchChangedEvent();
        }

        public void ReduceHealth(int value)
        {
            healthValue -= value;
            //DispatchChangedEvent();
        }

        private void DispatchChangedEvent()
        {
            if (EventHealthChange != null)
                EventHealthChange(healthValue);
        }
    }

}
