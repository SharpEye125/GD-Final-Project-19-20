using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderLaser : MonoBehaviour
{
    public bool laserOn;
    public LineRenderer line;
    public Transform firePoint;
    public Transform tempTarget;
    public LayerMask groundLayer;
    public GameObject startFX;
    public GameObject endFX;

    private Quaternion rotation;
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    //public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        FillLists();
        EnableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser();
        if (laserOn)
        {
            EnableLaser();
        }
        else
        {
            DisableLaser();
        }
    }

    void EnableLaser()
    {
        line.enabled = true;
        for(int i=0; i<particles.Count; i++)
        {
            if (particles[i].isEmitting == false)
            {
                particles[i].Play();
            }
        }
    }
    void DisableLaser()
    {
        line.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].isEmitting == true)
            {
                particles[i].Stop();
            }
        }
    }
    void UpdateLaser()
    {
        line.SetPosition(0, firePoint.position);
        startFX.transform.position = (Vector2)firePoint.position;
        Vector2 direction = tempTarget.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, groundLayer);
        line.SetPosition(1, hit.point);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;

        //RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, playerLayer);
        if (hit && laserOn)
        {
            line.SetPosition(1, hit.point);
            //Debug.Log("Hit " + hit.point);
            if (hit.collider.gameObject.GetComponent<PlayerDie>())
            {
                
                if (FindObjectOfType<PlayerDie>().dead == false)
                {
                    FindObjectOfType<PlayerDie>().dead = true;
                }
            }
        }
        endFX.transform.position = line.GetPosition(1);

    }
    void FillLists()
    {
        for(int i=0; i < startFX.transform.childCount; i++)
        {
            var ps = startFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
        for (int i = 0; i < endFX.transform.childCount; i++)
        {
            var ps = endFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }
    
}
