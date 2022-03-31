using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
  public Dialogue dialogue;
  public void Interact()
  {
    // Interact with this interactable
    TriggerDialogue();
  }

  public void TriggerDialogue()
  {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  }
}
