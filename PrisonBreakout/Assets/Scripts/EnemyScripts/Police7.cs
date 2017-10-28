using UnityEngine;
using System.Collections;

//Desc: Script for police enemy who chases & shoots player only with line of sight & goes changes direction when blocked by a wall
//Damage type: Melee & Ranged(SMG)

public class Police7 : MonoBehaviour
{

    private Vector2 Player;                // Direction of the player
    public int speed;                      // speed of the player

    private int wallLayer;                 // layers to avoid ignore collisions
    private int playerLayer;               // layers to avoid ignore collisions
    private int finalLayer;                // layers to avoid ignore collisions

    private Collider2D myColli;
    private Vector2 playerDirection;

    private Vector3 changeDirection;       // determines the direction the AI takes when blocked by a wall
    private int wallTimer;

    private float XDif;
    private float YDif;
    public GameObject bulletPrefab;
    public int fireInterval = 60;          // Determines rate of fire
    int fireCounter = 0;                   // counter for rate of fire

    bool chase;                            // Determines if the police move to the last seen player positio

    public Animator anim;


    void Start()
    {

        //selects the layer for the raycast to cast at
        wallLayer = 1 << 8;
        playerLayer = 1 << 9;
        finalLayer = wallLayer | playerLayer; // combining the 2 layers can also use finalLayer = (1 << 8) | (1 << 9)

        myColli = GetComponent<Collider2D>();

        changeDirection = new Vector3(0, 1, 0);
        wallTimer = 0;

        anim = GetComponent<Animator>();      //for animation
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform.position;
        XDif = Player.x - transform.position.x;
        YDif = Player.y - transform.position.y;
        playerDirection = new Vector2(XDif, YDif);

        //cast a ray at the player
        myColli.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, Mathf.Infinity, finalLayer);
        myColli.enabled = true;


        //chase the player only if not behind wall
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            chase = true;

            //move towards player
            float step = speed * Time.deltaTime;
            transform.position = (Vector2.MoveTowards(transform.position, Player, step));

            //shoot
            fireBullet();

            // For enemy Animation
            if (playerDirection != Vector2.zero)
            {
                anim.SetBool("walking", true);
                anim.SetFloat("input_x", XDif);
                anim.SetFloat("input_y", YDif);
            }
        }
        else
        {
            chase = false;
        }

        //tries to go up when the police hits a wall
        if (hit.collider != null && hit.collider.CompareTag("Wall") && !chase)
        {
            float step = speed * Time.deltaTime;
            transform.position = (Vector2.MoveTowards(transform.position, transform.position + changeDirection, step));
            anim.SetBool("walking", true);
            anim.SetFloat("input_x", changeDirection.x);
            anim.SetFloat("input_y", changeDirection.y);
        }

        wallTimer--;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //changeDirection Vector when hit by wall
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "PlayerBullet" && coll.gameObject.tag != "EnemyBullet" && wallTimer <= 0)
        {
            int direction = Random.Range(0, 4);
            if (direction == 0)
            {
                changeDirection = new Vector3(1, 0, 0);
                wallTimer = 100;
            }
            else if (direction == 1)
            {
                changeDirection = new Vector3(0, -1, 0);
                wallTimer = 100;
            }
            else if (direction == 2)
            {
                changeDirection = new Vector3(-1, 0, 0);
                wallTimer = 100;
            }
            else if (direction == 3)
            {
                changeDirection = new Vector3(0, 1, 0);
                wallTimer = 100;
            }

        }
    }



    //for shotgun
    public void fireBullet()
    {
        //determines rate of fire of enemy
        if (fireCounter == fireInterval)
        {

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(new Vector2(XDif, YDif), new Vector2(0, 1));

            //flips the sign of the angle determining on player position relative to the enemy current posisiton
            if (XDif >= 0)
            {
                angle = -angle;
            }

            //Clones and fires bullet in player direction
            Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

            fireCounter = 0;
        }
        fireCounter++;
    }


}
