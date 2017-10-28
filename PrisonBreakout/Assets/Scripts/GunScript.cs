using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {
    public GameObject bulletPrefab;     // prefab of the bullet used by this gun
    public GameObject secondBullet;     //prefab of 2nd bullet used if any

    public Text ammoText;               // displayes current ammo left in player's mag in UI
    public int clipSize;                // How many bullets in a mag

    public int currentClip;             // How many bullets left in the mag
    public int bulletLeft;              // Total amount of bullets that can be carried minus mag
    public string gunType;              // "Semi", "Auto", "Bolt", dafault is semi
    public int fireInterval;
    public int reloadTime;              //determines reload time
    int counter;

    // Use this for initialization
    void Start ()
    {
        currentClip = clipSize;
        setAmmoCounter();
        counter = 0;
    }


    //fires the bullet
    public void fireBullet()
    {   //Clone of the bullet

        if (currentClip > 0)
        {
            Vector3 sp = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
            Vector3 dir = (Input.mousePosition - sp);

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(dir, new Vector2(0, 1));

            if (dir.x >= 0)
            {
                angle = -angle;
            }

            Instantiate(bulletPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            currentClip--;
        }

        setAmmoCounter();
    }

    //method called when player presses R
    //To-do: implement reload time
    public void reload()
    {
        if (bulletLeft < (clipSize - currentClip))
        {
            currentClip += bulletLeft;
            bulletLeft = 0;

        }
        else
        {
            bulletLeft -= (clipSize - currentClip);
            currentClip = clipSize;
        }
        setAmmoCounter();
    }

    //diff attack for the different gun type
    public void attack()
    {
        switch (gunType)
        {
            case "auto":
                if(counter >= fireInterval)
                {
                    fireBullet();
                    counter = -1;
                }
                
                break;

            case "lastBullet":
                if (currentClip == 1)
                {
                    fireSecondBullet();
                    counter = -1;
                }
                if (counter >= fireInterval)
                {
                    fireBullet();
                    counter = -1;
                }
                break;

            default:
                if (Input.GetMouseButtonDown(0) && counter >= fireInterval) { 
                   fireBullet();
                   counter = -1;
                }
                break;
        }
        counter++;
    }

    //Updates the text UI which counts the ammo left in the mag & the total bullet left not in mag
    public void setAmmoCounter()
    {
        if(ammoText != null)
        ammoText.text = currentClip.ToString() + " / " + bulletLeft.ToString();
    }

    void fireSecondBullet()
    {
        if (currentClip > 0)
        {
            Vector3 sp = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
            Vector3 dir = (Input.mousePosition - sp);

            //calculates the angle the shotgun bullet prefab must be rotated to face the player
            float angle = Vector2.Angle(dir, new Vector2(0, 1));

            if (dir.x >= 0)
            {
                angle = -angle;
            }

            Instantiate(secondBullet, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            currentClip--;
        }

        setAmmoCounter();
    }

}
