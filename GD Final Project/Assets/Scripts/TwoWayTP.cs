using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayTP : MonoBehaviour
{
    public Transform point;
    public Transform player;
    public float inputRange = 2f;
    public Vector2 tpOffset = new Vector2(0, -0.8f);
    public float tpDelay = 0.5f;
    float timer;
    PlatformerMove pm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pm = player.GetComponent<PlatformerMove>();
        timer += Time.deltaTime;
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= inputRange && Input.GetKeyDown(KeyCode.E) && pm.grounded == true && player.gameObject.GetComponent<PlayerDie>().dead != true && timer >= tpDelay)
        {
            player.position = point.position + new Vector3(tpOffset.x, tpOffset.y, point.position.z);
            Debug.Log("TPing to point 2");
            timer = 0;
            return;
        }
        Vector2 pointDistance = new Vector2(point.position.x - player.position.x, point.position.y - player.position.y);
        if (pointDistance.magnitude <= inputRange && Input.GetKeyDown(KeyCode.E) && pm.grounded == true && player.gameObject.GetComponent<PlayerDie>().dead != true && timer >= tpDelay)
        {
            player.position = transform.position + new Vector3(tpOffset.x, tpOffset.y, transform.position.z);
            Debug.Log("TPing to point 1");
            timer = 0;
        }
    }
}
