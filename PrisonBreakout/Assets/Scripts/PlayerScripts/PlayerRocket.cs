using UnityEngine;
using System.Collections;

public class PlayerRocket : MonoBehaviour {
    public Collider2D outerColli;
    public bool exploded = false;
    bool fuse = false;
    public int damage;
    public bool homing;

    Animator anim;
    Rigidbody2D rb2d; //bullet rigidbody2d


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

        anim = GetComponent<Animator>();

        rb2d = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp);

        //calculates the angle the shotgun bullet prefab must be rotated to face the player
        float angle = Vector2.Angle(dir, new Vector2(0, 1));

        if (dir.x >= 0)
        {
            angle = -angle;
        }

        if (!fuse && homing) { 
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * 10);
		}else if(!fuse)
        {
            rb2d.AddForce(transform.up, ForceMode2D.Impulse);
        }
       

        if (fuse)
        {
            anim.enabled = true;
        }
        if (exploded)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag != "Player" && !fuse)
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
