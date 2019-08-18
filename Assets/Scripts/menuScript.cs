using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button starText;
    public Button exitText;
    public AudioSource menuMusic;

    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        starText = starText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
        menuMusic.Play();
    }
	
	public void ExitPress() // Make sure the player can't clikc on Play and Exit buttons anymore.
    {
        quitMenu.enabled = true;
        starText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress() // Reactivates Play and Exit Buttons and cloese quitMenu.
    {
        quitMenu.enabled = false;
        starText.enabled = true;
        exitText.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
