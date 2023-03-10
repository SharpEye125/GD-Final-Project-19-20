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
    public bool grounded = false;

    //Fall Stun related variables
    public float maxFallSpeed = -20f;
    public float fallStunLength = 2;
    float fallStunTimer = 0;
    bool receiveFallStun = false;
    bool fallStun = false;

    public Animator anim;
    public Vector2 velocity;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        anim = GetComponent<Animator>();
        //jumpSFXPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        velocity = GetComponent<Rigidbody2D>().velocity;
        if (fallStun)
        {
            //Activates fallStun effects and duration timer
            fallStunTimer += Time.deltaTime;
            velocity.x = moveX;
            UpdateAnimVars();
            GetComponent<SpriteRenderer>().color = Color.yellow;
            if (fallStunTimer >= fallStunLength)
            {
                fallStun = false;
                receiveFallStun = false;
                fallStunTimer = 0;
                GetComponent<SpriteRenderer>().color = Color.white;
                anim.SetBool("landed", fallStun);
            }
        }
        else if (GetComponent<SlimeCleanupTask>() != null && GetComponent<SlimeCleanupTask>().mopping == true)
        {
            //When mopping use mopSpeed
            velocity.x = GetComponent<SlimeCleanupTask>().mopSpeed * moveX;
            UpdateAnimVars();
        }
        else if (Input.GetButton("Sprint"))
        {
            //Debug.Log("Running!");
            velocity.x = runSpeed * moveX;
            UpdateAnimVars();
        }
        else
        {
            //Walk moveSpeed
            velocity.x = moveSpeed * moveX;
            UpdateAnimVars();
        }
        if (velocity.y <= maxFallSpeed)
        {
            //if falling at maxFallSpeed velocity keep velocity at maxSpeed and allow for fallStun
            velocity.y = maxFallSpeed;
            receiveFallStun = true;
        }
        else
        {
            receiveFallStun = false;
        }
        GetComponent<Rigidbody2D>().velocity = velocity;
        
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && fallStun == false)// && grounded)
        {
            Jump();
            jumpSFXPlayer.Play();
        }
        
        float x = Input.GetAxisRaw("Horizontal");

        //Turn player to correct direction
        if (x > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x);
            transform.localScale = s;
        }
        else if (x < 0)
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
    public void UpdateAnimVars()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("x", velocity.x);
        anim.SetFloat("y", velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            //When landing on the ground
            if(receiveFallStun)
            {
                //If landed at maxFallSpeed start fall stun
                fallStun = true;
                anim.SetBool("landed", fallStun);
            }
            //anim.SetTrigger("landed");
            jumpCount = 0;
            grounded = true;
            if (velocity.y == 0)
            {
                GetComponent<LadderClimb>().climbing = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            //When leaving the ground
            jumpCount++;
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            //When on the ground
            jumpCount = 0;
            grounded = true;
            GetComponent<LadderClimb>().climbing = false;
        }
    }
}
