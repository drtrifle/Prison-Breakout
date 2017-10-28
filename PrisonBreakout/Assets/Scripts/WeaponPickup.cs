using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour
{
    public string weaponName;
    WeaponAttack wa;

    // Use this for initialization
    void Start()
    {
        wa = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponAttack>();
    }


    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player picked up " + weaponName);
            if (wa.getCurr() != null)
            {
                wa.DropWeapon();
            }
            wa.setWeapon(this.gameObject, weaponName);
            this.gameObject.SetActive(false);
        }
    }
}
