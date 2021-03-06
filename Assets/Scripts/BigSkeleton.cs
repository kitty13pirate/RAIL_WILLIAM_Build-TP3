using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSkeleton : Enemy
{
    public Object tinySkeleton;

    // Pour avancer vers le joueur
    void FixedUpdate()
    {
        headTowards();
    }

    // Lorsqu'il meurt, il fait apparaitre 3 petits squelettes autour de lui
    public override void die()
    {
        base.die();
        for (int i = 0; i < 3; i++)
        {
            float x = Random.Range(transform.position.x - 1f, transform.position.x + 1f);
            float z = Random.Range(transform.position.z - 1f, transform.position.z + 1f);

            Vector3 newPosition = new Vector3(x, 0f, z);

            object revengeSkeleton = Instantiate(tinySkeleton, newPosition, transform.rotation);
        }
    }
}
