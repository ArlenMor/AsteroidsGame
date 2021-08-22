using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 1.0f;

        private Transform targetPlayer;
        public Vector3 target;

        private Bullet bullet;

        private Vector3 direction;
        private Vector3 heading;

        private void Awake()
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            bullet = GetComponent<Bullet>();
            bullet.InitBullet += OnInitBullet;
        }

        private void Update()
        {
            if(transform.parent.gameObject.tag == "ufo")
            {
                transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
            }
            
        }

        private void OnInitBullet()
        {
            heading = targetPlayer.position - transform.position;
            var distance = heading.magnitude;
            direction = heading / distance;
        }
    }

}
