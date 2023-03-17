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

    [Space]
    [Header("Progressing Dialogues:")]
    [TextArea(3, 10)]
    //HeaderAttribute[]
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
    [Header("Inbetween Tasks")]
    [Space]
    [TextArea(3, 10)]
    public string[] necroAndSlimeDone;
    [TextArea(3, 10)]
    public string[] necroAndSlimeDoneFT;
    [TextArea(3, 10)]

    public string[] necroAndBeholderDone;
    [TextArea(3, 10)]
    public string[] necroAndBeholderDoneFT;
    [TextArea(3, 10)]

    public string[] slimeAndBeholderDone;
    [TextArea(3, 10)]
    public string[] slimeAndBeholderDoneFT;


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
        else
        {
            SetCurrentTaskDialogue(tempDial);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogue.sentences = tempDial;
        }
    }

    void SetCurrentTaskDialogue(string[] tempDial)
    {
        if (firstInteractDialogue == false || interacted == true)
        {
            if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == true)
            {
                dialogue.sentences = allTasksDoneSentences;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == false && TasksManager.lastTaskCompleted == "NecroAndSlime")
            {
                dialogue.sentences = necroAndSlimeDone;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.slimeTask == false && TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "NecroAndBeholder")
            {
                dialogue.sentences = necroAndBeholderDone;
            }
            else if (TasksManager.necromancerTask == false && TasksManager.slimeTask == true && TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "SlimeAndBeholder")
            {
                dialogue.sentences = slimeAndBeholderDone;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.lastTaskCompleted == "Necromancer")
            {
                dialogue.sentences = necroTaskDoneSentences;
            }
            else if (TasksManager.slimeTask == true && TasksManager.lastTaskCompleted == "Slime")
            {
                dialogue.sentences = slimeTaskDoneSentences;
            }
            else if (TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "Beholder")
            {
                dialogue.sentences = beholderTaskDoneSentences;
            }
            else if (TasksManager.necromancerTask == false && TasksManager.slimeTask == false && TasksManager.beholderTask == false)
            {
                dialogue.sentences = tempDial;
            }
        }
        else if (interacted == false)
        {
            interacted = true;
            if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == true)
            {
                dialogue.sentences = allTasksDoneSentencesFT;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.slimeTask == true && TasksManager.beholderTask == false && TasksManager.lastTaskCompleted == "NecroAndSlime")
            {
                dialogue.sentences = necroAndSlimeDoneFT;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.slimeTask == false && TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "NecroAndBeholder")
            {
                dialogue.sentences = necroAndBeholderDoneFT;
            }
            else if (TasksManager.necromancerTask == false && TasksManager.slimeTask == true && TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "SlimeAndBeholder")
            {
                dialogue.sentences = slimeAndBeholderDoneFT;
            }
            else if (TasksManager.necromancerTask == true && TasksManager.lastTaskCompleted == "Necromancer")
            {
                dialogue.sentences = necroTaskDoneSentencesFT;
            }
            else if (TasksManager.slimeTask == true && TasksManager.lastTaskCompleted == "Slime")
            {
                dialogue.sentences = slimeTaskDoneSentencesFT;
            }
            else if (TasksManager.beholderTask == true && TasksManager.lastTaskCompleted == "Beholder")
            {
                dialogue.sentences = beholderTaskDoneSentencesFT;
            }
            else if (TasksManager.necromancerTask == false && TasksManager.slimeTask == false && TasksManager.beholderTask == false)
            {
                dialogue.sentences = firstTalkSentences;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
