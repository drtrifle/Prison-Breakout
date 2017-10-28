using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {

	public GameObject[] gate;
	public Sprite onSprite;
	SpriteRenderer spriteRender;

	void Start(){
		spriteRender = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{

		if (coll.gameObject.tag == "PlayerBullet" || coll.gameObject.tag == "Player")
		{
            for(int x = 0; x < gate.Length; x++)
			gate[x].SetActive(false);

			spriteRender.sprite = onSprite;
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "PlayerBullet")
        {
            for (int x = 0; x < gate.Length; x++)
            gate[x].SetActive(false);

            spriteRender.sprite = onSprite;
        }
    }
}
