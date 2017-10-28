using UnityEngine;
using System.Collections;

public class BearTrap : MonoBehaviour {

    public bool exploded = false;
    bool fuse = false;
    Animator anim;

    bool stunReady;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        stunReady = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuse)
        {
            if (anim != null)
            {
                anim.enabled = true;
            }
            else
            {
                DestroyAfterTime script = GetComponent<DestroyAfterTime>();
                script.enabled = true;
            }
        }

        if (exploded == true)
        {
            gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Player" && stunReady)
        {
            coll.gameObject.GetComponent<PlayerMovement>().stunFor(50);
            stunReady = false;
            fuse = true;
        }
    }
}
