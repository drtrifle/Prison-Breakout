using UnityEngine;
using System.Collections;

public class SecretWarp : MonoBehaviour {
    public Transform warpTarget;

    void OnTriggerStay2D(Collider2D other)
    {

        //teleports objects as long as they are not bullets
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            other.gameObject.transform.position = warpTarget.position;
        }

    }
}
