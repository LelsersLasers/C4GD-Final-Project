using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public float xSpeed = 2f;
    public float ySpeed = 4f;

    public Vector3 targetOffset = new Vector3(0f, 0f, -10f);

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentOffset = transform.position - player.transform.position;
        Vector3 move = (targetOffset - currentOffset) * Time.deltaTime;
        move.x *= xSpeed;
        move.y *= ySpeed;
        transform.position += move;
    }
}
