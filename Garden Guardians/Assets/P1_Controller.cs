using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Controller : MonoBehaviour {

    public int border = 10; //size of the board for calculating borders
 
    Vector3 initial;
    Vector3 pos;                                // For movement
    public float speed = 5.0f;                         // Speed of movement
    public int player = 0;

    Vector3 Last_Move;

    GameObject P2;
    void Start()
    {
        initial = transform.position;
        pos = transform.position;          // Take the initial position
        P2 = GameObject.FindGameObjectWithTag("P2");
    }



    void FixedUpdate()
    {
        if (transform.position == P2.transform.position)
        {
            fallbackp1();
            P2.GetComponent<P2_Controller>().fallbackp2();
        }

        if ((Input.GetKey(KeyCode.A) && transform.position == pos) && (pos + Vector3.left != P2.transform.position))
        {        // Left
            if ((pos + Vector3.left).x >= initial.x)
            {
                Last_Move = pos;
                pos += Vector3.left;
            }
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right

            if ((((pos+Vector3.right).x) <=initial.x + border-1) && (pos + Vector3.right != P2.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.right;
            }
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            if(((pos + Vector3.up).y <= initial.y)&& (pos + Vector3.up != P2.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.up;
            }
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            if (((pos + Vector3.down).y >= initial.y - (border-1)) && (pos + Vector3.down != P2.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.down;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }

    public void fallbackp1()
    {
        pos = Last_Move;
    }
}
