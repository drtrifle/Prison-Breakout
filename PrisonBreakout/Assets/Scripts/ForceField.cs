using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

    public int hp;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "PlayerBullet")
        {
            hp--;
            if(hp == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
