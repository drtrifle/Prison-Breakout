using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WeaponAttack : MonoBehaviour {
    public GameObject currWeapon;
    public GameObject Weapon1;
    public GameObject Weapon2;
    public Image redscreen;

    int maxHp = 20;
    public int hp;                      // How many times can player be hit;
    public Text hpText;                 // displayes current player health in UI
    public Image hpBar;

    public Text AmmoText;               // display current player ammo

    public Text gunName;
    GunScript gun;

    [SerializeField]
    private Image actual;                    //Bar part of the reload Bar
    [SerializeField]
    private GameObject rBar;                 //Actual whole reload Bar
    private bool reloading;

    [SerializeField]
    private Image gunImage;

    public GameObject forceField;
    public int fieldCounter;
    public Text fieldDisplay;
    public GameObject fieldBar;

    public GameObject LoseText;
    public float fadeDuration = 0.5f;
    public Color targetColor = new Color(255f / 255f, 0f / 255f, 0f / 255f, 0f / 255f);

    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {

        AmmoText.text = "";
        setHpCounter();

        actual.fillAmount = 0;
        rBar.SetActive(false);
        reloading = false;

        currWeapon = Weapon1;
        updateGunUI();

        fieldCounter = 0;
        StartCoroutine("FieldTimer");

        rb2d = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        redscreen.color = Color.Lerp(redscreen.color, targetColor, Time.deltaTime * fadeDuration);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeapon = Weapon1;
            updateGunUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeapon = Weapon2;
            updateGunUI();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetKeyDown(KeyCode.Q))
        {
            if(currWeapon == Weapon1)
            {
                currWeapon = Weapon2;
                updateGunUI();
            }
            else if (currWeapon == Weapon2)
            {
                currWeapon = Weapon1;
                updateGunUI();
            }

        }

        //ranged attack based on left mouse click
        if (Input.GetMouseButton(0)){
            if (currWeapon != null)
            {
                gun.attack();
            }
        }

        //Reload function based on R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currWeapon != null && gun.bulletLeft != 0)
            {
                reloading = true;
                rBar.SetActive(true);
            }
        }

        //Reload timer
        if (reloading)
        {
            reloadBar();
            if(actual.fillAmount >= 1)
            {
                gun.reload();
                actual.fillAmount = 0;
                rBar.SetActive(false);
                reloading = false;
            }
            if ( gun.currentClip != 0 && (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0))
            {
                actual.fillAmount = 0;
                rBar.SetActive(false);
                reloading = false;
            }
        }

        //forcefield
        if (Input.GetButtonDown("Fire2") && fieldCounter == 0)
        {
            Instantiate(forceField,transform.position,transform.rotation);
            fieldCounter = 20;
        }
    }


    //for pickup of weapons
    public GameObject getCurr()
    {
        return currWeapon;
    }

    public void setWeapon(GameObject gunObject, string GunName)
    {
        if (currWeapon == Weapon1) { 
           Weapon1 = gunObject;
        }else if (currWeapon == Weapon2) { 
           Weapon2 = gunObject;
        }
        currWeapon = gunObject;

        updateGunUI();   
    }

    //Handle player Damage
    //To-do: player death
    public void takeDamage(int dmg)
    {
        rb2d.velocity = Vector2.zero;
        hp -= dmg;
        setHpCounter();
        hurteffects();

        if (hp <= 0)
        {
            LoseText.SetActive(true);
            menuScript ms = GameObject.FindWithTag("Menu").GetComponent<menuScript>();
            ms.Restarting();
            Time.timeScale = 0;
        }
    }
    public void Heal(int dmg)
    {
        if(hp + dmg < maxHp)
        {
            hp += dmg;
        }else
        {
            hp = maxHp;
        }
        setHpCounter();
    }



    //Updates the text UI which counts the player health;
    void setHpCounter()
    {
        hpText.text = "Health: " + hp.ToString();
        hpBar.fillAmount = ((float)hp / (float)maxHp) * 1f;
    }

    void reloadBar()
    {
        actual.fillAmount += (1.0f / gun.reloadTime);
    }

    void updateGunUI()
    {
        gunImage.sprite = currWeapon.GetComponent<SpriteRenderer>().sprite;

        gunName.text = currWeapon.GetComponent<WeaponPickup>().weaponName;

        gun = currWeapon.GetComponent<GunScript>();
        gun.ammoText = AmmoText;
        gun.setAmmoCounter();
    }

    public void DropWeapon()
    {
        currWeapon.transform.position = transform.position;
        currWeapon.SetActive(true);
    }

    private IEnumerator FieldTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (fieldCounter != 0) { 
                fieldCounter--;
                setFieldTimer();
            }           
        }
    }

    void setFieldTimer()
    {
        fieldDisplay.text = "Forcefield Cooldown: " + fieldCounter;

        if(fieldCounter == 0)
        {
            fieldBar.SetActive(true);
        }else
        {
            fieldBar.SetActive(false);
        }
    }
    void hurteffects()
    {
        redscreen.color = new Color(1f, 0f, 0f, 1f);
    }
}
