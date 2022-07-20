using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonsterProjectile : MonoBehaviour
{
    private Animator projAnim;
    private Rigidbody2D projRb;
    private GameObject player;
    public float projSpeed = 10f;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        projAnim = GetComponent<Animator>();
        projRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
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

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        projRb.velocity = Vector2.zero;
        projAnim.SetTrigger("HitTrigger");
        yield return new WaitForSeconds(1);
        GetComponent<EyeMonsterProjectile>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
