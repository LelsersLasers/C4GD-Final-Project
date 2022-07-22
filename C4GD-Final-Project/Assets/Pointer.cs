using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour

{
    public PlayerController player;
    public GameObject Up, RightUp, Right,RightDown,Down,LeftDown,Left,LeftUp;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Point(GetDirection());
       
    }
    private Vector2 GetDirection()
    {
        Vector2 result = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            result += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            result += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            result += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            result += new Vector2(0, -1);
        }
        if (result == Vector2.zero)
        {
            return transform.right * player.orientation;
        }
        return result.normalized;
    }
    private void Point(Vector2 direction)
    {
        RightUp.SetActive(false);
        LeftUp.SetActive(false);
        Right.SetActive(false);
        Left.SetActive(false);
        RightDown.SetActive(false);
        LeftDown.SetActive(false);   
        Up.SetActive(false);
        Down.SetActive(false);        



        if (direction == new Vector2(1, 1).normalized)
        {
            RightUp.SetActive(true);
        }  
        else if (direction == new Vector2(-1, 1).normalized) {
            LeftUp.SetActive(true);
        }
        else if (direction == new Vector2(1, 0).normalized ) 
        {
        Right.SetActive(true);
        }
        else if (direction == new Vector2(-1, 0).normalized)
        {
        Left.SetActive(true);
        }
        else if (direction == new Vector2(1, -1).normalized)
        {
        RightDown.SetActive(true);
        }
        else if(direction == new Vector2(-1, -1).normalized)
        {
         LeftDown.SetActive(true);   
        }
        else if (direction == new Vector2(0, 1).normalized)
        {
           Up.SetActive(true);
        }
        else if (direction == new Vector2(0, -1).normalized)
        {
            Down.SetActive(true);        }
    }


}
