using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text p1Score, p2Score, timer;
    private int p1Wins = 0, p2Wins = 0, totalMatches = 0;
    public string roundWinner = "";
    private bool flipped = false;
    private static bool alreadyExists = false;
    

    void Awake()
    {
        if (!alreadyExists)
        {
            alreadyExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
    public void resetLevel()
    {
        roundWinner = "";
        if(gameEnd() == "notEnded")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            flipped = !flipped;
        }
        else
        {
            Debug.Log(" wins a round.");
            OnGameEnd(gameEnd());
        }
        
    }

    public void OnGameEnd(string winner)
    {
        if(winner != "tie")
        {
            Debug.Log(winner + " wins a round.");
            timer.text = (winner + " wins!");
        }
        else
        {
            timer.text = ("Tie game.");
        }
        
    }

    public string gameEnd()
    {
        if(totalMatches < 4)
        {
            return "notEnded";
        }
        else
        {
            if (p2Wins == p1Wins)
            {
                return "tie";
            }
            else
            {
                return (p2Wins > p1Wins) ? "Player 2" : "Player 1";
            }
        }
    }

    private void winRound(string winner)
    {
        totalMatches++;
        Debug.Log(winner + " wins a round.");
        if (winner == "P1")
        {
            p1Wins++;
        } else {
            p2Wins++;
        }
    }

	// Update is called once per frame
	void Update () {
        if (roundWinner != "")
        {
            if (roundWinner == "P1")
            {
                roundWinner = "";
                winRound(flipped ? "P2" : "P1");
                resetLevel();
            }
            else if (roundWinner == "P2")
            {
                roundWinner = "";
                winRound(flipped ? "P1" : "P2");
                resetLevel();
            }
            else
            {
                Debug.LogError("\"" + roundWinner + " \" is an invalid string value for roundWinner variable in ScoreManager.");
            }
        }

        p1Score.text = (flipped ? "P2: " + p2Wins :  "P1: " + p1Wins);
        p2Score.text = (!flipped ? "P2: " + p2Wins :  "P1: " + p1Wins);

	}

    public void updateTimer(bool p1)
    {
        p1 = (flipped? !p1: p1);
        if (totalMatches < 3)
        {
            
            timer.text = (p1 ? "P1":"P2") + " scores! Switch places!";
        }
        else
            timer.text = (p1 ? "P1":"P2") + " scores!";
    }
}
