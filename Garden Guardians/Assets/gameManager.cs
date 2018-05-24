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
    Text time;
	// Use this for initialization
	void Start () {
		elapsed = duration;
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        scrMngr = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}
	

	// Update is called once per frame
	void Update () {
        if(timer < 0)
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
		if (fadeWait > 0)
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
				GetComponent<Light>().enabled = false;
            start = true;
		}
	}

	private void fadeIn()
	{
		GetComponent<Light>().enabled = true;
		fadein = true;
	}

    public void endRound(string winner)
    {
        timer = 60;
        scrMngr.roundWinner = winner;
        fadeIn();
    }
}
