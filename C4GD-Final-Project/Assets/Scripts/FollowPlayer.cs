using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private GameObject Tavern;
    private SpriteRenderer TavernRend;
    public GameObject Box;
    private SpriteRenderer BoxRend; 
    
    public float xSpeed = 2f;
    public float ySpeed = 4f;
    private Vector3 MiddleVector, temp;
    
    private float MiddlePointX, dist;
    private float startpos;

  
    public Vector3 currentOffset;
    public Vector3 targetOffset = new Vector3(0f, 6f, -10f);
   

    void Start()
    {
        
        Tavern = GameObject.Find("TavernInt");
        player = GameObject.Find("Player");
        
        BoxRend = Box.GetComponent<SpriteRenderer>();
        TavernRend = Tavern.GetComponent<SpriteRenderer>();
        startpos  = player.transform.position.x;
        MiddlePointX = BoxRend.bounds.size.x/2;

        

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetOffset = new Vector3(0f, 4f, -10f);
        Vector3 currentOffset = transform.position - player.transform.position;
        Vector3 move = (targetOffset - currentOffset) * Time.deltaTime;
        
        dist = startpos - player.transform.position.x;
        


        //Debug.Log(dist);
   
         xSpeed = 0f;
        //Debug.Log(dist);
        if (dist >= MiddlePointX-.5||dist <= -MiddlePointX+.5)
        {
            targetOffset = new Vector3(0f, player.transform.position.y, -10f);
            currentOffset = transform.position - player.transform.position;
            currentOffset = new Vector3 (0,transform.position.y,player.transform.position.z);
            xSpeed = 2f;
            StartCoroutine(faststopx());
            move.x *= 0;
            move.y *= ySpeed;
            move = (targetOffset - currentOffset) * Time.deltaTime;
            transform.position += move;
          
        }
        else
        {
            currentOffset = transform.position - player.transform.position;
            xSpeed = 2f;
            move.x *= xSpeed;
            move.y *= ySpeed;
            move = (targetOffset -  currentOffset) * Time.deltaTime;
            transform.position += move;

            
        }
        IEnumerator faststopx()
        {
            for (float Speedx = xSpeed; Speedx >= 0; Speedx -= 0.2f)
            {    
            Debug.Log(Speedx);
            yield return new WaitForSeconds(0.3f);
            }
          
        }
    }
}
