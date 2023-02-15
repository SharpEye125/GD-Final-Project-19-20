using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSlimesCheck : MonoBehaviour
{

    [SerializeField] public static bool clean;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clean)
        {
            GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
            foreach (GameObject Slime in slimes)
            {
                Slime.GetComponent<SpriteRenderer>().color = FindObjectOfType<SlimeCleanupTask>().slimeClean;
            }
            
        }
    }
}
