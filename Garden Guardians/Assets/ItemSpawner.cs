using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	// Use this for initialization
	public GameObject Item;

	public gameManager gM;

	private float time = 60f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gM.timer <= time -10f)
		{
			if (this.transform.childCount == 0)
			{
				Spawn();
			}
			time = gM.timer;
		}
		
	}

	void Spawn()
	{
		GameObject spawn = Instantiate(Item);
        spawn.transform.position = this.transform.position;
		spawn.transform.SetParent(this.transform);
	}
}
