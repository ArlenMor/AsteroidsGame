using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

using CommonEnemies;

namespace Meteors
{
    public class Meteor : MonoBehaviour
    {
        public event Action<Meteor, int, Vector3, GameObject[]> EventDie;

        [SerializeField]
        private GameObject[] meteors;

        [SerializeField]
        private int pointsForDestroy;

        [SerializeField]
        private GameObject[] childMeteors;

        private Health health;

        private void Awake()
        {
            ChooseMeteor();
            health = GetComponent<Health>();
        }

        public void Collision(int damage = 1)
        {
            health.ReduceHealth(damage);

            if(health.healthValue <= 0)
            {
                if (EventDie != null)
                    EventDie(this, pointsForDestroy, transform.position, childMeteors);
                Destroy(gameObject);
            }
            
        }


        private void ChooseMeteor()
        {
            for(int i = 0; i < meteors.Length; i++)
            {
                GameObject meteor = meteors[i];
                meteor.SetActive(false);
            }

            GameObject chooseMeteor = meteors[UnityEngine.Random.Range(0, meteors.Length)];
            chooseMeteor.SetActive(true);
        }
    }

}
