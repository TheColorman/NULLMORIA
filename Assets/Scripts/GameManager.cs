using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool inputEnabled = false;
  public Animator animator;
  public Animator canvasAnimator;
  public Animator nighttimeAnimator;
  public Dictionary<int, string> inventory = new Dictionary<int, string>();
  public bool dogEnabled = false;
  DialogueManager dialogueManager;
  public bool ateMeat = false;
  public GameObject blackScreen;
  [Header("Dialogues")]
  public Dialogue eatMeatDialogue;
  public Dialogue postEatMeatDialogue;
  public Dialogue dontEatMeatDialogue;
  public Dialogue dogFindFoodDialogue;
  private bool dogFoundFood = false;
  public Dialogue dogAwayDialogue;
  public Dialogue dogReturnDialogue;
  public Interactable seeWolves;
  private bool wolvesSeen = false;
  public Dialogue runAwayaDialogue;
  public Dialogue fightDialogue;

  void Start()
  {
    dialogueManager = FindObjectOfType<DialogueManager>();
  }
  public IEnumerator EnableInput(float delay = 0.5f)
  {
    yield return new WaitForSeconds(delay);
    inputEnabled = true;
  }

  public IEnumerator DisableInput(float delay = 0.0f)
  {
    yield return new WaitForSeconds(delay);
    inputEnabled = false;
  }

  public IEnumerator DelayFunction(float delay, System.Action function)
  {
    yield return new WaitForSeconds(delay);
    function();
  }

  public void EatMeat()
  {
    // Start coroutine
    StartCoroutine(StartDialogue(eatMeatDialogue));
    ateMeat = true;
  }
  public void DontEatMeat()
  {
    // Start coroutine
    StartCoroutine(StartDialogue(dontEatMeatDialogue));
    ateMeat = false;
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
    StartCoroutine(DisableInput(1.0f));
    // Enable input after fade
    StartCoroutine(EnableInput(10f));
    StartCoroutine(DelayFunction(10f, () =>
    {
      dialogueManager.StartDialogue(postEatMeatDialogue);
    }));
    StartCoroutine(DelayFunction(5f, () =>
    {
      nighttimeAnimator.SetTrigger("Nighttime");
    }));
  }

  public void Escape()
  {
    // Fade to black using canvas
    canvasAnimator.SetTrigger("Fade");
    // Disable input
    StartCoroutine(DisableInput(1.0f));
    // Teleport player to -9, -30
    StartCoroutine(DelayFunction(5, () =>
    {
      FindObjectOfType<PlayerMovement>().transform.position = new Vector3(-8.5f, -29, -1);
    }));
    // Enable input after fade
    StartCoroutine(EnableInput(10f));
  }

  public void DogFindFood()
  {
    if (dogFoundFood)
    {
      return;
    }
    if (!dogEnabled || ateMeat)
    {
      return;
    }
    dogFoundFood = true;

    // Dog bark
    DogFollow dog = FindObjectOfType<DogFollow>();
    dog.Bark();
    // Display dogFindFoodDialogue
    dialogueManager.StartDialogue(dogFindFoodDialogue);
  }
  public void DogAway()
  {

    StartCoroutine(DelayFunction(0.1f, () => dialogueManager.StartDialogue(dogAwayDialogue)));
  }
  public void DogReturn()
  {
    DogFollow dog = FindObjectOfType<DogFollow>();
    dog.Bark();
    dog.ReturnFromForest();
    StartCoroutine(DelayFunction(1f, () =>
    {
      dialogueManager.StartDialogue(dogReturnDialogue);
    }));
  }
  public void FadeToBlackReal()
  {
    // Fade to black using canvas
    canvasAnimator.SetTrigger("Fade");
    // Disable input
    StartCoroutine(DisableInput(1.0f));
    // Enable input after fade
    StartCoroutine(EnableInput(10f));
  }
  public void StayBlack()
  {
    // Fade to black using canvas
    canvasAnimator.SetTrigger("FadePerm");
    // Disable input
    StartCoroutine(DisableInput(1.0f));
    // Enable black screen after fade
    StartCoroutine(DelayFunction(2f, () =>
    {
      blackScreen.SetActive(true);
    }));
  }

  public void SeeWolves()
  {
    if (wolvesSeen)
    {
      return;
    }
    wolvesSeen = true;
    seeWolves.Interact();
  }
  public void EscapeWolves()
  {
    // Fade to black
    StayBlack();
    // Show Dialogue
    StartCoroutine(DelayFunction(5f, () =>
    {
      dialogueManager.StartDialogue(runAwayaDialogue);
    }));
  }
  public void Fight()
  {
    // Fade to black
    StayBlack();
    // Show Dialogue
    StartCoroutine(DelayFunction(5f, () =>
    {
      dialogueManager.StartDialogue(fightDialogue);
    }));
  }

  public void CloseGame()
  {
    Application.Quit();
  }
}