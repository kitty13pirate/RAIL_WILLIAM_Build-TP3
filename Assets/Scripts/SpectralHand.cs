using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralHand : Sortilege
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator disappear()
    {
        // La FireBall explose
        yield return new WaitForSeconds(1f);
        // Ensuite elle disparait
        Destroy(gameObject);
    }
}
