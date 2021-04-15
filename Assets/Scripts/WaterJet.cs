using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJet : MonoBehaviour
{
    public ParticleSystem waterJet;
    public bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        // Le timer debute
        StartCoroutine(disappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pour faire disparaitre le WaterJet
    public IEnumerator disappear()
    {
        // Le WaterJet dure 3 secondes
        yield return new WaitForSeconds(3f);
        waterJet.Stop();
        isOn = false;
        // Le WaterJet attend que ses particules disparaissent avant de se detruire
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
