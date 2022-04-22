using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
  public Dialogue dialogue;
  public UnityEvent onInteract;
  public void Interact()
  {
    // Interact with this interactable
    TriggerDialogue();
    // Run the onInteract event
    onInteract.Invoke();
  }

  public void TriggerDialogue()
  {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  }
}

