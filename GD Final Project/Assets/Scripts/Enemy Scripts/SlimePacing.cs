using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePacing : MonoBehaviour
{
    float timer = 0;
    public float moveDir;
    public float moveSpeed = 3.0f;
    public float paceDuration = 3.0f;

    Rigidbody2D rb;
    public int jumpCount = 1;
    public bool grounded;
    public float jumpSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (timer >= paceDuration && grounded)
        {
            //do something
            moveDir *= -1;
            timer = 0;
            if (moveDir != 0)
            {
                if (moveDir == 1)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        if (grounded && jumpCount < 2)
        {
            SlimeHop();
        }
        
        velocity.x = moveDir * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;

    }
    public void SlimeHop()
    {
        jumpCount++;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
        grounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            grounded = true;
            jumpCount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            grounded = true;
            jumpCount = 0;
        }
    }
}
