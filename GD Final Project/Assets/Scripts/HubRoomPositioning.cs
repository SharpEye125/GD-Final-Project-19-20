using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubRoomPositioning : MonoBehaviour
{
    public static Vector3 startPosition;
    public static string temp;
    public static Vector3 offset = new Vector3(0, -0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && startPosition != new Vector3(0,0,0))
        {
            transform.position = startPosition + offset;
            Debug.Log("Started at " + temp);
        }
        else
        {
            Debug.Log("No Start Point set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            startPosition = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            startPosition = collision.transform.position;
            temp = collision.name;
            Debug.Log("Start point " + temp + " has been set.");
        }
    }
}
