using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour {

    public GameObject bomb_prefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bomb_prefab).transform.position = transform.position;

        }
	
	}
}
