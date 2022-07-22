using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour

{
    private GameObject player;
    public SpriteRenderer Up, RightUp, Right,RightDown,Down,LeftDown,Left,LeftUp;
    private float orientation = 1f;
    private Rigidbody2D Rightrb;
    private SpriteRenderer sr;
    private GameObject RightUpV;
    


    // Start is called before the first frame update
    void Start()
    {
        RightUpV = GameObject.Find("RightUp");
        Rightrb = Right.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(orientation);
        if (orientation == 1)
        {
            RightUp.flipX = false;
            Right.flipX = false;
            RightDown.flipX = false;
        }
        else if (orientation == -1)
        {
            RightUp.flipX = true;
            //Vector3 SwapPos = new Vector3 (RightUpV.transform.position.x,RightUpV.transform.position.y,RightUpV.transform.rotation.z + 90);
            RightUpV += (0,0,RightUpV.transform.rotation.z + 90);
            Right.flipX = true;
            RightDown.flipX = true;
        }
        StartCoroutine(Point(GetDirection()));
       
    }
    private Vector2 GetDirection()
    {
        Vector2 result = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            result += new Vector2(1, 0);
            orientation = 1;

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            result += new Vector2(-1, 0);
            orientation = -1;

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
            return transform.right * orientation;
        }
        Debug.Log(result);
        return result.normalized;
 
    }
    private IEnumerator Point(Vector2 direction)
    {
        Debug.Log("working");
          if (direction == new Vector2(1, 1).normalized || direction == new Vector2(-1, 1).normalized)
        {
            
        }
        else if (direction == new Vector2(1, 0).normalized || direction == new Vector2(-1, 0).normalized)
        {
            
        }
        else if (direction == new Vector2(1, -1).normalized || direction == new Vector2(-1, -1).normalized)
        {
            
        }
        else if (direction == new Vector2(0, 1).normalized)
        {
           
        }
        else if (direction == new Vector2(0, -1).normalized)
        {

        }
        yield return new WaitForSeconds(0f);
    }
}
