using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMove : MonoBehaviour
{
    //public AudioClip jumpSFX;
    public AudioSource jumpSFXPlayer;
    public float moveSpeed = 1.0f;
    public float runSpeed = 2f;
    public float jumpSpeed = 1.0f;
    public int maxJumps = 2;
    public int jumpCount = 0;
    public float maxFallSpeed = -20f;
    public bool grounded = false;
    bool right;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //jumpSFXPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (Input.GetButton("Sprint"))
        {
            //Debug.Log("Running!");
            velocity.x = runSpeed * moveX;
            anim.SetBool("grounded", grounded);
            anim.SetFloat("x", velocity.x * 2);
            anim.SetFloat("y", velocity.y * 2);
        }
        else
        {
            velocity.x = moveSpeed * moveX;
            anim.SetBool("grounded", grounded);
            anim.SetFloat("x", velocity.x);
            anim.SetFloat("y", velocity.y);
        }

        if (velocity.y <= maxFallSpeed)
        {
            velocity.y = maxFallSpeed;
        }
        GetComponent<Rigidbody2D>().velocity = velocity;
        
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)// && grounded)
        {
            Jump();
            jumpSFXPlayer.Play();
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //Vector3 s = transform.localScale;
            //s.x = 1;
            //transform.localScale = s;
            
        }
        else if (x < 0)
        {
            //Vector3 s = transform.localScale;
            //s.x = -1;
            //transform.localScale = s;
            
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void Jump()
    {
        if (!grounded)
        {
            jumpCount++;
        }
        if (GetComponent<LadderClimb>().climbing)
        {
            GetComponent<LadderClimb>().timer = 0;
        }

        GetComponent<LadderClimb>().GetOffLadder();

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
        GetComponent<Rigidbody2D>().AddForce
            (new Vector2(0, 100 * jumpSpeed));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            jumpCount = 0;
            grounded = true;
            GetComponent<LadderClimb>().climbing = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            jumpCount++;
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            jumpCount = 0;
            grounded = true;
            GetComponent<LadderClimb>().climbing = false;
        }
    }
}
