using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatormPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDir = 1;
    public LayerMask groundLayers = 12;
    Animator anim;

    public bool startingRight;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;

        velocity.x = moveDir * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;
        anim.SetFloat("x", moveDir);
    }
    public void TurnAround()
    {
        moveDir *= -1;

        if (startingRight)
        {
            if (moveDir > 0)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                Vector3 s = transform.localScale;
                s.x = Mathf.Abs(s.x);
                transform.localScale = s;
            }
            else if (moveDir < 0)
            {
                Vector3 s = transform.localScale;
                if (s.x > 0)
                {
                    s.x *= -1;
                }
                transform.localScale = s;
                //GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            if (moveDir < 0)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                Vector3 s = transform.localScale;
                s.x = Mathf.Abs(s.x);
                transform.localScale = s;
            }
            else if (moveDir > 0)
            {
                Vector3 s = transform.localScale;
                if (s.x > 0)
                {
                    s.x *= -1;
                }
                transform.localScale = s;
                //GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            TurnAround();
        }
    }
}
