using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;
    public float range = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color startColor;

    public Transform hp;
    private float hpW;

    public int maxHealth;
    private int currentHealth;
    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        startColor = sr.color;
        hpW = hp.localScale.x;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        hp.localScale = new Vector3(hpW * ((float)currentHealth / maxHealth), hp.localScale.y, hp.localScale.z);
        if ((player.transform.position - transform.position).magnitude <= range) {
            sr.color = Color.blue;
        }
        else {
            sr.color = startColor;
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
        hp.localScale = new Vector3(0, 0, 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DelayedDestory());
    }

    private IEnumerator DelayedDestory()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return dmg;
    }
}
