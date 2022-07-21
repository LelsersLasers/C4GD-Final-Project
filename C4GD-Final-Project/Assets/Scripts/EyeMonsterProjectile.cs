using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonsterProjectile : MonoBehaviour
{
    private Animator projAnim;
    private Rigidbody2D projRb;
    public float projSpeed = 8f;
    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        projAnim = GetComponent<Animator>();
        projRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollided)
        {
            projRb.velocity = transform.forward * projSpeed;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground")
        {
            StartCoroutine(Explode());  
        }
    }
}
