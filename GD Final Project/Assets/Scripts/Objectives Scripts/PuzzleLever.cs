using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLever : MonoBehaviour
{
    public bool isActive = false;
    public bool puzzleObjStartActive = false;
    public float interactRange = 1;

    Animator anim;
    Transform player;
    public GameObject puzzleObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlatformerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteractCheck();

        if (isActive)
        {
            anim.SetBool("isActive", isActive);
            if (puzzleObjStartActive)
            {
                puzzleObject.SetActive(false);
            }
            else
            {
                puzzleObject.SetActive(true);
            }
            

        }
        else
        {
            anim.SetBool("isActive", isActive);
            if (puzzleObjStartActive)
            {
                puzzleObject.SetActive(true);
            }
            else
            {
                puzzleObject.SetActive(false);
            }

        }
    }
    public void PlayerInteractCheck()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= interactRange && Input.GetKeyDown(KeyCode.E) && player.gameObject.GetComponent<PlatformerMove>().grounded == true)
        {
            if (isActive)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }

        }
    }
}
