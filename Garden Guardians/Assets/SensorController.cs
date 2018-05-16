using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour {


    public float imageFadeInTime, solidTime, imageFadeOutTime;

    private float currentTime = 0;
    private SpriteRenderer sr;
    private enum SensorState {Idle, FadingIn, Solid, FadingOut};
    private SensorState state;
    private Color col;

    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        col = sr.color;
        currentTime = 0;
        state = SensorState.Idle;
    }
	
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(state == SensorState.Idle && (col.tag == "P1" || col.tag == "P2"))
        {
            activate();
        }
    }

	// Update is called once per frame
	void Update () {
        if(state == SensorState.Idle)
        {
            return;
        }
        //check for state updates
        if (state == SensorState.FadingIn && currentTime >= (imageFadeInTime))
        {
            state = SensorState.Solid;
            col.a = 1;
            sr.color = col;
        }
        else if (state == SensorState.Solid && currentTime >= (imageFadeInTime + solidTime))
        {
            state = SensorState.FadingOut;
        }
        else if (state == SensorState.FadingOut && currentTime >= (imageFadeInTime + solidTime + imageFadeOutTime))
        {
            state = SensorState.Idle;
            col.a = 0;
            sr.color = col;
            currentTime = 0;
        }
        
        //update current time first then enact change according to the state
        currentTime += Time.deltaTime;

        if (state == SensorState.FadingIn)
        {
            col.a = currentTime / imageFadeInTime;
            sr.color = col;
        }
        else if (state == SensorState.Solid)
        {
            //do nothing in this state
        }
        else if (state == SensorState.FadingOut)
        {
            col.a = (imageFadeInTime + solidTime  + imageFadeOutTime - currentTime) / imageFadeOutTime;
            sr.color = col;
        }
	}

    public void activate()
    {
        state = SensorState.FadingIn;
    }
}
