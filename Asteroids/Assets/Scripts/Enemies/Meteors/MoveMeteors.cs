using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteors
{
    public class MoveMeteors : MonoBehaviour
    {
        [SerializeField]
        private float meteorSpeed = 1f;

        private void Update()
        {
            transform.Translate(transform.up * meteorSpeed * Time.deltaTime, Space.World);
        }
    }

}
