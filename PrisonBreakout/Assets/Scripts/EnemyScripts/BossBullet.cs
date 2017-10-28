using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {

    float timer = 5f;
    public int speed = 7;
    Rigidbody2D rb2d; //bullet rigidbody2d
    Transform initial;

    public GameObject bulletPrefab;        //spawn a shotgun prefab

    // Use this for initialization
    void Start()
    {

        initial = transform;
        //ignore collision between Enemy and EnemyBullet
        Physics2D.IgnoreLayerCollision(11, 12, true);

        //ignore collision between PlayerBullet and EnemyBullet
        Physics2D.IgnoreLayerCollision(10, 12, true);

        //ignore collision between EnemyBullets
        Physics2D.IgnoreLayerCollision(12, 12, true);

        //Ignore collision between bullets and floor objects
        Physics2D.IgnoreLayerCollision(12, 13, true);

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 180);
            Instantiate(bulletPrefab, transform.position - (transform.position - initial.position).normalized, Quaternion.Euler(rot));
            Destroy(this.gameObject);
            
        }


    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //makes player take damage on contact with bullet
            coll.gameObject.GetComponent<WeaponAttack>().takeDamage(1);
        }

        if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "EnemyBullet" && coll.gameObject.tag != "Barricade")
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 180);
            Instantiate(bulletPrefab, transform.position - (transform.position - initial.position).normalized, Quaternion.Euler(rot));
            Destroy(this.gameObject);

        }

    }
}
