using UnityEngine;
using System.Collections;

public class EnemyBulletDamageHandler : MonoBehaviour
{

    public int health;         // how many times can enemy be hit

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //makes player take damage on contact with bullet
            coll.gameObject.GetComponent<WeaponAttack>().takeDamage(1);
        }

        if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "EnemyBullet" && coll.gameObject.tag != "Barricade")
        {
            health -= 1;
        }

    }
}