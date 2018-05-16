using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public float duration = 1;
	public bool fade = true;
	public float fadeWait = 3;
	private float elapsed;
	private bool fadein = false;

	// Use this for initialization
	void Start () {
		elapsed = duration;
	}
	

	// Update is called once per frame
	void Update () {
		
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
			if (!fadeIn)
				GetComponent<Light>().enabled = false;
		}
	}

	public void fadeIn()
	{
		GetComponent<Light>().enabled = true;
		fadein = true;
	}
}
