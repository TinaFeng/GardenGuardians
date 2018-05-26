using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour {

	public float duration = 1;
	public bool fade = true;
	public float fadeWait = 3;
	private float elapsed;
	private bool fadein = false;

    private ScoreManager scrMngr;


    bool start = false;
    public float timer = 60;
    private bool timerEnded = false;

	private string winner;

	private bool ended = false;
    Text time;
	// Use this for initialization
	void Start () {
		elapsed = duration;
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        scrMngr = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		scrMngr.p1Score = GameObject.Find("P1Score").GetComponent<Text>();
		scrMngr.p2Score = GameObject.Find("P2Score").GetComponent<Text>();
		scrMngr.timer = time;
	}
	

	// Update is called once per frame
	void Update () {
		if (ended)
		{
			return;
		}
        if(timer < 0 && !timerEnded)
        {
            timerEnded = true;
            endRound("P1");
        }
        if (start && !timerEnded)
        {
            timer -= Time.deltaTime;
            time.text = "Time: " + Mathf.RoundToInt(timer).ToString();
            if (Mathf.RoundToInt(timer)<= 10)
            {
                time.color = Color.red;
            }
        }
		else if (fadeWait > 0)
		{
			fadeWait-= Time.deltaTime;
		}
		else if ((elapsed > 0 && !fadein) || (fadein && elapsed < duration))
		{
			if (fadein)
				elapsed +=Time.deltaTime;
			else
				elapsed -=Time.deltaTime;
			if (fade)
			{
				GetComponent<Light>().intensity = elapsed/duration;
			}
		}
		else
		{
			if (!fadein)
			{
				GetComponent<Light>().enabled = false;
				start = true;

			}
			else if (!ended)
			{
				ended = true;
				scrMngr.roundWinner = winner;
			}
			
			
            
		}
	}

	private void fadeIn()
	{
		GetComponent<Light>().enabled = true;
		fadein = true;
	}

    public void endRound(string w)
    {
		if (start)
		{
			winner = w;
			start = false;
			elapsed = 0;
			
			scrMngr.updateTimer(w=="P1");
		
			fadeIn();
		}
    }
}
