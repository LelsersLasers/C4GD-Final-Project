using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonster : MonoBehaviour
{
    public GameObject player;
    public GameObject eyeMonsterProjectile;
    private Rigidbody2D eyeRb;
    private Animator eyeAnim;
    private bool lockedOn = false;
    public float flySpeed;
    public float attackCd;
    public float aggroRange;
    private bool isShooting = false;
    private Vector2 startPos;
    private bool goingRight = true;
    private bool goingLeft = false;
    private float timeSinceShoot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        eyeRb = GetComponent<Rigidbody2D>();
        eyeAnim = GetComponent<Animator>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lockedOn = Vector2.Distance(player.transform.position, transform.position) <= aggroRange;
        if (!isShooting)
        {
            Move();
        }
        if (lockedOn && Time.time > timeSinceShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        timeSinceShoot = Time.time + attackCd;
        eyeRb.velocity = Vector2.zero;
        eyeAnim.SetTrigger("ShootTrigger");
        isShooting = true;
        yield return new WaitForSeconds(0.3f);
        Instantiate(eyeMonsterProjectile, transform.position, Quaternion.FromToRotation(Vector2.right, player.transform.position));
        isShooting = false;
    }

    void Move()
    {
        if (goingRight)
        {
            eyeRb.velocity = new Vector2(flySpeed, 0);
            if (transform.position.x > startPos.x + 4)
            {
                goingRight = false;
                goingLeft = true;
            }
        }
        if (goingLeft)
        {
            eyeRb.velocity = new Vector2(-flySpeed, 0);
            if (transform.position.x < startPos.x - 4)
            {
                goingLeft = false;
                goingRight = true;
            }
        }
    }
}
