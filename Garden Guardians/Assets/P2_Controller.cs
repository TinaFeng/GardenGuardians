using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Controller : MonoBehaviour
{

    public int border = 10; //size of the board for calculating borders

    Vector3 initial;
    Vector3 pos;                                // For movement
    public float speed = 5.0f;                         // Speed of movement
    public int player = 0;
    GameObject P1;

    Vector3 Last_Move;
    GameObject Lantern;
    void Start()
    {
        initial = transform.position;
        pos = transform.position;          // Take the initial position
        P1 = GameObject.FindGameObjectWithTag("P1");
        Lantern = transform.GetChild(0).gameObject;
    }


    void FixedUpdate()
    {
        if (Lantern.activeInHierarchy)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
            GetComponent<SpriteRenderer>().enabled = false;
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleLantern();
        }
        if (transform.position == P1.transform.position)
        {
            fallbackp2();
            P1.GetComponent<P1_Controller>().fallbackp1();
        }
        if (Input.GetKey(KeyCode.J) && transform.position == pos)
        {        // Left

            if (((pos + Vector3.left).x >= initial.x - (border - 1)) && (pos+Vector3.left != P1.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.left;
            }
        }
        if (Input.GetKey(KeyCode.L) && transform.position == pos)
        {        // Right

            if ((((pos + Vector3.right).x)  <= initial.x)&& (pos + Vector3.right != P1.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.right;
            }
        }
        if (Input.GetKey(KeyCode.I) && transform.position == pos)
        {        // Up
            if (((pos + Vector3.up).y <= initial.y + (border - 1))&& (pos + Vector3.up != P1.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.up;
            }
        }
        if (Input.GetKey(KeyCode.K) && transform.position == pos)
        {        // Down
            if (((pos + Vector3.down).y >= initial.y)&& (pos + Vector3.down != P1.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.down;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }
    void ToggleLantern()
    {
        if (Lantern.activeInHierarchy)
            Lantern.SetActive(false);
        else
            Lantern.SetActive(true);
    }
    public void fallbackp2()
    {
        pos = Last_Move;
    }
}
