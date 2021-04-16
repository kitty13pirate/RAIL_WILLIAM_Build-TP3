using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector3 destination;
    public float speed;
    public ParticleSystem psExplosion;
    private bool explosion = false;

    // Start
    private void Start()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the mousePointMask layer...
        // Le 10 est le numero du layermask mousePoint, utilise pour detecter la position de la souris
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, 10))
        {
            destination = floorHit.point;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // La boule de feu se dirige vers sa destination
        transform.position = Vector3.MoveTowards(transform.position, destination, speed);
        
        // Une fois arrivee a destination, la boule de feu explose
        if (transform.position == destination && !explosion)
        {
            psExplosion.Emit(50);
            StartCoroutine(disappear());
        }
    }

    // Pour faire disparaitre la FireBall
    public IEnumerator disappear()
    {
        // La FireBall explose
        explosion = true;
        yield return new WaitForSeconds(1f);
        // Ensuite elle disparait
        Destroy(gameObject);
    }
}
