using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonsterProjectile : MonoBehaviour
{
    private Animator projAnim;
    private Rigidbody2D projRb;
    private GameObject player;
    private PlayerController playerC;
    public float projSpeed = 10f;
    private float spawnTime;
    public int dmg = 2;
    // Start is called before the first frame update
    void Start()
    {
        projAnim = GetComponent<Animator>();
        projRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerC = player.GetComponent<PlayerController>();
        spawnTime = Time.time;
        projRb.AddForce(-(transform.position - player.transform.position).normalized * projSpeed, ForceMode2D.Impulse);
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
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(playerC.TakeDamage(dmg));
                dmg = 0;
            }
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        projRb.velocity = Vector2.zero;
        projAnim.SetTrigger("HitTrigger");
        yield return new WaitForSeconds(0.4f);
        GetComponent<EyeMonsterProjectile>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
