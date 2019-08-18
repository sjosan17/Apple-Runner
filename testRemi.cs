using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    // For End Menu
    public GameObject resetBtn;
    // Top Right Corner
    private int score = 0;
    // Top Right Corner
    public Text scoreText;
    // For End Menu
    public Text newHighScore;
    // For End Menu, change background to purple
    public Image background;
    // For End Menu, change letters to white
    public Text[] scoreTexts;

    // Use this for initialization
    void Start () {
        isDead = false;
        dir = Vector3.zero; // Make sure the player starts when ready.
        mainTheme.Play();
	}
	
	// Update is called once per frame
	void Update () {
        // Quit to Start Menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
        }

        // To move the player
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            isPlaying = true;
            score++;
            scoreText.text = score.ToString();
	}

    // Trigger for extra pickup bonus points
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedApple") // ps from ParticleScript
        {
            other.gameObject.SetActive(false);
            score+= 3;
            scoreText.text = score.ToString();
        }

        if (other.tag == "GoldenApple") // ga from GoldenAppleParticleScript
        {
            other.gameObject.SetActive(false);
            score += 10; //Spawns rarer, but more points
            scoreText.text = score.ToString();
        }
    }

    // The End Menu
    private void GameOver()
    {
        scoreTexts[1].text = score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            newHighScore.gameObject.SetActive(true);
            background.color = new Color32(53, 180, 255, 255);
            foreach (Text txt in scoreTexts)
            {
                txt.color = Color.white;
            }
        }
        scoreTexts[3].text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
}
