using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool inputEnabled = false;
  public Animator animator;
  public Animator canvasAnimator;
  public Dictionary<int, string> inventory = new Dictionary<int, string>();
  public bool dogEnabled = false;
  DialogueManager dialogueManager;
  [Header("Dialogues")]
  public Dialogue eatMeatDialogue;
  public Dialogue dontEatMeatDialogue;

  void Start()
  {
    dialogueManager = FindObjectOfType<DialogueManager>();
  }
  public IEnumerator EnableInput()
  {
    yield return new WaitForSeconds(0.5f);
    inputEnabled = true;
  }

  public void DisableInput()
  {
    inputEnabled = false;
  }

  public void EatMeat()
  {
    // Start coroutine
    StartCoroutine(StartDialogue(eatMeatDialogue));
  }
  public void DontEatMeat()
  {
    // Start coroutine
    StartCoroutine(StartDialogue(dontEatMeatDialogue));
  }
  IEnumerator StartDialogue(Dialogue dialogue)
  {
    yield return new WaitForSeconds(0.2f);
    dialogueManager.StartDialogue(dialogue);
  }
  public void FadeToBlack()
  {
    // Fade to black using canvas
    canvasAnimator.SetTrigger("Fade");
    // Disable input
    inputEnabled = false;
    // Enable input after fade
    StartCoroutine(EnableInput());
  }
}
