using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball2 : MonoBehaviour
{
    public GameObject boss;
    private Animator projAnim;
    private Rigidbody2D projRb;
    private GameObject player;
    public int dmg = 3;
    public float projSpeed = 15f;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        projAnim = GetComponent<Animator>();
        projRb = GetComponent<Rigidbody2D>();
        boss = GameObject.Find("Boss");
        player = GameObject.Find("Player");
        spawnTime = Time.time;
        projRb.AddForce((new Vector2(boss.transform.position.x - transform.position.x, 0)).normalized * projSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + 2.5f)
        {
            StartCoroutine(Explode());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        projRb.velocity = Vector2.zero;
        projAnim.SetTrigger("HitTrigger");
        yield return new WaitForSeconds(0.4f);
        GetComponent<BossFireball2>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
