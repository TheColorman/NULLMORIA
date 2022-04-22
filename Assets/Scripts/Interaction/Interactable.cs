using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
using UnityEditor.Animations;

public class Interactable : MonoBehaviour
{
  public delegate bool AlternateCondition();
  public AlternateCondition alternateCondition;
  public Dialogue dialogue;
  public Dialogue alternateDialogue;

  public UnityEvent onInteract;
  public void Interact()
  {
    // Interact with this interactable
    TriggerDialogue();
    // Run the onInteract event if condition is met
    if (alternateCondition == null || alternateCondition())
    {
      onInteract.Invoke();
    }
  }

  public void TriggerDialogue()
  {
    if (alternateCondition != null && alternateCondition())
    {
      FindObjectOfType<DialogueManager>().StartDialogue(alternateDialogue);
    }
    else
    {
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
  }
}