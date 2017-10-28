using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

    public int healAmt;

    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            //makes player take damage on contact with mine
            coll.gameObject.GetComponent<WeaponAttack>().Heal(healAmt);
            gameObject.SetActive(false);
        }


    }
}
