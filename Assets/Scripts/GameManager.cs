using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool escaped = false;
  public bool inputEnabled = false;
  public Animator animator;
  public Animator canvasAnimator;
  public Animator nighttimeAnimator;
  public Dictionary<int, string> inventory = new Dictionary<int, string>();
  public bool dogEnabled = false;
  DialogueManager dialogueManager;
  public bool ateMeat = false;
  public GameObject blackScreen;
  public AudioClip growl;
  public AudioSource windSource;
  public AudioClip breakGlass;
  public AudioClip wolfKill;
  public AudioSource geigerSource;
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
  public GameObject wolves;
  public Dialogue runAwayaDialogue;
  public Dialogue fightDialogue;
  public Interactable leaveDogPrompt;
  public Interactable raidHouse;
  public PlayerMovement player;
  public Dialogue enterHouseDialogue;
  private bool townSeen = false;
  public Dialogue runAwayaDialogueNoDog;
  public Dialogue dogBecomeEvilDialogue;
  private bool dogEvil = false;
  public Dialogue dogTurnDialogue;
  public Dialogue killedByDogDialogue;
  public Dialogue enterRadiationDialogue;
  public Dialogue toBeContinuedDialogue;

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
    // Hide wolves
    wolves.SetActive(false);

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
      nighttimeAnimator.SetBool("Nighttime", true);
    }));
  }

  public void Escape()
  {
    escaped = true;

    // Fade to black using canvas
    canvasAnimator.SetTrigger("Fade");
    // Disable input
    StartCoroutine(DisableInput(1.0f));
    // Teleport player to -9, -30
    StartCoroutine(DelayFunction(5, () =>
    {
      FindObjectOfType<PlayerMovement>().transform.position = new Vector3(-8.5f, -29, -1);
      // Disable reverb
      FindObjectOfType<AudioReverbFilter>().enabled = false;
      // Turn up wind
      windSource.volume = 0.8f;
      // Enable geiger counter if nighttime
      if (ateMeat)
      {
        geigerSource.Play();
      }
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
  public void UnStayBlack()
  {
    // Fade to black using canvas
    canvasAnimator.SetTrigger("UnFadePerm");
    // Disable input
    StartCoroutine(EnableInput(3.0f));

    StartCoroutine(DelayFunction(1f, () => blackScreen.SetActive(false)));
    // Enable black screen after fade
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
    StartCoroutine(DelayFunction(3f, () =>
    {
      // Play wolf kill sound
      windSource.PlayOneShot(wolfKill);
      dialogueManager.StartDialogue(runAwayaDialogue);
    }));
  }
  public void Fight()
  {
    // Fade to black
    StayBlack();
    // Show Dialogue
    StartCoroutine(DelayFunction(3f, () =>
    {
      // Play wolf kill sound
      windSource.PlayOneShot(wolfKill);
      dialogueManager.StartDialogue(fightDialogue);
    }));
  }

  public void CloseGame()
  {
    Application.Quit();
  }

  public void LeaevDogPrompt()
  {
    if (!dogEnabled)
    {
      return;
    }
    leaveDogPrompt.Interact();
  }
  public void LeaveDog()
  {
    dogEnabled = false;
  }
  public void KeepDog()
  {
    // Do nothing
  }

  public void RaidHouse()
  {
    // Disable if already ate meat or if have dog
    if (ateMeat || dogEnabled || townSeen)
    {
      return;
    }
    townSeen = true;
    raidHouse.Interact();
  }
  public void EnterHouse()
  {
    player.EnterHouse();
    // Darken screen

    StartCoroutine(DelayFunction(2f, () =>
    {
      StayBlack();
      StartCoroutine(DelayFunction(2f, () =>
      {
        // Break glass sound effect
        windSource.PlayOneShot(breakGlass);

        StartCoroutine(DelayFunction(1f, () =>
        {
          dialogueManager.StartDialogue(enterHouseDialogue);
        }));
      }));
    }));
  }

  public void EscapeRadiation()
  {
    if (!ateMeat || dogEnabled || townSeen)
    {
      return;
    }
    townSeen = true;
    raidHouse.Interact();
  }
  public void EnterRadiation()
  {
    player.EnterHouse();
    // Darken screen

    StartCoroutine(DelayFunction(2f, () =>
    {
      StayBlack();
      StartCoroutine(DelayFunction(2f, () =>
      {
        // Break glass sound effect
        windSource.PlayOneShot(breakGlass);

        StartCoroutine(DelayFunction(1f, () =>
        {
          dialogueManager.StartDialogue(enterRadiationDialogue);
        }));
      }));
    }));
  }
  public void WaitUntilDaytime()
  {
    StartCoroutine(DelayFunction(2f, () =>
    {
      nighttimeAnimator.SetBool("Nighttime", false);
      UnStayBlack();
      // Disable geiger
      geigerSource.Stop();

      StartCoroutine(DelayFunction(4f, () =>
      {
        dialogueManager.StartDialogue(toBeContinuedDialogue);
      }));
    }));
  }

  public void EscapeWolvesNoDog()
  {
    // Fade to black
    StayBlack();
    // Show Dialogue
    StartCoroutine(DelayFunction(3f, () =>
    {
      dialogueManager.StartDialogue(runAwayaDialogueNoDog);
      // Play wolf sound
      windSource.PlayOneShot(wolfKill);
    }));
  }

  public void DogBecomesEvil()
  {
    if (dogEvil)
    {
      return;
    }
    if (!ateMeat || !dogEnabled)
    {
      return;
    }
    dogEvil = true;

    // Dog bark
    DogFollow dog = FindObjectOfType<DogFollow>();
    dog.Bark();
    // Display dogFindFoodDialogue
    dialogueManager.StartDialogue(dogBecomeEvilDialogue);
  }
  public void DogTurns()
  {
    DogFollow dog = FindObjectOfType<DogFollow>();

    // Play growl
    dog.dogAudioSource.PlayOneShot(growl);

    StartCoroutine(DelayFunction(0.1f, () => dialogueManager.StartDialogue(dogTurnDialogue)));
  }
  public void KilledByDog()
  {
    // Fade to black
    StayBlack();
    // Show Dialogue
    StartCoroutine(DelayFunction(3f, () =>
    {
      dialogueManager.StartDialogue(killedByDogDialogue);
    }));
  }
}