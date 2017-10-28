﻿using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float timer = 5f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

    }
}
