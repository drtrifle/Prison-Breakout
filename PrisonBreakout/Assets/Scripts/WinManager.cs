using UnityEngine;
using System.Collections;

public class WinManager : MonoBehaviour {

    public GameObject winText;
    public bool wonGame;

    void Start()
    {
        wonGame = false;
    }
    public void win()
    {
        wonGame = true;
        winText.SetActive(true);
        menuScript ms = GameObject.FindWithTag("Menu").GetComponent<menuScript>();
        ms.Restarting();
        Time.timeScale = 0;          
    }
}
