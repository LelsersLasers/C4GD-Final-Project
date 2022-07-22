using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer laserSR;
    public float timeBeforeExplode;
    public float explosionDuration;
    private BoxCollider2D hitbox;
    private float lifeTime;
    private bool notExploded = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lifeTime = Time.time;
        laserSR = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        laserSR.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lifeTime + timeBeforeExplode && notExploded)
        {
            notExploded = false;
            StartCoroutine("Explode");
        }
    }

    IEnumerator Explode()
    {
        hitbox.enabled = true;
        laserSR.color = Color.red;
        yield return new WaitForSeconds(explosionDuration);
        hitbox.enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }
}
