using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Meteors;


namespace Player
{
    public class Player : MonoBehaviour
    {
        public event System.Action EventDie;

        private PlayerController playerController;
        private CollisionWithEnemy collisionWithEnemy;

        private PlayerShield shield;
        private PlayerDeath death;
        private Weapon.Fire fireConponent;
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();

            shield = GetComponent<PlayerShield>();
            death = GetComponent<PlayerDeath>();

            fireConponent = GetComponent<Weapon.Fire>();

            death.EventDie += OnEventDie;

            collisionWithEnemy = GetComponent<CollisionWithEnemy>();
            collisionWithEnemy.EventMeteorCollision += OnCollisionWithMeteor;

            collisionWithEnemy.EventUFOCollision += OnCollisionWithUFO;
        }
        
        public void Spawn()
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(true);

            playerController.Reset();
            fireConponent.DestroyAllBullets();

            shield.ActivateShield();
        }

        public void OnCollisionWithBullet()
        {
            Die();
        }


        private void OnCollisionWithMeteor(Meteor meteor)
        {
            meteor.Collision(int.MaxValue);

            Die();
        }

        private void OnCollisionWithUFO(UFO.UFO ufo)
        {
            ufo.Collision();

            Die();
        }

        private void Die()
        {
            if (!shield.IsInvinceble)
            {
                playerController.Reset();
                death.Die();
                gameObject.SetActive(false);
            }
        }

        private void OnEventDie()
        {
            if (EventDie != null)
                EventDie();
        }

        
    }
}


