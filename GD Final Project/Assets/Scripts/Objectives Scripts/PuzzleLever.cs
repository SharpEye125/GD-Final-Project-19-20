using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLever : MonoBehaviour
{
    public bool isActive = false;
    public float interactRange = 1;

    Animator anim;
    Transform player;
    public GameObject[] puzzleObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlatformerMove>().transform;
        anim.SetBool("isActive", isActive);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteractCheck();

    }
    public void PlayerInteractCheck()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= interactRange && Input.GetKeyDown(KeyCode.E))// && player.gameObject.GetComponent<PlatformerMove>().grounded == true)
        {
            if (isActive)
            {
                isActive = false;
                anim.SetBool("isActive", isActive);
                foreach (GameObject puzzle in puzzleObject)
                {
                    if (puzzle.activeSelf == true)
                    {
                        puzzle.SetActive(false);
                    }
                    else
                    {
                        puzzle.SetActive(true);
                    }
                }
            }
            else
            {
                isActive = true;
                anim.SetBool("isActive", isActive);
                foreach (GameObject puzzle in puzzleObject)
                {
                    if (puzzle.activeSelf == false)
                    {
                        puzzle.SetActive(true);
                    }
                    else
                    {
                        puzzle.SetActive(false);
                    }
                }
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (GameObject puzzle in puzzleObject)
        {
            Gizmos.DrawLine(transform.position, puzzle.transform.position);

        }
        
    }
}
