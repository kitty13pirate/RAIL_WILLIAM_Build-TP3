using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : Sortilege
{
    private Vector3 destination;
    public ParticleSystem psDust;
    public LayerMask mousePointMask;
    public Collider spikeHitbox1;
    public Collider spikeHitbox2;
    public Collider spikeHitbox3;
    public int spikeDamage;
    private List<Collider> colliders;

    // Start is called before the first frame update
    void Start()
    {
        // Je rentre les colliders dans la liste
        colliders = new List<Collider> { spikeHitbox1, spikeHitbox2, spikeHitbox3 };

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the mousePointMask layer...
        // Le 10 est le numero du layermask mousePoint, utilise pour detecter la position de la souris
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, mousePointMask))
        {
            destination = floorHit.point;
            transform.parent.position = destination;
        }
    }

    // La fonction pour endommager les enemies est appelee durant l'animation
    public void doDamage(int number)
    {
        psDust.Emit(400);
        hitboxCollision(colliders[number-1], 2, spikeDamage * number);
    }

    // Cette fonction est appelee dans l'animation EarthSpikeErupt
    public void disappear()
    {
        Destroy(transform.parent.gameObject);
    }
}
