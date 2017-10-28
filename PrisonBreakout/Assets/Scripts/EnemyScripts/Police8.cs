using UnityEngine;
using System.Collections;

//Desc: Script for police enemy who chases & shoots player only with line of sight & goes to player's last seen location
//Damage type: Melee & Ranged(Shotgun)

public class Police8 : MonoBehaviour
{

    private Vector2 Player;                // Direction of the player
    public int speed;                      // speed of the player
   
    private int wallLayer;                 // layers to avoid ignore collisions
    private int playerLayer;               // layers to avoid ignore collisions
    private int finalLayer;                // layers to avoid ignore collisions

    private Collider2D myColli;
    private Vector2 playerDirection;
    private float XDif;
    private float YDif;
    public GameObject bulletPrefab;
    public int fireInterval = 10;          // Determines rate of fire
    int fireCounter = 0;                   // counter for rate of fire

    Vector3 lastHit;                       // The last seen player position
    bool chase;                            // Determines if the police move to the last seen player position


    void Start()
    {

        //selects the layer for the raycast to cast at
        wallLayer = 1 << 8;
        playerLayer = 1 << 9;
        finalLayer = wallLayer | playerLayer; // combining the 2 layers can also use finalLayer = (1 << 8) | (1 << 9)

        myColli = GetComponent<Collider2D>();

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
            lastHit = hit.transform.position;
            chase = true;

            //move towards player
            float step = speed * Time.deltaTime;
            transform.position = (Vector2.MoveTowards(transform.position, Player, step));

            //shoot
            fireBullet();
        }
        else //move to player's last seen location
        {
            if (chase)
            {
                float step = speed * Time.deltaTime;
                transform.position = (Vector2.MoveTowards(transform.position, lastHit, step));
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

