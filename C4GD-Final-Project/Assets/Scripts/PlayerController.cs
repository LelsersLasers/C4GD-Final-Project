using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jump = 100f;
    private int jumps = 0;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontalInput = 0;
        horizontalInput -= Input.GetKey(KeyCode.A) ? 1 : 0;
        horizontalInput += Input.GetKey(KeyCode.D) ? 1 : 0;
        transform.Translate(horizontalInput * speed * Time.deltaTime, 0, 0);
        
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2)
        {
            jumps++;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || (collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<BoxCollider2D>().enabled))
        {
            jumps = 0;
        }
    }
}
