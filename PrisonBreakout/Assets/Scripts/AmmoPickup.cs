using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour {
    public int clipNum; 

    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            //makes player take damage on contact with mine
            WeaponAttack waScript = coll.gameObject.GetComponent<WeaponAttack>();
            GunScript currWep = waScript.getCurr().GetComponent<GunScript>();
            currWep.bulletLeft += (clipNum * currWep.clipSize);
            currWep.setAmmoCounter();

            gameObject.SetActive(false);
        }


    }
}
