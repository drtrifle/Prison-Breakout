using UnityEngine;
using System.Collections;

public class DestroyOnCollide : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameObject.SetActive(false);
    }
}
