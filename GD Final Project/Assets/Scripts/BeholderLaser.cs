using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderLaser : MonoBehaviour
{
    public LineRenderer line;
    public Transform firePoint;
    public Transform tempTarget;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        EnableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser();
    }

    void EnableLaser()
    {
        line.enabled = true;
    }
    void DisableLaser()
    {
        line.enabled = false;
    }
    void UpdateLaser()
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, tempTarget.position);

        Vector2 direction = tempTarget.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, groundLayer);
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, playerLayer);
        if (hit)
        {
            line.SetPosition(1, hit.point);
            //Debug.Log("Hit " + hit.point);
        }
        if (hitPlayer)
        {
            
        }
    }
    
}
