using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceWrapScreen
{
    public class WrapScreen : OffScreen
{
        public override void Update()
        {
            base.Update();

            if (isOffScreen)
                transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
        }
    }

}
