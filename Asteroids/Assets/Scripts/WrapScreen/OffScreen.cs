using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceWrapScreen
{
    public class OffScreen : MonoBehaviour
    {
        [SerializeField]
        private float padding = 0.1f;

        protected bool isOffScreen;
        protected Vector3 viewportPos;

        private float top;
        private float bot;
        private float left;
        private float right;

        private void Awake()
        {
            top = 0f - padding;
            bot = 1f + padding;
            left = 0f - padding;
            right = 1f + padding;
        }

        public virtual void Update()
        {
            viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            isOffScreen = false;

            if(viewportPos.x < left)
            {
                viewportPos.x = right;
                isOffScreen = true;
            }else if (viewportPos.x > right)
            {
                viewportPos.x = left;
                isOffScreen = true;
            }

            if(viewportPos.y < top)
            {
                viewportPos.y = bot;
                isOffScreen = true;
            }else if (viewportPos.y > bot)
            {
                viewportPos.y = top;
                isOffScreen = true;
            }
        }
    }
}

