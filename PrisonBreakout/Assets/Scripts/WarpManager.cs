using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarpManager : MonoBehaviour {
    public List<Transform> warpList;

    GameObject nextRoom;
    int rdmIndex;

    // Use this for initialization

    public void removeWarp(int index)
    {
        warpList.RemoveAt(index);
    }


}
