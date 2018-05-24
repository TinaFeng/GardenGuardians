using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    private int p1Wins = 0, p2Wins = 0, totalMatches = 0;
    public string roundWinner = "";
    private bool flipped = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
            OnGameEnd(gameEnd());
        }
        
    }

    public void OnGameEnd(string winner)
    {
        if(winner != "tie")
        {
            Debug.Log(winner + " wins!");
        }
        else
        {
            Debug.Log("Tie game.");
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
                winRound(flipped ? "P2" : "P1");
                resetLevel();
            }
            else if (roundWinner == "P2")
            {
                winRound(flipped ? "P1" : "P2");
                resetLevel();
            }
            else
            {
                Debug.LogError("\"" + roundWinner + " \" is an invalid string value for roundWinner variable in ScoreManager.");
            }
        }
	}
}
