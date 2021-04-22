using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLight : Sortilege
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
        yield return new WaitForSeconds(3f);
        // Ensuite elle disparait
        Destroy(gameObject);
    }
}
