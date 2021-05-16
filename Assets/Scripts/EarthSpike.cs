using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : Sortilege
{
    private Vector3 destination;
    public ParticleSystem psDust;
    public LayerMask mousePointMask;

    // Les 3 hitboxes pour le degat
    public Collider spikeHitbox1;
    public Collider spikeHitbox2;
    public Collider spikeHitbox3;
    private List<Collider> colliders;

    // Le degat
    public int spikeDamage;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        // Je rentre les colliders dans la liste
        colliders = new List<Collider> { spikeHitbox1, spikeHitbox2, spikeHitbox3 };

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the mousePointMask layer...
        // Le layermask mousePoint, est utilise pour detecter la position de la souris
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, mousePointMask))
        {
            destination = floorHit.point;
            transform.parent.position = destination;
        }
    }

    // La fonction pour endommager les enemies est appelee durant l'animation, a trois reprises
    public void doDamage(int number)
    {
        psDust.Emit(400);
        audioSource.PlayOneShot(audioSource.clip);
        hitboxCollision(colliders[number-1], 2, spikeDamage * number);
    }

    // Cette fonction est appelee dans l'animation EarthSpikeErupt pour faire disparaitre le pique
    public void disappear()
    {
        Destroy(transform.parent.gameObject);
    }
}
