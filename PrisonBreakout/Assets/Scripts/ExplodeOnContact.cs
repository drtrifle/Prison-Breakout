using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {

    public Collider2D innerColli;
    public Collider2D outerColli;
    public bool exploded = false;
    bool fuse = false;
    Animator anim;

	public int damage;



    // Use this for initialization
    void Start() {
		anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (fuse)
        {
            if (anim != null) { 
               anim.enabled = true;
            }
            else
            {
                DestroyAfterTime script = GetComponent<DestroyAfterTime>();
                script.enabled = true;
            }
        }

        if(exploded == true)
        {
            gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.tag == "Player") {
            //makes player take damage on contact with mine
			coll.gameObject.GetComponent<WeaponAttack>().takeDamage(damage);

			innerColli.enabled = false;
            outerColli.enabled = true;
            fuse = true;
        }
    }
}
