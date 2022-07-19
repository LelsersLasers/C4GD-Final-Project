using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformConfigure : MonoBehaviour
{
    public float distance;
    public GameObject Left;
    public GameObject Middle;
    public GameObject Right;

    // Start is called before the first frame update
    void Start()
    {
        Left = transform.Find("Platform Left").gameObject;
        Middle = transform.Find("Platform Middle").gameObject;
        Right = transform.Find("Platform Right").gameObject;
        Middle.transform.localScale = new Vector2 (distance,1);
        Middle.transform.position = new Vector2(Left.transform.position.x + 1, Left.transform.position.y);
        Right.transform.position = new Vector2(Left.transform.position.x + distance + 1, Left.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
