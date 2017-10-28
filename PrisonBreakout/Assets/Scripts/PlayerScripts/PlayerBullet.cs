using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    float timer = 5f;
    public int speed = 500;
    public int damage;
    Rigidbody2D rb2d; //bullet rigidbody2d
    

    // Use this for initialization
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

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            EnemyTakeDamage EnemyDamageScript = hit.gameObject.GetComponent<EnemyTakeDamage>();
            EnemyDamageScript.takeDamage(damage);
        }
    }
}
