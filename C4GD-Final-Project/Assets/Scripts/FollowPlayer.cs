using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public float xSpeed = 2f;
    public float ySpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentOffset = transform.position - player.transform.position;
        Vector3 move = new Vector3(
            -currentOffset.x * Time.deltaTime * xSpeed,
            -currentOffset.y * Time.deltaTime * ySpeed,
            0
        );
        // currentOffset.z = startOffset.z;
        // currentOffset.x = startOffset.x - currentOffset.x;
        // transform.position += (startOffset - currentOffset) * Time.deltaTime * 2f;
        transform.position += move;
    }
}
