using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 startOffset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        startOffset.z = transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentOffset = transform.position - player.transform.position;
        currentOffset.z = startOffset.z;
        transform.position += (startOffset - currentOffset) * Time.deltaTime * 2f;

    }
}
