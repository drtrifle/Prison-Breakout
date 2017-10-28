using UnityEngine;
using System.Collections;

public class WeaponChest : MonoBehaviour {

    public GameObject[] WepArr;

    void OnCollisionEnter2D(Collision2D coll)
    {
        Instantiate(WepArr[Random.Range(0, WepArr.Length)],transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
