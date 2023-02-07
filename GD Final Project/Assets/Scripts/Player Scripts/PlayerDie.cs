using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public float timer = 0;
    public float respawnDelay = 4;
    public Vector3 checkPoint;
    public bool dead;
    

    //in case of multiple death animation depending on enemy:
    //public Animation deathAnim;

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            timer += Time.deltaTime;
        }
        if (timer >= respawnDelay)
        {
            timer = 0;
            dead = false;
            RespawnState();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            dead = true;
            DeathState();
            //sample code for grabbing the death animation from a specific enemy type: (don't forget to uncomment deathAnim)
            //deathAnim = collision.GetComponent<EnemyType>().killAnim;
        }
        if (collision.tag == "Checkpoint")
        {
            checkPoint = collision.transform.position;
        }
    }

    public void DeathState()
    {


        GetComponent<PlatformerMove>().enabled = false;
        //temp:
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void RespawnState()
    {
        transform.position = checkPoint;
        GetComponent<PlatformerMove>().enabled = true;

        //temp:
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
