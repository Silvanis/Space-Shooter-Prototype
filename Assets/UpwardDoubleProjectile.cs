using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardDoubleProjectile : ProjectileBase
{

    override protected void Start()
    {
        transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

}
