using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 destination;
    public float speed;
    public ParticleSystem psExplosion;
    private bool explosion = false;

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
