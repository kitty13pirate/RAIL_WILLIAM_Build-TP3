using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortilege : MonoBehaviour
{
    public virtual IEnumerator disappear(float time)
    {
        // On attend pendant le temps indique
        yield return new WaitForSeconds(time);
        // Ensuite elle disparait
        Destroy(gameObject);
    }

    public virtual void hitboxCollision(Collider collider, int type, int damage)
    {
        Collider[] colliders;
        if (type == 1)
        {
            colliders = Physics.OverlapSphere(collider.transform.position, collider.transform.localScale.x / 2);
        }
        else
        {
            colliders = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2);
        }

        foreach(Collider collision in colliders)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy scriptEnemy = collision.GetComponent(typeof(Enemy)) as Enemy;
                scriptEnemy.takeDamage(damage);
            }
        }
    }
}
