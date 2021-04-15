using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : MonoBehaviour
{
    public ParticleSystem psDust;
    // Start is called before the first frame update
    void Start()
    {

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
        Destroy(gameObject);
    }
}
