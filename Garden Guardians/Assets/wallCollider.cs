using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "P1")
		{
			col.GetComponent<P1_Controller>().fallbackp1();
		}
		else if (col.tag == "P2")
		{
			col.GetComponent<P2_Controller>().fallbackp2();
		}
	}
}
