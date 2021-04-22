using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : Sortilege
{
    private Vector3 destination;
    public ParticleSystem psDust;
    public LayerMask mousePointMask;

    // Start is called before the first frame update
    void Start()
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
            transform.parent.position = destination;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    // La fonction pour endommager les enemies
    public void doDamage()
    {
        psDust.Emit(400);
        Debug.Log("DAMAGE");
    }

    // Cette fonction est appelee dans l'animation EarthSpikeErupt
    public void disappear()
    {
        Destroy(transform.parent.gameObject);
    }
}
