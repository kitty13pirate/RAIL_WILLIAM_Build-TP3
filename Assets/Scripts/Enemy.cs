using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent NavMeshAgent;
    public int health = 1000;
    private bool isDead = false;
    public Canvas canvasHealth;
    public Slider healthBar;
    private Collider bodyCollider;
    public Collider HitboxCollider;
    public int damage;
    protected bool isAttacking;
    public float speed;
    public float reach;
    protected GameObject playerCharacter; 
    protected Animator enemyAnimator;
    protected PlayerCharacterBoi playerCharacterScript;

    public void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        playerCharacterScript = FindObjectOfType<PlayerCharacterBoi>();
        playerCharacter = FindObjectOfType<PlayerCharacterBoi>().gameObject;
        enemyAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider>();

        //modifier la vitesse du pnj
        NavMeshAgent.speed = speed;
    }

    public void takeDamage(int damageNumber)
    {
        health -= damageNumber;
        healthBar.value = health;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            die();
        }
    }

    public virtual void die()
    {
        canvasHealth.enabled = false;
        NavMeshAgent.isStopped = true;
        enemyAnimator.SetTrigger("die");
        bodyCollider.isTrigger = true;
        GameManager.singleton.deadEnemy();
        NavMeshAgent.isStopped = true;
    }

    public void win()
    {
        enemyAnimator.SetTrigger("win");
    }

    public void attack()
    {
        Collider[] colliders;
        colliders = Physics.OverlapBox(HitboxCollider.transform.position, HitboxCollider.transform.lossyScale / 2, HitboxCollider.transform.rotation);

        foreach (Collider collision in colliders)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerCharacterBoi scriptBoi = collision.GetComponent(typeof(PlayerCharacterBoi)) as PlayerCharacterBoi;
                scriptBoi.takeDamage(damage);
            }
        }
    }
    public void stopAttacking()
    {
        isAttacking = false;
    }

    public void headTowards()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        if (!isDead)
            transform.LookAt(playerCharacter.transform);
        healthBar.transform.LookAt(Camera.main.transform.position - v);
        healthBar.transform.rotation = (Camera.main.transform.rotation);

        if (!isAttacking)
        {
            NavMeshAgent.speed = speed;
            // Me deplacer vers la destination
            NavMeshAgent.SetDestination(playerCharacter.transform.position);
        }

        //Attaque
        if (Vector3.Distance(transform.position, playerCharacter.transform.position) < reach && !isAttacking && !playerCharacterScript.isDead && !isDead)
        {
            isAttacking = true;
            enemyAnimator.SetTrigger("attack");
            NavMeshAgent.speed = 0;
        }
    }

    public void disappearRef()
    {
        StartCoroutine(disappear());
    }

    public IEnumerator disappear()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
