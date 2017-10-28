    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;

public class menuScript : MonoBehaviour {
	public GameObject pauseMenu;
    public GameObject quitMenu;
    public GameObject mainMenu;
    public GameObject restartMenu;
	public GameObject creditMenu;
    public Button play;
    public Button restart;
    public Button exit;
    public Button exit2;
    public Button yesQuit;
    public Button noQuit;
    public Button yesRestart;
    public Button noRestart;
    public Button resume;
	public Button credit;
	public Button creditBack;
    public bool shouldActive;

    void Start()
    {
        //quitMenu = quitMenu.GetComponent<Canvas>();
        //mainMenu = mainMenu.GetComponent<Canvas>();
        //pauseMenu = pauseMenu.GetComponent<GameObject>();
        //restartMenu = restartMenu.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        restart = restart.GetComponent<Button>();
        exit = exit.GetComponent<Button>();
        exit2 = exit2.GetComponent<Button>();
        yesQuit = yesQuit.GetComponent<Button>();
        noQuit = noQuit.GetComponent<Button>();
        yesRestart = yesRestart.GetComponent<Button>();
        noRestart = noRestart.GetComponent<Button>();
        resume = resume.GetComponent<Button>();
		credit = credit.GetComponent<Button>();
		creditBack = creditBack.GetComponent<Button>();
        mainMenu.SetActive(shouldActive);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        restartMenu.SetActive(false);
		creditMenu.SetActive (false);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
	public void Unpause(){
		pauseMenu.SetActive(false);
		Time.timeScale = 1;	
	}
    public void Startlevel() {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }
    public void ExitGame() {
        Application.Quit();
    }
	public void Quitting(){
		quitMenu.SetActive(true);
    }
	public void Restarting(){
		restartMenu.SetActive(true);
    }
    public void NoPressQuit() {
        quitMenu.SetActive(false);
    }
    public void NoPressRestart()
    {
        restartMenu.SetActive(false);
        WeaponAttack playerScript = GameObject.FindWithTag("Player").GetComponent<WeaponAttack>();
        WinManager winScript = GameObject.FindWithTag("Manager").GetComponent<WinManager>();
        if (playerScript.hp <= 0 || winScript.wonGame)
        {
            SceneManager.LoadScene("MenuScene");
        }

    }
	public void CreditPress(){
		creditMenu.SetActive (true);
	}
	public void CreditBack(){
		creditMenu.SetActive (false);
	}

        


}
