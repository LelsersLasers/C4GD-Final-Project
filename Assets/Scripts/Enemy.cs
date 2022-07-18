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

    private int action = 0;
    private float actionTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
        randomAction();
    }

    // Update is called once per frame
    void Update()
    {
        actionTime -= Time.deltaTime;
        if (actionTime < 0)
        {
            randomAction();
        }
        sr.color = startColor;
        switch (action)
        {
            case 0: // move towards player
                Vector2 force = new Vector2(player.transform.position.x - transform.position.x, 0).normalized * speed;
                rb.velocity = new Vector2(force.x, rb.velocity.y);
                break;
            case 1: // stand still
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
            case 2: // shoot
                sr.color = Color.red;
                break;
        }
    }

    void randomAction() {
        if ((player.transform.position - transform.position).magnitude <= range)
        {
            action = Random.Range(0, 3);
            Debug.Log(action);
            actionTime = Random.Range(3f, 5f);
        }
    }
}
