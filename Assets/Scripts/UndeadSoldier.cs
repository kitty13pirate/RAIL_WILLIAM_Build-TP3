using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSoldier : Enemy
{
    private GameObject playerCharacter;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = FindObjectOfType<PlayerCharacterBoi>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerCharacter.transform.position, speed);

        //Attaque
        if (Vector3.Distance(transform.position, playerCharacter.transform.position) < 1f)
        {
            PlayerCharacterBoi scriptBoi = playerCharacter.GetComponent(typeof(PlayerCharacterBoi)) as PlayerCharacterBoi;
            scriptBoi.takeDamage(1);
        }
    }
}
