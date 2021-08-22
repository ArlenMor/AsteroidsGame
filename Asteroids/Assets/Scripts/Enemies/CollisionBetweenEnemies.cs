using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Meteors;

namespace CommonEnemies
{
    public class CollisionBetweenEnemies : MonoBehaviour
    {
        public event System.Action<Meteor> EventMeteorCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.tag.ToLower();

            if(tag == TagsCheck.Meteor)
            {
                Meteor meteor = collision.gameObject.GetComponentInParent<Meteor>();

                EventMeteorCollision?.Invoke(meteor);
            }
        }
    }
}
