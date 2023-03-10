using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPacing : MonoBehaviour
{
    float timer = 0;
    float pauseTimer = 0;
    public Vector2 moveDir;
    Vector2 tempDir;
    public float moveSpeed = 3.0f;
    public float paceDuration = 3.0f;
    public float pauseDuration = 2f;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        moveDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
        {
            //if not paused start/continue pacing timer
            timer += Time.deltaTime;
        }
        else
        {
            //if paused, set moveDirection to 0 to stop in place and start/continue timer
            pauseTimer += Time.deltaTime;
            moveDir = new Vector2(0, 0);
        }
        if (paused == true && pauseTimer >= pauseDuration)
        {
            //when pauseTimer is up, unpause, reset timer, and set moveDirection back to not 0
            paused = false;
            pauseTimer = 0;
            moveDir = tempDir;
        }

        if (timer >= paceDuration)
        {
            //do something
            paused = true;
            moveDir *= -1;
            tempDir = moveDir;
            timer = 0;
            if (moveDir.x != 0)
            {
                Vector3 s = transform.localScale;
                s.x *= -1;
                transform.localScale = s;
                //Alternate method to get object to look where it's moving
                if (moveDir.x == 1)
                {
                    //GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    //GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;

    }
}
