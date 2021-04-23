using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBurst : Sortilege
{
    public ParticleSystem psWind;
    public Collider burstHitbox;
    public int windBurstDamage;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        psWind.Emit(300);
        Explosion(force, transform.position, burstHitbox.transform.localScale.x / 2, 0.05f);
        hitboxCollision(burstHitbox, 1, windBurstDamage);
        StartCoroutine(disappear(2f));
    }

    void Explosion(float force, Vector3 position, float radius, float michaelBay)
    {
        //Recuperer tous les colliders a proximite
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        //Regarde pour trouver les objets physiques
        foreach (Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb != null && rb.CompareTag("Enemy"))
            {
                //Appliquer un velocite aux objets
                rb.AddExplosionForce(force, position, radius, michaelBay, ForceMode.Impulse);
            }
        }
    }
}
