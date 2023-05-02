using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool attackMode;
    //public Animator anim;
    public float attackRange = 0.5f;
    public int attackDamage = 3;
    public float attackRate = 2f;
    float nextAttackTime = 0;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    nextAttackTime = Time.time + 1 / attackRate;
                }
            }
        }
    }

    public void Attack()
    {
        //Play Animation
        //anim.SetTrigger("Attack");

        //Detect enemies in attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        Debug.Log(hitEnemies);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
