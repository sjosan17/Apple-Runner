using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    // Speed for movement, faster = harder game
    public float speed;
    // Main movement input
    private Vector3 dir;
    // Pickup effect for RedApple
    public GameObject ps;
    // Pickup effect for GoldenApple
    public GameObject ga;
    // For stopping the player moving the character after game over.
    private bool isDead;
    // For End Menu
    public GameObject resetBtn;
    // Top Right Corner
    private int score = 0;
    // Top Right Corner
    public Text scoreText;
    // For End Menu
    public Animator gameOverAnim;
    // For End Menu
    public Text newHighScore;
    // For End Menu, change background to purple
    public Image background;
    // For End Menu, change letters to white
    public Text[] scoreTexts;
    // What is ground for the player
    public LayerMask whatIsGround;
    // Indicates if the player is playing the game
    private bool isPlaying = false;
    // A ref to the contactPoint(GameObject)
    public Transform contactPoint;

    /// <summary>
    /// Sound and Music
    /// </summary>
    // Main theme
    public AudioSource mainTheme;
    // Red Apple pickup
    public AudioSource redApple;
    // Golden Apple pickup
    public AudioSource goldenApple;
    // Death sound
    public AudioSource deathSound;
    // Bool to make deathsound only play once
    bool hasPlayed = true;

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

        // From former OnTriggerExit
        if (!IsGrounded() && isPlaying)
        {
            isDead = true;
            mainTheme.Stop();
            GameOver();
            resetBtn.SetActive(true); // Activate retry bytton in the End Menu
        }

        // To move the player
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            isPlaying = true;
            score++;
            scoreText.text = score.ToString();

            // First clikc will move the player forward, the next clicks will only change diraction.
            if (gameObject.transform.rotation.eulerAngles.y == 90 || gameObject.transform.rotation.eulerAngles.y == 0)
            {
                transform.Rotate(0, -90, 0);
            }
            else
            {
                transform.Rotate(0, 90, 0);
                dir = Vector3.forward;
            }
        }

        float amountToMove = speed * Time.deltaTime;

        transform.Translate(dir * amountToMove);
	}

    // Trigger for extra pickup bonus points
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedApple") // ps from ParticleScript
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score+= 3;
            scoreText.text = score.ToString();
            redApple.Play();
        }

        if (other.tag == "GoldenApple") // ga from GoldenAppleParticleScript
        {
            other.gameObject.SetActive(false);
            Instantiate(ga, transform.position, Quaternion.identity);
            score += 10; //Spawns rarer, but more points
            scoreText.text = score.ToString();
            goldenApple.Play();
        }
    }

    // The End Menu
    private void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");

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
        // Played the deathSound only once
        if(hasPlayed == true)
        {
            deathSound.Play();
            hasPlayed = false;
        }
    }

    // To check if player has not fallen off
    private bool IsGrounded()
    {
        Collider[] Colliders = Physics.OverlapSphere(contactPoint.position, 0.5f, whatIsGround); // whatIsGround = everything/ all other tags.
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
