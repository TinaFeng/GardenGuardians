using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Controller : MonoBehaviour {

    public int border = 10; //size of the board for calculating borders
 
    Vector3 initial;
    Vector3 pos;                                // For movement
    public float speed = 5.0f;                         // Speed of movement
    public int player = 0;
    public LayerMask mask;
    Vector3 Last_Move;

    GameObject P2;
    GameObject Lantern;

    GameObject[] Item_List;
    void Start()
    {
        initial = transform.position;
        pos = transform.position;          // Take the initial position
        P2 = GameObject.FindGameObjectWithTag("P2");
        Lantern = transform.GetChild(0).gameObject;
        Item_List = new GameObject[3];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void FixedUpdate()
    {
        if (Lantern.activeInHierarchy)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
            GetComponent<SpriteRenderer>().enabled = false;
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLantern();
        }

        if (transform.position == P2.transform.position)
        {
            fallbackp1();
            P2.GetComponent<P2_Controller>().fallbackp2();
        }

        if ((Input.GetKey(KeyCode.A) && transform.position == pos) && (pos + Vector3.left != P2.transform.position))
        {        // Left
            if (((pos + Vector3.left).x >= initial.x)&&checkDirection(Vector2.left))
            {
                Last_Move = pos;
                pos += Vector3.left;
            }
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right

            if ((((pos+Vector3.right).x) <=initial.x + border-1) && (pos + Vector3.right != P2.transform.position) &&checkDirection(Vector2.right)) 
            {
                Last_Move = pos;
                pos += Vector3.right;
            }
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            if(((pos + Vector3.up).y <= initial.y)&& (pos + Vector3.up != P2.transform.position)&&checkDirection(Vector2.up))
            {
                Last_Move = pos;
                pos += Vector3.up;
            }
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            if (((pos + Vector3.down).y >= initial.y - (border-1)) && (pos + Vector3.down != P2.transform.position)&&checkDirection(Vector2.down))
            {
                OnItemhit(Vector2.down);
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
    public void fallbackp1()
    {
        pos = Last_Move;
    }

    private bool checkDirection(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)pos,dir*2f,1f,mask);

        if (hit.collider != null && hit.collider.gameObject.tag == "Walls")
        {
            return false;
        }
        return true;

    }
    
    void OnItemhit(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)pos, dir * 2f, 1f);
        if (hit.collider != null && hit.collider.gameObject.tag == "Item")
        {
            Debug.Log("yo");
        }

    }


}

