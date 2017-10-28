using UnityEngine;
using System.Collections;

//Desc: Script for police enemy who chases & shoots player only with line of sight
//Damage type: Melee & Ranged(Single Shot)

public class Police3 : MonoBehaviour
{

    private Vector2 Player;
    public int speed;                   //speed of movement
    private int wallLayer;
    private int playerLayer;
    private int finalLayer;
    private Collider2D myColli;
    private Vector2 playerDirection;
    private float XDif;
    private float YDif;
    public GameObject bulletPrefab;
    public int fireInterval = 60; // Determines rate of fire
    int fireCounter = 0;          // counter for rate of fire

	public Animator anim;


    void Start()
    {

        //selects the layer for the raycast to cast at
        wallLayer = 1 << 8;
        playerLayer = 1 << 9;
        finalLayer = wallLayer | playerLayer; // combining the 2 layers can also use finalLayer = (1 << 8) | (1 << 9)

        myColli = GetComponent<Collider2D>();

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
			anim.SetBool("walking", false); // for Idle animation
		}

    }

    //for single shot pistol
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
