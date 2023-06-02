using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool tempDie;
    public float deadTime;
    float timer;
    //public Animator animator;
    bool dead;
    public int maxHealth = 1;
    public int currentHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && tempDie)
        {
            timer = Time.deltaTime;
            if (timer >= deadTime / 2)
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            if (timer >= deadTime)
            {
                Revive();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!dead)
        {
            currentHealth -= damage;

            //animator.SetTrigger("Hurt");

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        dead = true;
        //animator.SetBool("IsDead", true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        GetComponent<Collider2D>().enabled = false;
        if (!tempDie)
        {
            this.enabled = false;
        }
    }
    void Revive()
    {
        dead = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Collider2D>().enabled = true;
    }
}
