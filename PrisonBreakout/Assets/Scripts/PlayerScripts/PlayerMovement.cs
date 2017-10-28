using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public Rigidbody2D rb2D;
    public Animator anim;
    int stunCounter;
    bool stunned;

    public GameObject stunText;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector2 movement_Vector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (movement_Vector != Vector2.zero && !stunned)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("input_x", movement_Vector.x);
            anim.SetFloat("input_y", movement_Vector.y);
            rb2D.MovePosition(rb2D.position + movement_Vector * Time.deltaTime * speed);

        }
        else
        {
            anim.SetBool("walking", false);
        }

        //rb2D.MovePosition(rb2D.position + movement_Vector * Time.deltaTime * speed);

        if(stunCounter == 0)
        {
            stunned = false;
            stunText.SetActive(false);
        }
        else
        {
            stunCounter--;
        }
    }

    public void stunFor(int x)
    {
        stunCounter = x;
        stunned = true;
        stunText.SetActive(true);
    }
}
