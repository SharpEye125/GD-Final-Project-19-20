using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMove : MonoBehaviour
{
    //public float speedGainRate = 0.05f;
    //public AudioClip jumpSFX;
    public AudioSource jumpSFXPlayer;
    public float moveSpeed = 1.0f;
    public float runSpeed = 2f;
    public float minMoveX = 0.2f;

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

    [SerializeField] bool funHover = false;

    float tempGravity;
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
        //Debug.Log(moveX);
        velocity = GetComponent<Rigidbody2D>().velocity;
        PlayerMove();
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
        
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && fallStun == false)// && grounded)
        {
            Jump();
            jumpSFXPlayer.Play();
        }
        ChangePlayerDirection();
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
    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        //Debug.Log(moveX);
        if (moveX < 0 && moveX > -minMoveX || moveX > 0 && moveX < minMoveX)
        {
            moveX = 0;
        }
        velocity = GetComponent<Rigidbody2D>().velocity;
        if (GetComponent<Rigidbody2D>().gravityScale != 0)
        {
            tempGravity = GetComponent<Rigidbody2D>().gravityScale;
        }
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
        else if (GetComponentInChildren<SlimeCleanupTask>() != null && GetComponentInChildren<SlimeCleanupTask>().mopping == true)
        {
            if (GetComponent<PlayerCombat>() != null && GetComponent<PlayerCombat>().attackMode == false)
            //When mopping use mopSpeed
            velocity.x = GetComponentInChildren<SlimeCleanupTask>().mopSpeed * moveX;
            UpdateAnimVars();
        }
        else if (Input.GetButton("Sprint") && GetComponent<PlayerCombat>().attackMode == false)
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
        if (funHover == true)
        {
            if (grounded == false && GetComponent<LadderClimb>().climbing == false && Input.GetButton("Jump") && GetComponent<Rigidbody2D>().velocity.y < 1)
            {
                GetComponent<BetterJumping>().enabled = false;
                velocity.y = 0;
                GetComponent<Rigidbody2D>().gravityScale = -0.1f;
            }
            else
            {
                Debug.Log("No hover");
                GetComponent<BetterJumping>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = tempGravity;
            }
        }
        GetComponent<Rigidbody2D>().velocity = velocity;
        //GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, velocity, speedGainRate);

    }
    void ChangePlayerDirection()
    {
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
    public void UpdateAnimVars()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("x", velocity.x);
        anim.SetFloat("y", velocity.y);
        anim.SetBool("stunned", fallStun);
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
                anim.SetBool("stunned", fallStun);
            }
            else
            {
                anim.SetBool("stunned", fallStun);
                //anim.SetBool("landed", fallStun);
                anim.SetTrigger("landed");
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
