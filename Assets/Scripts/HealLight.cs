using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLight : Sortilege
{
    public int damage;
    public Collider healHitBox;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappear());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitboxCollision(healHitBox, 0, damage);
    }

    public IEnumerator disappear()
    {
        // La lumiere attend avant de disparaitre
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
    }

    // La lumiere guerit le joueur lorsqu'il est dedans
    public override void hitboxCollision(Collider collider, int type, int damage)
    {
        // la lumiere prend tous les colliders en collision avec sa hitbox
        Collider[] colliders;
        colliders = Physics.OverlapSphere(collider.transform.position, collider.transform.localScale.x / 2);

        // Si c'est le joueur elle le guerit
        foreach (Collider collision in colliders)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerCharacterBoi scriptBoi = collision.GetComponent(typeof(PlayerCharacterBoi)) as PlayerCharacterBoi;
                scriptBoi.takeDamage(damage);
            }
        }
    }
}
