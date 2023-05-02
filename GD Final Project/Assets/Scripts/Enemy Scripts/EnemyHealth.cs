using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    //public Animator animator;

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
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        //animator.SetBool("IsDead", true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
