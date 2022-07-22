using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject player;
    private Animator attackAnim;
    private SpriteRenderer attackSprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        attackAnim = GetComponent<Animator>();
        attackSprite = GetComponent<SpriteRenderer>();
        attackSprite.enabled = false;
    }

    public void StartAttack(Vector2 direction)
    {
        transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.right, direction));
        transform.position = player.transform.position + new Vector3(direction.x, direction.y, 0) * 2;
        attackSprite.enabled = true;
        attackAnim.SetTrigger("AttackTrigger");
    }

    public void EndAttack()
    {
        transform.rotation = Quaternion.identity;
        transform.position = player.transform.position;
        attackSprite.enabled = false;
    }
}
