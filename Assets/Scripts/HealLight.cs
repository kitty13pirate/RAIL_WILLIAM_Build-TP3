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
        // La FireBall explose
        yield return new WaitForSeconds(4.5f);
        // Ensuite elle disparait
        Destroy(gameObject);
    }

    public override void hitboxCollision(Collider collider, int type, int damage)
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(collider.transform.position, collider.transform.localScale.x / 2);


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
