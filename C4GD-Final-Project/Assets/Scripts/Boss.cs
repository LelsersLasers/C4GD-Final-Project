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
    private float timeSinceAttack = 0f;
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
        if (player.transform.position.x > leftBound && player.transform.position.x < rightBound)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, 0);
        }
        if (Time.time > timeSinceAttack)
        {
            attackChooser = Random.Range(0, 9);
            if (attackChooser <= 3)
            {
                AttackOne();
            }
            else if (attackChooser <= 7)
            {
                AttackTwo();
            }
            else
            {
                AttackThree();
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
        int yOffset = 0;
        //If the player is around less than 2 blocks above the ground
        if (player.transform.position.y < -1)
        {
            yOffset = 4;
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bossFireball2, player.transform.position + new Vector3(12 * leftOrRight, player.transform.position.y + yOffset + i, 0), bossFireball2.transform.rotation);
            Instantiate(bossFireball2, player.transform.position + new Vector3(-12 * leftOrRight, player.transform.position.y + yOffset - i, 0), bossFireball2.transform.rotation);
        }
    }

    void AttackThree()
    {
        Instantiate(bossSummon, new Vector3((leftBound + rightBound) / 2, 3, 0), bossSummon.transform.rotation);
    }
}
