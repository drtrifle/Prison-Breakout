using UnityEngine;
using System.Collections;

public class BossMusic : MonoBehaviour {

    public AudioSource sound;
    public AudioClip bossclip;

	// Use this for initialization
	void Start () {
        sound.clip = bossclip;
        sound.Play();
	
	}
}
