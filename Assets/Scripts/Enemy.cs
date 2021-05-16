using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Les statistiqes des enemies
    public int health = 1000;
    public int damage;
    public float speed;
    public float reach;

    // Utilises pour les diverses fonctions
    public Canvas canvasHealth;
    public Slider healthBar;
    private Collider bodyCollider;
    public Collider HitboxCollider;
    protected bool isAttacking;
    private bool isDead = false;

    // Le joueur
    protected GameObject playerCharacter; 
    protected PlayerCharacterBoi playerCharacterScript;

    // Les composants
    NavMeshAgent NavMeshAgent;
    protected Animator enemyAnimator;
    protected AudioSource audioSource;
    public AudioClip attackSound;

    public void Awake()
    {
        // Les variables sont assignes
        NavMeshAgent = GetComponent<NavMeshAgent>();
        playerCharacterScript = FindObjectOfType<PlayerCharacterBoi>();
        playerCharacter = FindObjectOfType<PlayerCharacterBoi>().gameObject;
        enemyAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

        // modifier la vitesse du pnj
        NavMeshAgent.speed = speed;
    }

    // L'enemie prend du degat et meurt s'il n'a plus de vies
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

    // L'enemie meurt. Le canvas, le NavMeshAgent, et le collider sont arretes
    public virtual void die()
    {
        canvasHealth.enabled = false;
        NavMeshAgent.isStopped = true;
        bodyCollider.isTrigger = true;

        // L'enemie joue une animation et un son
        enemyAnimator.SetTrigger("die");
        audioSource.PlayOneShot(audioSource.clip);

        // Le GameManager est informer de la mort d'un enemie
        GameManager.singleton.deadEnemy();
        
    }

    // Une animation de victoire
    public void win()
    {
        enemyAnimator.SetTrigger("win");
    }

    // L'enemie attaque
    public void attack()
    {
        // Il fait un son et enregistre tous les colliders qui entrent en contact avec sa hibox d'attaque
        audioSource.PlayOneShot(attackSound);
        Collider[] colliders;
        colliders = Physics.OverlapBox(HitboxCollider.transform.position, HitboxCollider.transform.lossyScale / 2, HitboxCollider.transform.rotation);

        foreach (Collider collision in colliders)
        {
            // Si le collider est celui du joueur, le joueur prend des degats
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

    // L<enemie se deplace vers le joueur
    public void headTowards()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;

        // L'enemie fixe le joueur tant qu'il est en vie
        if (!isDead)
            transform.LookAt(playerCharacter.transform);
        // La bar de vie fixe la camera
        healthBar.transform.LookAt(Camera.main.transform.position - v);
        healthBar.transform.rotation = (Camera.main.transform.rotation);

        if (!isAttacking)
        {
            NavMeshAgent.speed = speed;
            // Me deplacer vers la destination
            NavMeshAgent.SetDestination(playerCharacter.transform.position);
        }

        // L'enemie attaque lorsqu'il est assez proche
        if (Vector3.Distance(transform.position, playerCharacter.transform.position) < reach && !isAttacking && !playerCharacterScript.isDead && !isDead)
        {
            isAttacking = true;
            enemyAnimator.SetTrigger("attack");
            NavMeshAgent.speed = 0;
        }
    }

    // Le corp de l'enemie commence a disparaitre
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
