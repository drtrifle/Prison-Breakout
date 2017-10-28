using UnityEngine;
using System.Collections;

//Desc: Script for police enemy who chases player only with line of sight
//Damage type: Melee

public class Police2 : MonoBehaviour
{

    private Vector2 Player;
    public int speed;
    private int wallLayer;
    private int playerLayer;
    private int finalLayer;
    private Collider2D myColli;
    private Vector2 playerDirection;
    private float XDif;
    private float YDif;
    public Animator anim;

    bool stunReady;

    void Start()
    {

        //selects the layer for the raycast to cast at
        wallLayer = 1 << 8;
        playerLayer = 1 << 9;
        finalLayer = wallLayer | playerLayer; // combiniing the 2 layers can also use finalLayer = (1 << 8) | (1 << 9)

        myColli = GetComponent<Collider2D>();

        anim = GetComponent<Animator>();      //for animation

        stunReady = true;
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
            float step = speed * Time.deltaTime;
            transform.position = (Vector2.MoveTowards(transform.position, Player, step));

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && stunReady)
        {
            other.GetComponent<PlayerMovement>().stunFor(100);
            stunReady = false;
        }
    }
}
