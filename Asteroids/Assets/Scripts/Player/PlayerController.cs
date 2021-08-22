using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Parametrs")]
        [SerializeField]
        private float rotationSpeed = 250.0f;
        [SerializeField]
        private float maxVelocity = 5.0f;
        [SerializeField]
        private float acceleration = 5.0f;

        private Vector3 velocity = new Vector3();
        private Vector3 clampedVelocity = new Vector3();

        private bool onlyKey;

        public bool OnlyKey { get => onlyKey; set => onlyKey = value; }

        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            if(onlyKey)
            {
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);

                transform.Rotate(new Vector3(0, 0, -inputX), rotationSpeed * Time.deltaTime);

                velocity += (inputY * (transform.up * acceleration)) * Time.deltaTime;

                clampedVelocity = Vector3.ClampMagnitude(velocity, maxVelocity);
                transform.Translate(clampedVelocity * Time.deltaTime, Space.World);
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = Vector2.Angle(Vector2.up, mousePosition - transform.position);
                float inputY = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.x < mousePosition.x ? -angle : angle), rotationSpeed * Time.deltaTime);

                velocity += (inputY * (transform.up * acceleration)) * Time.deltaTime;

                clampedVelocity = Vector3.ClampMagnitude(velocity, maxVelocity);
                transform.Translate(clampedVelocity * Time.deltaTime, Space.World);
            }
            

        }


        public void Reset()
        {
            velocity = Vector3.zero;
            clampedVelocity = Vector3.zero;
        }
    }

}
