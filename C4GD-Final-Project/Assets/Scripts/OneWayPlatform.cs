using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public GameObject player;

    private BoxCollider2D bc;
    private float activateDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        activateDelay -= Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) {
            activateDelay = 0.5f;
        }
        bc.enabled = playerRb.velocity.y <= 0 && activateDelay < 0;
    }
}
