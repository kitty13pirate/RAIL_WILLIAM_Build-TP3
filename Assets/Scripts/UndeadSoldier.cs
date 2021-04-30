using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSoldier : Enemy
{
    private GameObject playerCharacter;
    private Animator enemyAnimator;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = FindObjectOfType<PlayerCharacterBoi>().gameObject;
        enemyAnimator = GetComponent<Animator>();
        damage = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(playerCharacter.transform);
        healthBar.transform.LookAt(Camera.main.transform.position - v);
        healthBar.transform.rotation = (Camera.main.transform.rotation);

        if (!isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCharacter.transform.position, speed);
        }

        //Attaque
        if (Vector3.Distance(transform.position, playerCharacter.transform.position) < 1f && !isAttacking)
        {
            isAttacking = true;
            enemyAnimator.SetTrigger("attack");
        }
    }
}
