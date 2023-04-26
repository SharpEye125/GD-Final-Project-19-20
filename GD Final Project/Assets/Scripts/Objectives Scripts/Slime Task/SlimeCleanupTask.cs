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
        //Checks how many slime objects are in scene for tallying
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach (GameObject Slime in slimes)
        {
            slimeCount++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if the slime object color equals the clean slime color, or if alpha (transparency) is below 3, and that the slime hasn't been cleaned
        if (slime != null && slime.GetComponent<SpriteRenderer>().color == slimeClean && slime.transform.localScale.z != 2 ||
    slime != null && slime.GetComponent<SpriteRenderer>().color.a <= 0.1f && slime.transform.localScale.z != 2)
        {
            cleanSlimeCount++;
            if (slime.GetComponent<ParticleSystem>() != null)
            {
                slime.GetComponent<ParticleSystem>().Play();
            }
            //newly cleaned slime gets labelled as clean by setting unseeable Z scale from 1 to 2 to prevent multiple tallies from already clean slimes
            slime.transform.localScale = new Vector3(slime.transform.localScale.x, slime.transform.localScale.y, 2);
            if (slime.GetComponent<BoxCollider2D>().isTrigger == false)
            {
                //if slime object has a box collider that the player can stand on, turn it off since the slime object is clean
                slime.SetActive(false);
            }
            if (slime.GetComponent<SlimePacing>() != null)
            {
                slime.GetComponent<SlimePacing>().enabled = true;
            }
        }
        if (cleanSlimeCount < slimeCount)
        {
            //Temporary indicator that not all slime objects are clean
            completion.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            //Temporary indicator that all slime objects are clean
            completion.GetComponent<SpriteRenderer>().color = Color.green;
            //CleanSlimesCheck.clean = true;
            //TasksManager.slimeTask = true;
        }
        if (Input.GetButton("Fire1") && hasMop && GetComponent<PlatformerMove>().grounded == true)
        {
            //Player starts cleaning/mopping if on the ground and pressing a Fire1 input key
            mopping = true;
            
            
            if (slime != null)
            {
                //if standing in slime object trigger, start cleaning it by changing its color to slimeClean color
                slime.GetComponent<SpriteRenderer>().color = Color.Lerp(slime.GetComponent<SpriteRenderer>().color, slimeClean, colorChangeRate);
            }
        }
        else
        {
            //if not pressing Fire1 input key to mop or not grounded stop mopping
            mopping = false;
            mopPrefab.GetComponent<TrailRenderer>().emitting = false;
            mopPrefab.GetComponent<ParticleSystem>().Stop();
        }
        if (mopping)
        {
            //play mopping particles
            if (mopPrefab.GetComponent<ParticleSystem>().isEmitting == false)
            {
                mopPrefab.GetComponent<ParticleSystem>().Play();
            }
            
            mopPrefab.GetComponent<TrailRenderer>().emitting = true;
        }
        else
        {
            //stop emitting mopping particles
            if (mopPrefab.GetComponent<ParticleSystem>().isEmitting)
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
            //if standing on slime, set current slime gameObject to current slime and set slimeClean color to this slime gameObjects clean color
            slime = collision.gameObject;
            slimeClean = slime.GetComponent<CleanSlimesCheck>().cleanedColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slime" && collision.gameObject == slime)
        {
            //if walked away from slime
            if (slime.GetComponent<SpriteRenderer>().color == slimeClean && collision.transform.localScale.z != 2)
            {
                //if slime the player walked away from is clean add it to the tally
                cleanSlimeCount++;
                slime.GetComponent<ParticleSystem>().Play();
                collision.transform.localScale = new Vector3(collision.transform.localScale.x, collision.transform.localScale.y, 2);
            }
            //remove slime from current slime object variable
            slime = null;
        }
    }
    void UpdateParticles()
    {

    }
}
