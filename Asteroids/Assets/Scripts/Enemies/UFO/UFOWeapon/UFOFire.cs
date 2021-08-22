using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapon;

namespace UFOWeapon
{
    public class UFOFire : MonoBehaviour
    {
        [SerializeField]
        private float fireRate = 0.33f;

        [SerializeField]
        private Transform shootPointTransform;

        [SerializeField]
        private float LifeTimeBullet;

        [SerializeField]
        private Sprite spriteBullet;

        private Transform playerTarget;

        private ObjectPool weaponPool;
        private float nextFire = 2.0f;

        private void Start()
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            weaponPool = GetComponent<ObjectPool>();
            weaponPool.Init();
        }

        private void Update()
        {
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + fireRate;

                GameObject weapon = weaponPool.GetGameObject();
                weapon.transform.position = shootPointTransform.position;

                Vector3 dir = playerTarget.position - weapon.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                weapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                Bullet bullet = weapon.GetComponent<Bullet>();
                bullet.Init(weaponPool, 1, LifeTimeBullet, spriteBullet);
            }else if(Time.time > nextFire)
                nextFire = Time.time;
        }

        public void DestroyAllBullets()
        {
            weaponPool.ReleaseAll();
        }
    }
}

