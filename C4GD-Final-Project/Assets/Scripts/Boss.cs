using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D bossRb;
    private Animator bossAnim;
    private SpriteRenderer bossSR;
    private GameObject player;
    public float attackCd;
    public GameObject bossFireball1;
    public GameObject bossFireball2;
    public GameObject bossSummon;
    public GameObject bossLaser;
    private float timeSinceAttack = 1f;
    public float leftBound;
    public float rightBound;
    private int attackChooser = 0;

    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();
        bossSR = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Enemy>().alive) {
            player.GetComponent<PlayerController>().Win();
        }
        if (GetComponent<Enemy>().currentHealth <= GetComponent<Enemy>().maxHealth * 2 / 3)
        {
            attackCd = 1.25f;
        }
        if (GetComponent<Enemy>().currentHealth <= GetComponent<Enemy>().maxHealth / 3)
        {
            attackCd = 1f;
        }
        if (player.transform.position.x > leftBound && player.transform.position.x < rightBound)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, 0);
        }
        if (Time.time > timeSinceAttack)
        {
            attackChooser = Random.Range(0, 15);
            if (attackChooser < 4)
            {
                AttackOne();
            }
            else if (attackChooser < 8)
            {
                AttackTwo();
            }
            else if (attackChooser < 12)
            {
                AttackThree();
            }
            else if (attackChooser < 14)
            {
                AttackOne();
                AttackTwo();
                AttackThree();
            }
            else
            {
                AttackFour();
            }
            timeSinceAttack = Time.time + attackCd;
        }
    }

    //Shoots a spread of fireballs that target the player
    void AttackOne()
    {
        for (int i = -2; i < 3; i++)
        {
            Instantiate(bossFireball1, transform.position + new Vector3(2 * i, -1), Quaternion.FromToRotation(Vector2.right, player.transform.position));
        }
    }

    //Summons fireballs from the left and right of the screen 
    void AttackTwo()
    {
        int leftOrRight = (Random.Range(1, 3) * 2) - 3;
        float yOffset = 0;
        //If the player is around less than 2 blocks above the ground
        if (player.transform.position.y < 0)
        {
            yOffset = 3.5f;
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bossFireball2, player.transform.position + new Vector3(15 * leftOrRight, player.transform.position.y + yOffset + i, 0), bossFireball2.transform.rotation);
            Instantiate(bossFireball2, player.transform.position + new Vector3(-15 * leftOrRight, player.transform.position.y + yOffset - i, 0), bossFireball2.transform.rotation);
        }
    }

    void AttackThree()
    {
        Instantiate(bossLaser, new Vector3(player.transform.position.x, 1.4f, 0), bossLaser.transform.rotation);
    }

    void AttackFour()
    {
        Instantiate(bossSummon, new Vector3((leftBound + rightBound) / 2, 1f, 0), bossSummon.transform.rotation);
    }
}
