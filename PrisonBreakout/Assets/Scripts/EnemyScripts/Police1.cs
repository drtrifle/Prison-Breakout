using UnityEngine;
using System.Collections;

//Desc: Script for police enemy who chases player constantly without pathfinding
//Damage type: Melee

public class Police1 : MonoBehaviour
{

    private Vector3 Player;
    public int speed;

    public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //basic chase the player
        Player = GameObject.Find("Player").transform.position;
        float step = speed * Time.deltaTime;


        // For enemy Animation
        Vector2 newVector = Player - transform.position; // used for animation
        if (newVector != Vector2.zero)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("input_x", newVector.x);
            anim.SetFloat("input_y", newVector.y);
        }
        else
        {
            anim.SetBool("walking", false);
        }


        transform.position = (Vector3.MoveTowards(transform.position, Player, step));


    }



}
