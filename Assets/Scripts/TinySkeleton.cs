using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinySkeleton : Enemy
{

    // Update is called once per frame
    void FixedUpdate()
    {
        takeDamage(1);
        headTowards();
    }
}
