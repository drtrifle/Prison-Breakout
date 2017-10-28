using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warp : MonoBehaviour {

    WarpManager warpScript;
    List<Transform> warpTarget;
    GameObject thisRoom;
    GameObject nextRoom;
    int rdmIndex;
    Vector2 nextVector;
	
    void OnTriggerEnter2D(Collider2D other) {

        //teleports objects as long as they are not bullets
        if (other.gameObject.tag == "Player")
        {

            warpScript = GameObject.FindWithTag("Manager").GetComponent<WarpManager>();
            warpTarget = warpScript.warpList;

            thisRoom = gameObject.transform.parent.gameObject;
            rdmIndex = Random.Range(0, warpTarget.Count);

            nextRoom = warpTarget[rdmIndex].transform.GetChild(0).gameObject;
            nextVector = warpTarget[rdmIndex].position;

            warpScript.removeWarp(rdmIndex);

            nextRoom.SetActive(true);
            other.gameObject.transform.position = nextVector;
            thisRoom.SetActive(false);
        }

    }
}
