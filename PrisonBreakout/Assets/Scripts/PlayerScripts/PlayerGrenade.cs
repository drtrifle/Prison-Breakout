using UnityEngine;
using System.Collections;

public class PlayerGrenade : MonoBehaviour {

    Collider2D outerColli;
    public bool exploded = false;
    bool fuse = false;
    public int damage;
    Rigidbody2D rb2d; //bullet rigidbody2d
    Animator anim;


    void Start()
    {
        //Ignore collision between player and bullet
        Physics2D.IgnoreLayerCollision(9, 10, true);

        //Ignore collision between bullets
        Physics2D.IgnoreLayerCollision(10, 10, true);

        //ignore collision between PlayerBullet and EnemyBullet
        Physics2D.IgnoreLayerCollision(10, 12, true);

        //Ignore collision between bullets and floor objects
        Physics2D.IgnoreLayerCollision(10, 13, true);

        outerColli = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddForce(transform.up * 10, ForceMode2D.Impulse);

        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuse)
        {
            anim.enabled = true;
        }
        if (exploded == true)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag != "Player")
        {
            outerColli.enabled = true;
            rb2d.velocity = Vector2.zero;
            fuse = true;
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            EnemyTakeDamage EnemyDamageScript = hit.gameObject.GetComponent<EnemyTakeDamage>();
            EnemyDamageScript.takeDamage(damage);
        }
    }
}
