using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public Text winText;
    public Text finalScoreText;
    public Text restartText;
    public int scoreValue = 0;
    public int livesValue = 5;

	private bool gameOver;
	private bool restart;

    private PlayerController playerController;

    // Use this for initialization
    void Start () 
	{
        // Finding GameController game object to access methods in GameController script 
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }

        this.gameOverText.enabled = false;
        this.winText.enabled = false;
		this.finalScoreText.enabled = false;
		this.restartText.enabled = false;

        UpdateScore();
        UpdateLives();
	}
	
	/// Pree "R" to reset
	void Update()
	{
		if (restart) 
		{
			if(Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	// When player kills a enemy or collects a jewel
    public void GainScore(int newScoreValue)
    {
        scoreValue += newScoreValue;
        UpdateScore();
    }

    // Updates score value from above
    public void UpdateScore()
    {
        scoreText.text = "Score: " + scoreValue;
    }

	// When player collides with a Enemy
	public void LoseLife(int newLifeValue)
	{
		livesValue -= newLifeValue;
		UpdateLives();
	}
	
	// Updates lives value from above
	public void UpdateLives()
	{
		livesText.text = "Lives: " + livesValue;

		if (livesValue <= 0) 
		{
			playerController.killPlayer(); // Reference to PlayerController to destroy player game object
			
			this.GameOver ();
		}
	}

	// When lives value hits zero
	private void GameOver()
	{
		gameOver = true;
		this.scoreText.enabled = false;
		this.livesText.enabled = false;
		this.gameOverText.enabled = true;
		this.finalScoreText.enabled = true;
		this.finalScoreText.text = "Final Score: " + this.scoreValue;
		
		//Meassge to press "R" to play again
		if (gameOver)
		{
			this.restartText.enabled = true;
			this.restartText.text = "Did you find the 3 secrets? Press 'R' to play again";
			restart = true;
		}
	}

    public void youWin()
    {
        gameOver = true;
        this.scoreText.enabled = false;
        this.livesText.enabled = false;
        this.winText.enabled = true;
        this.finalScoreText.enabled = true;
        this.finalScoreText.text = "Final Score: " + this.scoreValue;

        //Meassge to press "R" to play again
        if (gameOver)
        {
            this.restartText.enabled = true;
            this.restartText.text = "Did you find the 3 secrets? Press 'R' to play again";
            restart = true;
        }
    }
	


}
