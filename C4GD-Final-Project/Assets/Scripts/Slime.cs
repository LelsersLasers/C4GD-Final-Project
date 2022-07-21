using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject player;
    private bool lockedOn;
    public float aggroRange;
    public float minJump;
    public float maxJump;
    public float jumpCd;
    public float attackCd;
    public float jumpSpeed;
    private float timeSinceJump = 0f;
    private float timeSinceAttack = 0f;
    private Rigidbody2D slimeRb;
    private Animator slimeAnim;
    private bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeAnim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lockedOn = Vector2.Distance(player.transform.position, transform.position) <= aggroRange;
        if (Time.time >= timeSinceJump)
        {
            if (!lockedOn)
            {
                Vector2 position = new Vector2(Random.Range(transform.position.x - 6, transform.position.x + 6), Random.Range(transform.position.y - 6, transform.position.y + 6));
                StartCoroutine(Jump(position));
            }
            else
            {
                StartCoroutine(Jump(player.transform.position));
            }
            timeSinceJump = Time.time + jumpCd;
        }
        if (lockedOn && Time.time >= timeSinceAttack && !isJumping)
        {
            Attack();
            timeSinceAttack = Time.time + attackCd;
        }
    }

    IEnumerator Jump(Vector2 target)
    {
        float jumpMultiplier = Random.Range(minJump, maxJump);
        float directionX;
        if (target.x - transform.position.x >= 0)
        {
            directionX = Random.Range(3f, 5f);
        }
        else
        {
            directionX = Random.Range(-5f, 3f);
        }
        float directionY = Random.Range(3f, 5f);
        slimeAnim.SetTrigger("JumpTrigger");
        yield return new WaitForSeconds(0.6f);
        slimeRb.velocity = new Vector2(directionX, directionY).normalized * jumpMultiplier;
        isJumping = true;
        yield return new WaitForSeconds(1f);
        isJumping = false;
        slimeRb.velocity = Vector2.zero;
    }
    void Attack()
    {
        slimeAnim.SetTrigger("AttackTrigger");
    }
}
