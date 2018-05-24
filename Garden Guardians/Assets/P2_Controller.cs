using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2_Controller : MonoBehaviour
{

    public int border = 10; //size of the board for calculating borders

    Vector3 initial;
    Vector3 pos;                                // For movement
    public float speed = 5.0f;                         // Speed of movement
    public int player = 0;
    public LayerMask mask;
    public GameObject Inventory; 
    private bool spriteOn = false;
    private bool spotting = false;

    GameObject P1;
    public gameManager gM;
    Vector3 Last_Move;
    Light Lantern;
    Dictionary<int,string> Item_List;
    void Start()
    {
        initial = transform.position;
        pos = transform.position;          // Take the initial position
        P1 = GameObject.FindGameObjectWithTag("P1");
        Lantern = transform.GetChild(0).GetComponent<Light>();
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        Item_List = new Dictionary<int,string>();
    }

    void Update()
    {
        vision();

        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleLantern();
        }

        GetComponent<SpriteRenderer>().enabled = spriteOn || Lantern.enabled;

        if (transform.position == P1.transform.position)
        {
            fallbackp2();
            P1.GetComponent<P1_Controller>().fallbackp1();
        }

        if (Input.GetKey(KeyCode.J) && transform.position == pos)
        {        // Left

            if ((pos+Vector3.left != P1.transform.position)&&checkDirection(Vector2.left))
            {
                Last_Move = pos;
                pos += Vector3.left;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",3);
            }
        }
        else if (Input.GetKey(KeyCode.L) && transform.position == pos)
        {        // Right

            if ((pos + Vector3.right != P1.transform.position)&&checkDirection(Vector2.right))
            {
                Last_Move = pos;
                pos += Vector3.right;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",1);
            }
        }
        else if (Input.GetKey(KeyCode.I) && transform.position == pos)
        {        // Up
            if ((pos + Vector3.up != P1.transform.position)&&checkDirection(Vector2.up))
            {
                Last_Move = pos;
                pos += Vector3.up;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",2);
            }
        }
        else if (Input.GetKey(KeyCode.K) && transform.position == pos)
        {        // Down
            if ((pos + Vector3.down != P1.transform.position)&&checkDirection(Vector2.down))
            {
                Last_Move = pos;
                pos += Vector3.down;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",0);
            }
        }
        else if (transform.position == pos)
        {
            GetComponent<Animator>().SetBool("Walking",false);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }
    void ToggleLantern()
    {
        Lantern.enabled = !Lantern.enabled;
    }
    public void fallbackp2()
    {
        pos = Last_Move;
    }

    void vision()

    {

        RaycastHit2D light_check = Physics2D.Raycast((Vector2)pos, (Vector2)P1.transform.position - (Vector2)pos, 2f,mask);
       
            if (Lantern.enabled && light_check.collider != null && light_check.collider.gameObject.tag == "P1")
            {
                P1.GetComponent<P1_Controller>().spotted(true);
                spotting = true;
            }
            else if (spotting)
            {
                P1.GetComponent<P1_Controller>().spotted(false);
                spotting = false;
            }


    }

    void pickup(GameObject item)
    {
        if (Item_List.Count <3) // if we still has room
        {
            int i = 0;
            for(; i < 3;++i)
            {
                if (!Item_List.ContainsKey(i))
                {
                    Item_List[i] = item.tag;
                    break;
                }
            }
            
            Destroy(item.gameObject);
            Inventory.transform.GetChild(i + 1).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
        if (Item_List.Count == 3)
        {
            //Kobold Victory
            gM.fadeIn();
        }
    }


    public void spotted(bool s)
    {
        spriteOn = s;
        //GetComponent<SpriteRenderer>().enabled = true;
    }

    private bool checkDirection(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)pos,dir*2f,1f,mask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Flower")
        {
            pickup(hit.collider.gameObject);
        }
        if (hit.collider != null && (hit.collider.gameObject.tag == "Walls" || hit.collider.gameObject.tag == "P1"))
        {
            return false;
        }
        return true;

    }
}
