using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
    public GameObject bulletPrefab;     // for now takes in what prefab you want to use to represent the projectile
    public string direction;
    public int fireInterval;
    float angle;
    int counter;

    // Use this for initialization
    void Start () {
        counter = 0;

        switch (direction) {
            case "up":
                angle = 0;
                break;
            case "down":
                angle = 180;
                break;
            case "left":
                angle = 90;
                break;
            case "right":
                angle = 270;
                break;
            
            
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        counter++;

        if(counter == fireInterval)
        Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        counter %= fireInterval;
    }
}
