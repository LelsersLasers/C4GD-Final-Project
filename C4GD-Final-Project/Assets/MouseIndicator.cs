using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    public GameObject Pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      Vector2 mousePos = Input.mousePosition;
    //  mousePos.x = mousePosition.x- transform.position.x;
    //mousePos.y = mousePosition.y- transform.position.y;

    }
}
