using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("If Enabled: Utilizing firstTalkDialogue's sentences, the character will have an inital set of sentences to speak when first spoked to by the player.")]
    public bool firstInteractDialogue;
    public bool interacted;
    //public bool changingExpressions;
    [Header("Dialogues")]
    public Dialogue dialogue;
    [TextArea(3, 10)]
    public string[] firstTalkSentences;


    public void TriggerDialogue()
    {
        string[] tempDial = dialogue.sentences;
        if (firstInteractDialogue == false || interacted == true)
        {
            dialogue.sentences = tempDial;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else if (interacted == false)
        {
            interacted = true;
            dialogue.sentences = firstTalkSentences;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogue.sentences = tempDial;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
