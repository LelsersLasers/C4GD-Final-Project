using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public GameObject player;
    private PlayerController pController;
    private BoxCollider2D bc;
    private float activateDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        pController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        activateDelay -= Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) {
            activateDelay = 0.5f;
            if (bc.enabled)
            {
                pController.IncrementJumps();
            }
        }
        bc.enabled = playerRb.velocity.y <= 0 && activateDelay < 0;
    }
}
