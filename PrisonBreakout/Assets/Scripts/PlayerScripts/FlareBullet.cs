using UnityEngine;
using System.Collections;

public class FlareBullet : MonoBehaviour {

    public int damage;
    public int speed;
    Rigidbody2D rb2d; //bullet rigidbody2d

    // Use this for initialization
    void Start () {
        //Ignore collision between player and bullet
        Physics2D.IgnoreLayerCollision(9, 10, true);

        //Ignore collision between bullets
        Physics2D.IgnoreLayerCollision(10, 10, true);

        //ignore collision between PlayerBullet and EnemyBullet
        Physics2D.IgnoreLayerCollision(10, 12, true);

        //Ignore collision between bullets and floor objects
        Physics2D.IgnoreLayerCollision(10, 13, true);

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            EnemyTakeDamage EnemyDamageScript = hit.gameObject.GetComponent<EnemyTakeDamage>();
            EnemyDamageScript.takeDamage(damage);
        }
        if (hit.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
