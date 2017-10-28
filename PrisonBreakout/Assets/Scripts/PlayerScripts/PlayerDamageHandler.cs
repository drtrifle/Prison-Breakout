using UnityEngine;
using System.Collections;

public class PlayerDamageHandler : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet" && coll.gameObject.tag != "Barricade") {
            health -= 1;

        }
      
    }
}
