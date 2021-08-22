using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using SpaceWrapScreen;
using Weapon;
public class DestroyBulletOffscreen : OffScreen
{
    public event Action EventDestroy;

    public override void Update()
    {
        base.Update();

        if (isOffScreen)
            if (EventDestroy != null)
                EventDestroy();
            else
                Destroy(gameObject);
    }
}
