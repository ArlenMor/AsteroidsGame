using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonEnemies;
using Meteors;

namespace UFO
{
    public class UFO : MonoBehaviour
    {
        public event System.Action<UFO, int> EventDie;

        [SerializeField]
        private int pointsForDestroy;

        [Header("SpawnTimeBetween")]
        [SerializeField]
        private float firstSpawnNumber;
        [SerializeField]
        private float secondSpawnNumber;

        private CollisionBetweenEnemies collisionBetweenEnemies;

        private UFOWeapon.UFOFire fireComponent;


        private Health health;

        private float timeSpawn;

        private void Awake()
        {
            health = GetComponent<Health>();

            fireComponent = GetComponent<UFOWeapon.UFOFire>();

            collisionBetweenEnemies = GetComponent<CollisionBetweenEnemies>();
            collisionBetweenEnemies.EventMeteorCollision += OnMeteorCollision;

            timeSpawn = Random.Range(firstSpawnNumber, secondSpawnNumber);
        }

        public void Collision(int damage = 1)
        {
            health.ReduceHealth(damage);

            if (health.healthValue <= 0)
            {
                if (EventDie != null)
                    EventDie(this, pointsForDestroy);
                Destroy(gameObject);
            }
        }

        public void DestroyAllUFOBullets()
        {
            fireComponent.DestroyAllBullets();
        }

        private void OnMeteorCollision(Meteor meteor)
        {
            meteor.Collision(int.MaxValue);

            Collision();
        }
    }

}
