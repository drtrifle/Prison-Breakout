using UnityEngine;
using System.Collections;

public class PlayerField : MonoBehaviour {
    public int hp;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "EnemyBullet")
        {
            hp--;
            if (hp == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
