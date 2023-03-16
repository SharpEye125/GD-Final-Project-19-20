using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerConditional : MonoBehaviour
{
    [Tooltip("If Enabled: Utilizing firstTalkDialogue's sentences, the character will have an inital set of sentences to speak when first spoked to by the player.")]
    public bool progressingDialogue;
    public bool firstInteractDialogue;
    public bool interacted;
    public Dialogue dialogue;
    [TextArea(3, 10)]
    public string[] firstTalkSentences;

    [Header("Progressing Dialogues:")]
    [TextArea(3, 10)]
    public string[] slimeTaskDoneSentences;
    [TextArea(3, 10)]
    public string[] slimeTaskDoneSentencesFT;

    [TextArea(3, 10)]
    public string[] necroTaskDoneSentences;
    [TextArea(3, 10)]
    public string[] necroTaskDoneSentencesFT;
    
    [TextArea(3, 10)]
    public string[] beholderTaskDoneSentences;
    [TextArea(3, 10)]
    public string[] beholderTaskDoneSentencesFT;

    [TextArea(3, 10)]
    public string[] allTasksDoneSentences;
    [TextArea(3, 10)]
    public string[] allTasksDoneSentencesFT;


    public void TriggerDialogue()
    {
        var tempDial = dialogue.sentences;
        if (progressingDialogue == false)
        {
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
    }

    void SetCurrentTaskDialogue()
    {
        if (firstInteractDialogue == false || interacted == true)
        {
            if (TasksManager.necromancerTask == true)
            {
                dialogue.sentences = necroTaskDoneSentences;
            }
            if (TasksManager.slimeTask == true)
            {
                dialogue.sentences = slimeTaskDoneSentences;
            }
            if (TasksManager.beholderTask == true)
            {
                dialogue.sentences = beholderTaskDoneSentences;
            }
            if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == true)
            {
                dialogue.sentences = allTasksDoneSentences;
            }
        }
        else if (interacted == false)
        {
            interacted = true;
            if (TasksManager.necromancerTask == true)
            {
                dialogue.sentences = necroTaskDoneSentencesFT;
            }
            if (TasksManager.slimeTask == true)
            {
                dialogue.sentences = slimeTaskDoneSentencesFT;
            }
            if (TasksManager.beholderTask == true)
            {
                dialogue.sentences = beholderTaskDoneSentencesFT;
            }
            if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == true)
            {
                dialogue.sentences = allTasksDoneSentencesFT;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
