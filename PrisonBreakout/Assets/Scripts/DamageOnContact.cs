using UnityEngine;
using System.Collections;

public class DamageOnContact : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //makes player take damage on contact
            coll.gameObject.GetComponent<WeaponAttack>().takeDamage(1);
        }

    }
}
