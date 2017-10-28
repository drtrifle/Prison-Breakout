using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    float timer = 5f;
    public int speed = 7;
    Rigidbody2D rb2d; //bullet rigidbody2d

    // Use this for initialization
    void Start()
    {
        //ignore collision between Enemy and EnemyBullet
        Physics2D.IgnoreLayerCollision(11,12,true);

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
            Destroy(gameObject);
        }

    
    }

}