using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortilege : MonoBehaviour
{
    protected AudioSource audioSource;

    // La fonction pour faire disparaitre les sorts
    public virtual IEnumerator disappear(float time)
    {
        // On attend pendant le temps indique
        yield return new WaitForSeconds(time);
        // Ensuite elle disparait
        Destroy(gameObject);
    }

    // La fonction pour verifier les collisions
    public virtual void hitboxCollision(Collider collider, int type, int damage)
    {
        // On prend tous les colliders en collision avec sa hitbox
        Collider[] colliders;
        if (type == 1)
        {
            colliders = Physics.OverlapSphere(collider.transform.position, collider.transform.lossyScale.x / 2);
        }
        else
        {
            colliders = Physics.OverlapBox(collider.transform.position, collider.transform.lossyScale / 2, collider.transform.rotation);
        }

        foreach(Collider collision in colliders)
        {
            // Si c'est un enemy, il prend du degat
            if (collision.CompareTag("Enemy"))
            {
                Enemy scriptEnemy = collision.GetComponent(typeof(Enemy)) as Enemy;
                scriptEnemy.takeDamage(damage);
            }
        }
    }
}
