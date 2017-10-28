using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {
    public Animator anim;
    Vector2 lastMovement;
	int damage =1;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        Vector2 movement_Vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement_Vector != Vector2.zero)
        {
            lastMovement = movement_Vector;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("isAttack", true);
            anim.SetFloat("input_x", lastMovement.x);
            anim.SetFloat("input_y", lastMovement.y);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }

	void OnTriggerEnter2D(Collider2D hit)
    {
		if(hit.gameObject.tag == "Enemy")
		{
			EnemyTakeDamage EnemyDamageScript = hit.gameObject.GetComponent<EnemyTakeDamage>();
			EnemyDamageScript.takeDamage(damage);
		}
        if(hit.gameObject.tag == "EnemyBullet")
        {
            Destroy(hit.gameObject);
        }
    }
}
