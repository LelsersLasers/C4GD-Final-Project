using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 10f;
    public float range = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color startColor;

    public int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude <= range) {
            Vector2 force = new Vector2(player.transform.position.x - transform.position.x, 0).normalized * speed;
            rb.velocity = new Vector2(force.x, rb.velocity.y);
            sr.color = Color.blue;
        }
        else {
            sr.color = startColor;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Set death animation later
        sr.color = Color.red;
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
