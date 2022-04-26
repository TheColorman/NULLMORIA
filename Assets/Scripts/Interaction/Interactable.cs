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
  public UnityEvent onInteractAlternate;
  public UnityEvent[] onInteractChoices;
  public UnityEvent onInteractEnd;
  public void Interact()
  {
    // Interact with this interactable
    TriggerDialogue();
    // Run the onInteract event if condition is met
    if (alternateCondition == null || alternateCondition())
    {
      onInteract.Invoke();
    } else
    {
      onInteractAlternate.Invoke();
    }
  }

  public void TriggerDialogue()
  {
    if (alternateCondition != null && alternateCondition())
    {
      FindObjectOfType<DialogueManager>().StartDialogue(alternateDialogue, this);
    }
    else
    {
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
    }
  }
}