using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text livesText;
    //public Text gameOverText;
    //public Text finalScoreText;
    //public Text restartText;
    public int scoreValue = 0;
    public int livesValue = 5;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
	}

}
