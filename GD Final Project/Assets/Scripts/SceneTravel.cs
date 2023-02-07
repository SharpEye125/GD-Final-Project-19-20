using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTravel : MonoBehaviour
{

    public int sceneNumber;
    public Transform player;
    public float inputRange = 2f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= inputRange && Input.GetKeyDown(KeyCode.E) && player.gameObject.GetComponent<PlatformerMove>().grounded == true && player.gameObject.GetComponent<PlayerDie>().dead != true)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
