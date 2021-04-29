using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJet : Sortilege
{
    public ParticleSystem psWater;
    public bool isOn = true;
    public Collider jetHitbox;
    public int jetDamage;
    // Start is called before the first frame update
    void Start()
    {
        // Le timer debute
        StartCoroutine(disappear(1.5f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOn)
        {
            hitboxCollision(jetHitbox, 2, jetDamage);
        }
    }

    // Pour faire disparaitre le WaterJet
    public override IEnumerator disappear(float time)
    {
        // Le WaterJet dure 2 secondes
        yield return new WaitForSeconds(time);
        psWater.Stop();
        isOn = false;
        // Le WaterJet attend que ses particules disparaissent avant de se detruire
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
