using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinySkeleton : Enemy
{

    // Update is called once per frame
    void FixedUpdate()
    {
        // Le squelette prend 1 degat a chaque frame du fixed update, soit 50 degat par secondes
        takeDamage(1);
        headTowards();
    }
}
