using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour {

    public GameObject bomb_prefab;
	public int bombs = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && bombs > 0)
        {
			GameObject bomb = Instantiate(bomb_prefab);
            bomb.transform.position = transform.position;
			bomb.GetComponent<Bomb_Behavior>().t = this;
			bombs--;
        }
	
	}
}
