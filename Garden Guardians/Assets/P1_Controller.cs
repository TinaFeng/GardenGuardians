using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P1_Controller : MonoBehaviour {

    public int border = 10; //size of the board for calculating borders
 
    Vector3 initial;
    Vector3 pos;                                // For movement
    public float speed = 5.0f;                         // Speed of movement
    public int player = 0;
    public LayerMask mask;
    public GameObject Inventory;  //the display
    private bool spriteOn = false;
    private bool spotting = false;
    Vector3 Last_Move;

    GameObject P2;
    Light Lantern;

    List<string> Item_List;
    public GameObject Sensor;
    public GameObject Bomb;
    List<GameObject> Item_names;
//    public GameObject Blocker;
    void Start()
    {
        initial = transform.position;
        pos = transform.position;          // Take the initial position
        P2 = GameObject.FindGameObjectWithTag("P2");
        Lantern = transform.GetChild(0).GetComponent<Light>();
        Item_List = new List<string>() ;

        Item_names = new List<GameObject>();
        Item_names.Add(Sensor.gameObject);
        Item_names.Add(Bomb.gameObject);
    }



    void Update()
    {
        vision();
        //item dropping mechanics
        if (Input.GetKeyDown(KeyCode.Z)) // 1
        {
            SetupItem(0);
        }

        if (Input.GetKeyDown(KeyCode.X)) // 1
        {
            SetupItem(1);
        }
        if (Input.GetKeyDown(KeyCode.C)) // 1
        {
            SetupItem(2); 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLantern();
        }

        GetComponent<SpriteRenderer>().enabled = spriteOn || Lantern.enabled;

        if (transform.position == P2.transform.position)
        {
            fallbackp1();
            P2.GetComponent<P2_Controller>().fallbackp2();
        }

        if ((Input.GetKey(KeyCode.A) && transform.position == pos) )
        {        // Left
            if (((pos + Vector3.left).x >= initial.x)&&checkDirection(Vector2.left)&& (pos + Vector3.left != P2.transform.position))
            {
                Last_Move = pos;
                pos += Vector3.left;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",3);
            }
        }
        else if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right

            if ((((pos+Vector3.right).x) <=initial.x + border-1) && (pos + Vector3.right != P2.transform.position) &&checkDirection(Vector2.right)) 
            {
                Last_Move = pos;
                pos += Vector3.right;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",1);
            }
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            if(((pos + Vector3.up).y <= initial.y)&& (pos + Vector3.up != P2.transform.position)&&checkDirection(Vector2.up))
            {
                Last_Move = pos;
                pos += Vector3.up;
                if (!GetComponent<Animator>().GetBool("Walking")) GetComponent<Animator>().SetBool("Walking",true);
                GetComponent<Animator>().SetInteger("Direction",2);
            }
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            if (((pos + Vector3.down).y >= initial.y - (border-1)) && (pos + Vector3.down != P2.transform.position)&&checkDirection(Vector2.down))
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
    public void fallbackp1()
    {
        pos = Last_Move;
    }

    private bool checkDirection(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)pos,dir*2f,1f,mask);


        if (hit.collider != null && (hit.collider.gameObject.tag== "Sensor"|| hit.collider.gameObject.tag =="Bomb")) 
        {
            //pick up  
            pickup(hit.collider.gameObject);
        }
        if (hit.collider != null && (hit.collider.gameObject.tag == "Walls" || hit.collider.gameObject.tag == "P2"))
        {
            return false;
        }
        return true;

    }

    void vision()

    {

        RaycastHit2D light_check = Physics2D.Raycast((Vector2)pos, (Vector2)P2.transform.position - (Vector2)pos, 2f,mask);
       
            if (Lantern.enabled && light_check.collider != null && light_check.collider.gameObject.tag == "P2")
            {
                P2.GetComponent<P2_Controller>().spotted(true);
                spotting = true;
            }
            else if (spotting)
            {
                P2.GetComponent<P2_Controller>().spotted(false);
                spotting = false;
            }


    }

    public void spotted(bool s)
    {
        spriteOn = s;
        //GetComponent<SpriteRenderer>().enabled = true;
    }


    void pickup(GameObject item)
    {
        if (Item_List.Count <3) // if we still has room
        {
            Item_List.Add(item.tag);
            Destroy(item.gameObject);
           Inventory.transform.GetChild(Item_List.LastIndexOf(item.tag) + 1).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void SetupItem(int index)
    {
        Debug.Log(Item_List[index]);
        GameObject trap = Instantiate(FindPrefab(Item_List[index]));
        trap.transform.position = this.transform.position;
        Item_List.Remove(Item_List[index]);
        
        Inventory.transform.GetChild(index+1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item_Empty");
    }
    
    void UpdateInventory()
    {
        for(int i = 0; i!= Item_List.Count; i++)
        {
            Inventory.transform.GetChild(i+ 1).GetComponent<Image>().sprite = FindPrefab(Item_List[i]).GetComponent<SpriteRenderer>().sprite;
        }
    }


    GameObject FindPrefab(string name)
    {

        for (int i = 0; i!= Item_names.Count; i++)
        {
    
            if (Item_names[i].name == name)
                return Item_names[i];
        }
        return null;
    }

}

