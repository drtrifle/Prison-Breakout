using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    Camera myCam;

	// Use this for initialization
	void Start () {
        myCam = GetComponent<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {

        myCam.orthographicSize = (Screen.height / 90f);
	
	}
}
