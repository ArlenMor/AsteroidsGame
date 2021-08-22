using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        private float fireRate = 0.33f;

        [SerializeField]
        private Transform shootPointTransform;

        [SerializeField]
        private float LifeTimeBullet;

        private ObjectPool weaponPool;
        private float nextFire = 0.0f;

        private bool onlyKey;

        private string shootingMode; //can be "OnlyKey" and "KeyAndMouse"

        public bool OnlyKey { get => onlyKey; set => onlyKey = value; }

        private void Awake()
        {
            weaponPool = GetComponent<ObjectPool>();
            weaponPool.Init();
        }

        private void Update()
        {
            if (onlyKey)
                shootingMode = "OnlyKey";
            else
                shootingMode = "KeyAndMouse";

            if (Input.GetButton(shootingMode))
            {
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + fireRate;

                    GameObject weapon = weaponPool.GetGameObject();
                    weapon.transform.position = shootPointTransform.position;
                    weapon.transform.rotation = transform.rotation;

                    Bullet bullet = weapon.GetComponent<Bullet>();
                    bullet.Init(weaponPool, 1, LifeTimeBullet);


                }
            }else
            {
                if(Time.time > nextFire)
                    nextFire = Time.time;
            }
        }

        public void DestroyAllBullets()
        {
            weaponPool.ReleaseAll();
        }
    }
}

