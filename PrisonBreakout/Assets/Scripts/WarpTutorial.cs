using UnityEngine;
using System.Collections;

public class WarpTutorial : MonoBehaviour {
    public Transform nextVector;

    void OnTriggerEnter2D(Collider2D other)
    {

        //teleports objects as long as they are not bullets
        if (other.gameObject.tag == "Player")
        {

            GameObject nextRoom = nextVector.transform.GetChild(0).gameObject;
            nextRoom.SetActive(true);

            other.gameObject.transform.position = nextVector.position;

        }
    }
}
