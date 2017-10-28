using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour {

    public Text timerDisplay;
    public int playtime = 0;
    private int seconds;
    private int minutes;
	// Use this for initialization
	void Start () {

        StartCoroutine("Playtimer");
	
	}

    private IEnumerator Playtimer(){
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime++;
            seconds = playtime % 60;
            minutes = (playtime / 60) % 60;
            setTimer();
        }
    }
	
    void setTimer()
    {
        timerDisplay.text = "Time - " +  minutes + ":" + seconds;
    }
}
