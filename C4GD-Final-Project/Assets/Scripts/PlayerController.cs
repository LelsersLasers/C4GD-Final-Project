using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 12f;
    public float dashSpeed = 16f;
    public int damage = 5;
    private bool isDashing = false;
    private bool isAttacking = false;
    private float dashCd = 0;
    private int jumps = 0;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public int maxHealth;
    private int currentHealth;
    
    public Transform attackPoint;
    public Transform hp;
    private float hpW;

    public float attackRange = 0.5f;
    public float attackCd = 1.0f;
    private float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    private float orientation = 1f;
    public bool iFramesActive = false;
    public float iFrameDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        hpW = hp.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x < 0) {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0) {
            sr.flipX = false;
        }
        hp.localScale = new Vector3(hpW * ((float)currentHealth / maxHealth), hp.localScale.y, hp.localScale.z);
        dashCd -= Time.deltaTime;
        if (!isDashing)
        {
            Move();
        }
        //Change the transform.position.y to a check for collision with ground later
        if (Input.GetKey(KeyCode.Space) && jumps < 1 && !isDashing)
        {
            Jump();
        }
        //Add check for if the player can dash again later
        if (Input.GetKey("c") && dashCd <= 0 && !isAttacking)
        {
            dashCd = 1.5f;
            StartCoroutine(Dash(GetDirection()));
        }
        if (Input.GetKey("x") && !isDashing && Time.time >= nextAttackTime)
        {
            StartCoroutine(Attack(GetDirection()));
            nextAttackTime = Time.time + attackCd;
        }
    }

    private void Move()
    {
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1;
            orientation = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1;
            orientation = 1;
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
    private Vector2 GetDirection()
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
            return new Vector2(orientation,0);
        }
        return result.normalized;
    }

    IEnumerator TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        iFramesActive = true;
        yield return new WaitForSeconds(iFrameDuration);
        iFramesActive = false;
    }

    void Die()
    {

    }

    IEnumerator Attack(Vector2 direction)
    {
        //Set trigger for animator once we have animations

        attackPoint.RotateAround(transform.position, new Vector3(0,0,1), Vector2.SignedAngle(new Vector2(1,0), direction));
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D target in hitTargets)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
        //Time should be as long as the attack animation
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        attackPoint.RotateAround(transform.position, new Vector3(0, 0, 1), -Vector2.SignedAngle(new Vector2(1, 0), direction));
        isAttacking = false;
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
        if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<BoxCollider2D>().enabled))
        {
            jumps = 0;
        }
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            if (!iFramesActive)
            {
                StartCoroutine(TakeDamage(collision.gameObject.GetComponent<Enemy>().GetDamage()));
            }
        }
    }

    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
