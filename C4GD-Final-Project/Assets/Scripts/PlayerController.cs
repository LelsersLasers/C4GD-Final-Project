using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public SpriteRenderer SpriteRenderer; 
    public float speed = 6f;
    public float jumpSpeed = 16f;
    public float dashSpeed = 20f;
    public int damage = 5;
    private bool isDashing = false;
    private bool isAttacking = false;
    private float dashCd = 0;
    public float orientation = 1f;

    public bool alive = true;

    private float maxRot = 90f;
    private float currentRot = 0f;

    public float deathY = -25f;
    public GameObject deathUI;
    public GameObject winUI;
    

    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip jumpSound;
    public AudioClip dashSound;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public int maxHealth;
    private int currentHealth;
    
    public Transform attackPoint;
    private GameObject playerAttack;

    public Transform hp;
    private float hpW;

    public Transform cd;
    private float cdW;

    public float attackRange = 0.5f;
    public float attackCd = 1.0f;
    private float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public bool iFramesActive = false;
    public float iFrameDuration = 0.5f;
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerAttack = GameObject.Find("Player Attack");
        currentHealth = maxHealth;
        hpW = hp.localScale.x;
        cdW = cd.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHud();
        if (alive)
        {
            if (rb.velocity.x < 0 ) {
                sr.flipX = true;
            }
            else if (rb.velocity.x > 0) {
                sr.flipX = false;
            }
            
            dashCd -= Time.deltaTime;
        
            if (!isDashing)
            {
                Move();
            }
            //Change the transform.position.y to a check for collision with ground later
            if (Input.GetKey(KeyCode.Space) && isOnGround && !isDashing)
            {
                Jump();
            }
            //Add check for if the player can dash again later
            if ((Input.GetKey("c") || Input.GetKey(KeyCode.LeftShift)) && dashCd <= 0 && !isAttacking)
            {
                dashCd = 1.5f;
                StartCoroutine(Dash(GetDirection()));
            }
            if (Input.GetKey("x") && !isDashing && Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack(GetDirection()));
                nextAttackTime = Time.time + attackCd;
            }
            if (transform.position.y < deathY)
            {
                Die();
            }
        }
        else {
            currentRot += orientation * Time.deltaTime * 180f;
            if ((orientation == 1 && currentRot > maxRot) || (orientation == -1 && currentRot < maxRot))
            {
                currentRot = maxRot;
            }
            transform.rotation = Quaternion.Euler(0, 0, currentRot);
        }
    }

    private void UpdateHud()
    {
        if (alive)
        {
            hp.localScale = new Vector3(hpW * ((float)currentHealth / maxHealth), hp.localScale.y, hp.localScale.z);
            float dashDisplay = (1.5f - dashCd) / 1.5f;
            if (dashDisplay >= 1) {
                dashDisplay = 0;
            }
            cd.localScale = new Vector3(cdW * dashDisplay, cd.localScale.y, cd.localScale.z);
        }
        else
        {
            hp.localScale = new Vector3(0, 0, 0);
            cd.localScale = new Vector3(0, 0, 0);
        }
    }

    private void Move()
    {
        float horizontalInput = 0;
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1;
            orientation = -1;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1;
            orientation = 1;
        }
        maxRot = 90f * orientation;
        rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);
        
        animator.SetBool("IsRunning",isOnGround && horizontalInput != 0);
    }

    private void Jump()
    {
        audioSource.PlayOneShot(jumpSound, 1f);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        isOnGround = false;
    }

    //Returns a unit vector in one of 8 directions based on the arrow key combination used. Add a unit vector in the direction each pressed arrow key to a result vector.
    //Normalize the final resulting vector and return it.
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
            return transform.right * orientation;
        }
        return result.normalized;
    }

    public IEnumerator TakeDamage(int dmg)
    {
        if (!iFramesActive)
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
    }

    void Die()
    {
        deathUI.SetActive(true);
        alive = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("IsRunning", false);
    }

    void Win()
    {
        winUI.SetActive(true);
    }

    IEnumerator Attack(Vector2 direction)
    {
        audioSource.PlayOneShot(attackSound, 1f);
        //Set trigger for animator once we have animations
        attackPoint.position = transform.position + new Vector3(direction.x * 2f, direction.y * 2f, 0);
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D target in hitTargets)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
        //Time should be as long as the attack animation
        isAttacking = true;
        playerAttack.GetComponent<PlayerAttack>().StartAttack(direction);
        yield return new WaitForSeconds(0.2f);
        playerAttack.GetComponent<PlayerAttack>().EndAttack();
        attackPoint.position = transform.position;
        isAttacking = false;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        audioSource.PlayOneShot(dashSound, 0.1f);
        rb.velocity = direction * dashSpeed;
        isDashing = true;
        iFramesActive = true;
        StartCoroutine(turnIFrameOff());
        rb.gravityScale = 0f;
        if (direction == new Vector2(1, 1).normalized || direction == new Vector2(-1, 1).normalized)
        {
            animator.SetTrigger("DashUFTrigger");
        }
        else if (direction == new Vector2(1, 0).normalized || direction == new Vector2(-1, 0).normalized)
        {
            animator.SetTrigger("DashFTrigger");
        }
        else if (direction == new Vector2(1, -1).normalized || direction == new Vector2(-1, -1).normalized)
        {
            animator.SetTrigger("DashDFTrigger");
        }
        else if (direction == new Vector2(0, 1).normalized)
        {
            animator.SetTrigger("DashUTrigger");
        }
        else if (direction == new Vector2(0, -1).normalized)
        {
            animator.SetTrigger("DashDTrigger");
        }
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 5);
        rb.gravityScale = 4.0f;
    }

    private IEnumerator turnIFrameOff() {
        yield return new WaitForSeconds(iFrameDuration / 2);
        iFramesActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.tag == "Platform" && collision.gameObject.GetComponent<BoxCollider2D>().enabled)
        {
            isOnGround = true;
        }
        else if (collision.gameObject.tag == "Trap")
        {
            Die();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "BossRoom")
        {
            SceneManager.LoadScene("TavernFight");
        }
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            StartCoroutine(TakeDamage(collision.gameObject.GetComponent<Enemy>().GetDamage()));
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
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "HostileProjectile")
        {
            StartCoroutine(TakeDamage(2));
        }
    }
}
