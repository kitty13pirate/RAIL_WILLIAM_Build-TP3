using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralHand : Sortilege
{
    public Collider handHitbox;
    public int spectralHandDamage;
    // Start is called before the first frame update
    void Start()
    {
        // Un workaround puisque la main est 200 fois plus petite qu'elle ne le devrait etre ce qui cause du trouble a la fonction
        // hitboxCollision qui utilise localScale
        StartCoroutine(disappear(1f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitboxCollision(handHitbox, 2, spectralHandDamage);
    }

    public override IEnumerator disappear(float time)
    {
        // La main apparait
        yield return new WaitForSeconds(time);
        // Ensuite elle disparait
        Destroy(gameObject);
    }
}
