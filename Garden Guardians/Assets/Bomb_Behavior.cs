using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Behavior : MonoBehaviour {

    public float explosion_countdown;
    public float effects_countdown;
    public trap t;
    float spawn_time;
    float counter;
    bool boom = false;
	// Use this for initialization
	void Start () {

        spawn_time = Time.time;
        counter = spawn_time;
        foreach (var c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
    }
	
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "P1")
        {
            col.gameObject.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if ((counter - spawn_time)>= explosion_countdown)
        {
         
            if (boom == false)
            {
                foreach (var c in GetComponents<Collider2D>())
                {
                    c.enabled = true;
                }
                boom = true;
                explosion_countdown += effects_countdown;
            }
            else
            {
                t.bombs ++;
                Destroy(this.gameObject);
            }

        }



    }
}
