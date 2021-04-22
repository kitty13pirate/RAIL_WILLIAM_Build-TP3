using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Sortilege
{
    private Vector3 destination;
    public float speed;
    public ParticleSystem psExplosion;
    private bool explosion = false;
    public LayerMask mousePointMask;
    public Collider trailHitbox;
    public Collider explosionHitbox;
    public int trailDamage;
    public int explosionDamage;

    // Start
    private void Start()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the mousePointMask layer...
        // Le 10 est le numero du layermask mousePoint, utilise pour detecter la position de la souris
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, mousePointMask))
        {
            destination = floorHit.point;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // La boule de feu se dirige vers sa destination
        transform.position = Vector3.MoveTowards(transform.position, destination, speed);
        // La firetrail fait du degat aux enemies qu'elle touche tant que la boule de feu n'a pas encore explose
        if (explosion != true)
        {
            hitboxCollision(trailHitbox, 1, trailDamage);
        }
        
        // Une fois arrivee a destination, la boule de feu explose
        if (transform.position == destination && !explosion)
        {
            psExplosion.Emit(50);
            // La boule de feu ne fait du degat qu'une seule fois, lorsqu'elle explose
            if (explosion != true)
            {
                hitboxCollision(explosionHitbox, 1, explosionDamage);
                explosion = true;
            }
            StartCoroutine(disappear(0.65f));
        }
    }


}
