using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health = 1000;
    public Slider healthBar;
    public Collider collider;
    protected int damage;
    protected bool isAttacking;

    public void takeDamage(int damageNumber)
    {
        health -= damageNumber;
        healthBar.value = health;
        if (health <= 0)
        {
            die();
        }
    }

    public virtual void die()
    {
        Destroy(gameObject);
        GameManager.singleton.deadEnemy();
    }



    public void attack()
    {
        Collider[] colliders;
        colliders = Physics.OverlapBox(collider.transform.position, collider.transform.lossyScale / 2, collider.transform.rotation);

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
}
