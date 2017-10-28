using UnityEngine;
using System.Collections;

public class EnemyTakeDamage : MonoBehaviour {

    public int hp;
    public int invincible = 0;
	public bool contact;
    public GameObject[] PickUpArr;
    public GameObject BossFinalForm;

    public bool isBoss;
    public bool isFinalBoss;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        //invincibility frames after being hit
        if (invincible > 0)
        {
            invincible -= 1;
        }

        //enemy death
        if (hp <= 0)
        {
            if (isBoss)
            {
                Instantiate(BossFinalForm, transform.position, transform.rotation);
            }
            else if (isFinalBoss){
                WinManager winScript = GameObject.FindWithTag("Manager").GetComponent<WinManager>();
                winScript.win();
            }else{
                spawnPickup();
            }
            gameObject.SetActive(false);
        }

    }

    public void takeDamage(int x)
    {
        rb2d.velocity = Vector2.zero;
        hp -= x;
        invincible = 30;
        if (isBoss)
        {
            Boss bossScript = GetComponent<Boss>();
            bossScript.HealthBar(x);
        }
    }

    //handle damage
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Contact Damage
		if (hp > 0 && coll.gameObject.tag == "Player" && invincible == 0 && contact)
        {
            coll.gameObject.GetComponent<WeaponAttack>().takeDamage(1);
            takeDamage(1);    
        }
    }

    void spawnPickup()
    {
        int rng = Random.Range(0, 100);
        if(rng == 0){
            Instantiate(PickUpArr[0], transform.position, transform.rotation);
        }else if (rng <= 5){
            Instantiate(PickUpArr[1], transform.position, transform.rotation);
        }else if (rng <= 10){
            Instantiate(PickUpArr[2], transform.position, transform.rotation);
        }else if (rng <= 20){
            Instantiate(PickUpArr[3], transform.position, transform.rotation);
        }else if (rng <= 30){
            Instantiate(PickUpArr[4], transform.position, transform.rotation);
        }
    }
   
}
