using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 12f;
    public float dashSpeed = 16f;
    private bool isDashing = false;
    private float dashCd = 0;
    private int jumps = 0;
    private Rigidbody2D rb;
    public int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        dashCd -= Time.deltaTime;
        if (!isDashing)
        {
            Move();
        }
        //Change the transform.position.y to a check for collision with ground later
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 1 && !isDashing)
        {
            Jump();
        }
        //Add check for if the player can dash again later
        if (Input.GetKey("c") && dashCd <= 0)
        {
            dashCd = 2;
            StartCoroutine(Dash(getDirection()));
        }
    }

    private void Move()
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
        rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        jumps++;
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
            return Vector2.right.normalized;
        }
        return result.normalized;
    }

    void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    private IEnumerator Dash(Vector2 direction)
    {
        rb.velocity = direction * dashSpeed;
        isDashing = true;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 5);
        rb.gravityScale = 2.4f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" && rb.velocity.y <= 0) || (collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<BoxCollider2D>().enabled))
        {
            jumps = 0;
        }
    }
}
