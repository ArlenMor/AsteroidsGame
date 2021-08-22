using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Meteors;
using UFO;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        public event System.Action InitBullet;

        private ObjectPool bulletPool;
        private int damage;

        private CollisionWithEnemy collisionWithEnemy;

        private CollisionWithPlayer collisionWithPlayer;

        private void Awake()
        {
            /*destroyBulletOffscreen = GetComponent<DestroyBulletOffscreen>();
            destroyBulletOffscreen.EventDestroy += OnEventDestroy;*/

            collisionWithEnemy = GetComponent<CollisionWithEnemy>();
            collisionWithEnemy.EventMeteorCollision += OnCollisionWithMeteor;

            collisionWithPlayer = GetComponent<CollisionWithPlayer>();
            collisionWithPlayer.EventCollisionWithPlayer += OnCollisionWithPlayer;

            collisionWithEnemy.EventUFOCollision += OnCollisionWithUFO;

            
        }

        public void Init(ObjectPool pool, int damageValue, float _lifeTimeBullet, Sprite _bulletSprite = null)
        {
            bulletPool = pool;
            damage = damageValue;
            Invoke("BulletDestroy", _lifeTimeBullet);
            InitBullet();
            if (_bulletSprite != null)
                GetComponentInChildren<SpriteRenderer>().sprite = _bulletSprite;
        }

        private void BulletDestroy()
        {
            if (bulletPool)
                bulletPool.ReleaseObject(gameObject);     
            else
                Destroy(gameObject);
        }

        private void OnCollisionWithMeteor(Meteor meteor)
        {
            meteor.Collision(damage);
            BulletDestroy();
        }

        private void OnCollisionWithPlayer(Player.Player player)
        {
            player.OnCollisionWithBullet();
            BulletDestroy();
        }

        private void OnCollisionWithUFO(UFO.UFO ufo)
        {
            ufo.Collision();
            BulletDestroy();
        }
    }
}
