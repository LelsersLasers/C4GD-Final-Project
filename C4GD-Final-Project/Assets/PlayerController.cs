using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 12f;
    public float dashSpeed = 16f;
    private bool isDashing = false;
    private Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Later remove this once there is ground/solid assets
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector2(transform.position.x, -4.5f);
        }
        if (!isDashing)
        {
            move();
        }
        //Change the transform.position.y to a check for collision with ground later
        if (Input.GetKeyDown("space") && transform.position.y == -4.5f && !isDashing)
        {
            jump();
        }
        //Add check for if the player can dash again later
        if (Input.GetKeyDown("c"))
        {
            StartCoroutine(dash(getDirection()));
        }
    }

    private void move()
    {
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1;
        }
        playerRb.velocity = new Vector2(speed * horizontalInput, playerRb.velocity.y);
    }

    private void jump()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
    }

    //Returns a unit vector in one of 8 directions based on the arrow key combination used. Add a unit vector in the direction each pressed arrow key to a result vector.
    //Normalize the final resulting vector and return it. If the final vector ends up being a zero vector, then return Vector2.right.
    private Vector2 getDirection()
    {
        Vector2 result = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            result += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            result += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            result += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            result += new Vector2(0, -1);
        }
        if (result == Vector2.zero)
        {
            return Vector2.right;
        }
        result = result.normalized;
        return result;
    }

    private IEnumerator dash(Vector2 direction)
    {
        playerRb.velocity = direction * dashSpeed;
        isDashing = true;
        playerRb.gravityScale = 0f;
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        playerRb.gravityScale = 2.4f;
    }
}
