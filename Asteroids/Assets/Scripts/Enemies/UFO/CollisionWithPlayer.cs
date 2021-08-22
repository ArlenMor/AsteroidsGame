using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

namespace UFO
{
    public class CollisionWithPlayer : MonoBehaviour
    {
        public event System.Action<Player.Player> EventCollisionWithPlayer;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.tag.ToLower();

            if (transform.parent.name == "UFOBulletHolder" && tag == TagsCheck.Player)
            {
                EventCollisionWithPlayer?.Invoke(collision.gameObject.GetComponentInParent<Player.Player>());
            }
        }
    }

}
