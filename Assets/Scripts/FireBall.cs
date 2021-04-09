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
    void Update()
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

    public IEnumerator disappear()
    {
        explosion = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
