using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    public static bool slimeTask;
    public static bool necromancerTask;
    public static bool beholderTask;
    public static string lastTaskCompleted;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            slimeTask = true;
            lastTaskCompleted = "Slime";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            necromancerTask = true;
            lastTaskCompleted = "Necromancer";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            beholderTask = true;
            lastTaskCompleted = "Beholder";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            slimeTask = true;
            necromancerTask = true;
            beholderTask = false;
            lastTaskCompleted = "NecroAndSlime";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            slimeTask = false;
            necromancerTask = true;
            beholderTask = true;
            lastTaskCompleted = "NecroAndBeholder";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            slimeTask = true;
            beholderTask = true;
            necromancerTask = false;
            lastTaskCompleted = "SlimeAndBeholder";
            Debug.Log(lastTaskCompleted);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            slimeTask = false;
            necromancerTask = false;
            beholderTask = false;
            lastTaskCompleted = "";
            FindObjectOfType<DialogueTriggerConditional>().interacted = false;
            Debug.Log("Tasks reset");
        }
    }
}
