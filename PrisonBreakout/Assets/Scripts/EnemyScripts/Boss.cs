using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    private Vector2 Player;                // Direction of the player
    public int speed;                      // speed of the player

    private float XDif;
    private float YDif;

    public GameObject bulletPrefab;        //phase 1
    public GameObject bulletPrefab2;       //phase 2
    public GameObject bulletPrefab3;       //phase 3


    public int fireInterval = 10;          // Determines rate of fire
    int fireCounter = 0;                   // counter for rate of fire

    Vector3 EnemyDirection;                // default to right

    public Transform turret1;
    public Transform turret2;

    EnemyTakeDamage dmgScript;

    [SerializeField]
    private Image actual;                    //Bar part of the reload Bar
    [SerializeField]
    private GameObject hpBar;                 //Actual whole reload Bar

    void Start()
    {
        EnemyDirection = Vector3.right;

        dmgScript = GetComponent<EnemyTakeDamage>();

        hpBar.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform.position;
        XDif = Player.x - transform.position.x;
        YDif = Player.y - transform.position.y;
       
        //move towards Right
        float step = speed * Time.deltaTime;
        transform.position = (Vector2.MoveTowards(transform.position, transform.position + EnemyDirection, step));

        Attack();
    }

    public void Attack()
    {
        if(dmgScript.hp >= 80)
        {
            Phase1();
        }else if (dmgScript.hp >= 50)
        {
            Phase2();
        }else if (dmgScript.hp >= 0)
        {
            Phase3();
        }
    }

    public void Phase1()
    {
        //determines rate of fire of enemy
        if (fireCounter == fireInterval)
        {

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(new Vector2(XDif, YDif), new Vector2(0, 1));

            //flips the sign of the angle determining on player position relative to the enemy current posisiton
            if (XDif >= 0)
            {
                angle = -angle;
            }

            //Clones and fires bullet in player direction
            Instantiate(bulletPrefab, turret1.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Instantiate(bulletPrefab, turret2.position, Quaternion.AngleAxis(angle, Vector3.forward));

            fireCounter = 0;
        }
        fireCounter++;
    }

    public void Phase2()
    {
        //determines rate of fire of enemy
        if (fireCounter == fireInterval)
        {

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(new Vector2(XDif, YDif), new Vector2(0, 1));

            //flips the sign of the angle determining on player position relative to the enemy current posisiton
            if (XDif >= 0)
            {
                angle = -angle;
            }

            //Clones and fires bullet in player direction
            Instantiate(bulletPrefab2, turret1.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Instantiate(bulletPrefab2, turret2.position, Quaternion.AngleAxis(angle, Vector3.forward));

            fireCounter = 0;
        }
        fireCounter++;
    }

    public void Phase3()
    {
        //determines rate of fire of enemy
        if (fireCounter == fireInterval)
        {

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(new Vector2(XDif, YDif), new Vector2(0, 1));

            //flips the sign of the angle determining on player position relative to the enemy current posisiton
            if (XDif >= 0)
            {
                angle = -angle;
            }

            //Clones and fires bullet in player direction
            Instantiate(bulletPrefab3, turret1.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Instantiate(bulletPrefab3, turret2.position, Quaternion.AngleAxis(angle, Vector3.forward));

            fireCounter = 0;
        }
        fireCounter++;
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Barricade")
        {
            if(EnemyDirection == Vector3.right)
            {
                EnemyDirection = Vector3.left;
            }
            else if (EnemyDirection == Vector3.left)
            {
                EnemyDirection = Vector3.right;
            }
        }
    }

    public void HealthBar(int x)
    {
        actual.fillAmount -= (x * (1.0f / 100));

        if(actual.fillAmount <= 0)
        {
            hpBar.SetActive(false);
        }
    }
}
