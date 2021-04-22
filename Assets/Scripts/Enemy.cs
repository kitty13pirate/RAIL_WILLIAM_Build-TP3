using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageNumber)
    {
        health -= damageNumber;
        Debug.Log(health);
        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
