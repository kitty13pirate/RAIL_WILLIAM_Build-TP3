using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBurst : Sortilege
{
    public ParticleSystem psWind;
    // Start is called before the first frame update
    void Start()
    {
        psWind.Emit(300);
        StartCoroutine(disappear(2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
