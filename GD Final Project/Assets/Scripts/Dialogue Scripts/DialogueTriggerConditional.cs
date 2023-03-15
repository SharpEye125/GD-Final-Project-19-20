using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerConditional : MonoBehaviour
{
    [Tooltip("If Enabled: Utilizing firstTalkDialogue's sentences, the character will have an inital set of sentences to speak when first spoked to by the player.")]
    public bool firstInteractDialogue;
    public bool interacted;
    public Dialogue dialogue;
    [Tooltip("The non sentence variables will automatically be set to the normal dialogue variables.")]
    public Dialogue firstTalkDialogue;


    public void TriggerDialogue()
    {
        if (firstInteractDialogue == false || interacted == true)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else if (interacted == false)
        {
            interacted = true;
            FindObjectOfType<DialogueManager>().StartDialogue(firstTalkDialogue);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(firstTalkDialogue.name == null)
        {
            firstTalkDialogue.name = dialogue.name;
        }
        if (firstTalkDialogue.portrait == null)
        {
            firstTalkDialogue.portrait = dialogue.portrait;
        }
        if (firstTalkDialogue.portrait == null)
        {
            firstTalkDialogue.voice = dialogue.voice;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
