using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCleanupTask : MonoBehaviour
{
    public bool hasMop;

    //Will be used to let animator know what animation to use
    public bool mopping;
    public float mopSpeed = 2;
    public GameObject mopPrefab;
    public GameObject slime;
    public int cleanSlimeCount;
    public int slimeCount;

    public float colorChangeRate = 2;
    //public Color slimeNorm;
    public Color slimeClean;
    public GameObject completion;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach (GameObject Slime in slimes)
        {
            slimeCount++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (slime != null && slime.GetComponent<SpriteRenderer>().color == slimeClean && slime.transform.localScale.z != 2 ||
    slime != null && slime.GetComponent<SpriteRenderer>().color.a <= 0.1f && slime.transform.localScale.z != 2)
        {
            cleanSlimeCount++;
            if (slime.GetComponent<ParticleSystem>() != null)
            {
                slime.GetComponent<ParticleSystem>().Play();
            }
            slime.transform.localScale = new Vector3(slime.transform.localScale.x, slime.transform.localScale.y, 2);
            if (slime.GetComponent<BoxCollider2D>().isTrigger == false)
            {
                slime.SetActive(false);
            }
        }
        if (cleanSlimeCount < slimeCount)
        {
            completion.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            completion.GetComponent<SpriteRenderer>().color = Color.green;
            //CleanSlimesCheck.clean = true;
            //TasksManager.slimeTask = true;
        }
        if (Input.GetButton("Fire1") && hasMop && GetComponent<PlatformerMove>().grounded == true)
        {
            mopping = true;
            
            
            if (slime != null)
            {
                slime.GetComponent<SpriteRenderer>().color = Color.Lerp(slime.GetComponent<SpriteRenderer>().color, slimeClean, colorChangeRate);
            }
        }
        else
        {
            mopping = false;
            mopPrefab.GetComponent<TrailRenderer>().emitting = false;
            mopPrefab.GetComponent<ParticleSystem>().Stop();
        }
        if (mopping)
        {
            if (mopPrefab.GetComponent<ParticleSystem>().isPlaying == false)
            {
                mopPrefab.GetComponent<ParticleSystem>().Play();
            }
            
            mopPrefab.GetComponent<TrailRenderer>().emitting = true;
        }
        else
        {
            if (mopPrefab.GetComponent<ParticleSystem>().isPlaying)
            {
                mopPrefab.GetComponent<ParticleSystem>().Stop();
            }
            mopPrefab.GetComponent<TrailRenderer>().emitting = false;
        }
        GetComponent<Animator>().SetBool("mopping", mopping);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Slime")
        {
            slime = collision.gameObject;
            slimeClean = slime.GetComponent<CleanSlimesCheck>().cleanedColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slime" && collision.gameObject == slime)
        {
            if (slime.GetComponent<SpriteRenderer>().color == slimeClean && collision.transform.localScale.z != 2)
            {
                cleanSlimeCount++;
                slime.GetComponent<ParticleSystem>().Play();
                collision.transform.localScale = new Vector3(collision.transform.localScale.x, collision.transform.localScale.y, 2);
            }
            slime = null;
        }
    }
}
