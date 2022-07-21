using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCore : Enemy
{
    private GameObject boss;
    private SpriteRenderer coreSR;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        coreSR = GetComponent<SpriteRenderer>();
        maxHealth = boss.GetComponent<Enemy>().maxHealth / 3;
        currentHealth = maxHealth;
        hpW = hp.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.GetComponent<Enemy>().enabled == false)
        {
            Die();
        }
        hp.localScale = new Vector3(hpW * ((float)currentHealth / maxHealth), hp.localScale.y, hp.localScale.z);
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        boss.GetComponent<Enemy>().TakeDamage(dmg);
    }

    public override void Die()
    {
        hp.localScale = new Vector3(0, 0, 0);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DelayedDestory());
    }
}
