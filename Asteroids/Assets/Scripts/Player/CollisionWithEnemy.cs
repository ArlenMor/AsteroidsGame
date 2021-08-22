using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Meteors;
using UFO;

namespace Player
{
    public class CollisionWithEnemy : MonoBehaviour
    {
        public event Action<Meteor> EventMeteorCollision;

        public event Action<UFO.UFO> EventUFOCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.tag.ToLower();

            if(tag == TagsCheck.Meteor)
            {
                Meteor meteor = collision.gameObject.GetComponentInParent<Meteor>();

                EventMeteorCollision?.Invoke(meteor);
            }

            if(tag == TagsCheck.Ufo)
            {
                if(transform.parent.name != "UFOBulletHolder")
                {
                    UFO.UFO ufo = collision.gameObject.GetComponent<UFO.UFO>();

                    EventUFOCollision?.Invoke(ufo);
                }
                
            }
        }
    }

}
