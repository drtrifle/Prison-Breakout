using UnityEngine;
using System.Collections;

public class EnemyShotgun : MonoBehaviour {

    public int speed = 5;
    Rigidbody2D rb2d; //bullet rigidbody2d

    // Use this for initialization
    void Start () {

        //ignore collision between Enemy and EnemyBullet
        Physics2D.IgnoreLayerCollision(11, 12, true);

        //ignore collision between EnemyBullets
        Physics2D.IgnoreLayerCollision(12, 12, true);

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);

    }
}
