using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 2;
    public bool climbing;
    public float grabOffJumpDelay = 1f;

    float gravityAtStart;
    Transform player;
    public Transform ladder;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        gravityAtStart = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= grabOffJumpDelay)
        {
            timer += Time.deltaTime;
        }

        float moveY = Input.GetAxis("Vertical");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (climbing && timer >= grabOffJumpDelay)
        {
            ClimbLadder();
            //player.position = new Vector3(ladder.transform.position.x, player.position.y);
        }
        else
        {
            GetOffLadder();
        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ladder" && Input.GetButton("Vertical") && !climbing)
        {
            ladder = collision.transform;
            
            climbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            GetOffLadder();
        }
    }
    public void ClimbLadder()
    {
        float moveY = Input.GetAxis("Vertical");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.y = climbSpeed * moveY;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = velocity;
        GetComponent<PlatformerMove>().jumpCount = 0;
        player.position = new Vector3(ladder.transform.position.x, player.position.y);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }
    public void GetOffLadder()
    {
        GetComponent<BetterJumping>().enabled = true;
        climbing = false;
        GetComponent<Rigidbody2D>().gravityScale = gravityAtStart;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
