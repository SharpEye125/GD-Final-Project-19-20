using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThru : MonoBehaviour
{
    public BoxCollider2D pTriggerCollider;
    public CompositeCollider2D dCollider;
    public EdgeCollider2D altCollider;
    public float timer = 0f;
    public float disableTime = 2f;
    public bool fallThru = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fallThru == true)
        {
            timer += Time.deltaTime;

            if (timer >= disableTime)
            {
                if (dCollider != null)
                {
                    EnableDCollider();
                }
                else 
                {
                    EnableAltCollider();
                }
                fallThru = false;
                timer = 0;
                dCollider = null;
                altCollider = null;
                pTriggerCollider.enabled = true;
                
            }
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "2Way" && Input.GetButton("Down") )
        {
            if (collision.gameObject.GetComponent<EdgeCollider2D>() != null)
            {
                altCollider = collision.gameObject.GetComponent<EdgeCollider2D>();
                DisableAltCollider();
            }
            if (collision.gameObject.GetComponent<CompositeCollider2D>() != null)
            {
                dCollider = collision.gameObject.GetComponent<CompositeCollider2D>();
                DisableDCollider();
            }
            pTriggerCollider.enabled = false;
            fallThru = true;
            gameObject.GetComponent<PlatformerMove>().grounded = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "2Way" && timer >= disableTime)
        {
            if (dCollider != null)
            {
                EnableDCollider();
            }
            if (altCollider != null)
            {
                EnableAltCollider();
            }
            fallThru = false;
            timer = 0;
            pTriggerCollider.enabled = true;
        }
    }
    void DisableDCollider()
    {
        dCollider.isTrigger = true;
    }
    void EnableDCollider()
    {
        dCollider.isTrigger = false;
    }
    void DisableAltCollider()
    {
        altCollider.isTrigger = true;
    }
    void EnableAltCollider()
    {
        altCollider.isTrigger = false;
    }
}
