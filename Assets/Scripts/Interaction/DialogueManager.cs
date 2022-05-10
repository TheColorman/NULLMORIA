using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
  public Text titleText;
  public Text dialogueText;

  public Animator animator;
  public AudioSource audioSource;

  private Queue<string> sentences = new Queue<string>();
  private List<string> choices = new List<string>();
  private GameManager gameManager;
  private int currentChoice = 0;
  public List<GameObject> choiceTexts = new List<GameObject>();
  private List<GameObject> choiceArrows = new List<GameObject>();
  private bool choiceActive = false;
  private GameManager manager;
  private Interactable interactable;
  private UnityEvent onEnd;

  // Initialization
  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    // Get all first children of choices
    foreach (GameObject choice in choiceTexts)
    {
      choiceArrows.Add(choice.transform.GetChild(0).gameObject);
    }
    // Hide all choices and arrows
    foreach (GameObject text in choiceTexts)
    {
      text.SetActive(false);
    }
    foreach (GameObject arrow in choiceArrows)
    {
      arrow.SetActive(false);
    }

    manager = FindObjectOfType<GameManager>();
  }
  void Update()
  {
    // Dialogue skip keybind ("E")
    if (Input.GetKeyDown(KeyCode.E) && gameManager.inputEnabled == false)
    {
      DisplayNextSentence();
    }
    // Next/previous choice if pressing W or S
    if (Input.GetKeyDown(KeyCode.W) && gameManager.inputEnabled == false && choiceActive)
    {
      NextChoice();
    }
    if (Input.GetKeyDown(KeyCode.S) && gameManager.inputEnabled == false && choiceActive)
    {
      PreviousChoice();
    }
  }

  public void StartDialogue(Dialogue dialogue, Interactable inter = null)
  {
    onEnd = dialogue.onEnd;

    interactable = inter;
    animator.SetBool("IsOpen", true);

    gameManager.inputEnabled = false;

    titleText.text = dialogue.title;

    sentences.Clear();
    choices.Clear();

    choiceActive = false;

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }
    foreach (string choice in dialogue.choices)
    {
      choices.Add(choice);
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (choiceActive)
    {
      interactable.onInteractChoices[currentChoice].Invoke();
      EndDialogue();
      return;
    }
    if (sentences.Count == 0)
    {
      // Clear dialogue text
      dialogueText.text = "";

      // Display choices
      if (choices.Count > 0)
      {
        // Display choices
        for (int i = 0; i < choices.Count; i++)
        {
          choiceTexts[i].SetActive(true);
          choiceTexts[i].GetComponent<Text>().text = choices[i];
        }
        // Set current choice to 0
        currentChoice = 0;
        choiceArrows[currentChoice].SetActive(true);
        // Disable dialogue text
        dialogueText.gameObject.SetActive(false);
        // Enable choice bool
        choiceActive = true;
      }
      else
      {
        EndDialogue();
      }
      return;
    }

    string sentence = sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  IEnumerator TypeSentence(string sentence)
  {
    dialogueText.text = "";
    yield return new WaitForSeconds(0.3f);
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      // Random chance to play sound
      audioSource.Play();
      yield return new WaitForSeconds(0.03f);
    }
  }

  void EndDialogue()
  {
    StartCoroutine(gameManager.EnableInput());

    if (interactable && interactable.onInteractEnd != null)
    {
      interactable.onInteractEnd.Invoke();
    }
    if (onEnd != null)
    {
      onEnd.Invoke();
    }

    // Enable dialogue text
    dialogueText.gameObject.SetActive(true);
    // Disable choices and arrows
    foreach (GameObject text in choiceTexts)
    {
      text.SetActive(false);
    }
    foreach (GameObject arrow in choiceArrows)
    {
      arrow.SetActive(false);
    }
    choiceActive = false;
    animator.SetBool("IsOpen", false);
  }

  void NextChoice()
  {
    // Return if not at the end of dialogue
    if (sentences.Count > 0)
    {
      return;
    }
    if (currentChoice < choices.Count - 1)
    {
      currentChoice++;
    }
    else
    {
      currentChoice = 0;
    }
    // Hide all arrows
    foreach (GameObject arrow in choiceArrows)
    {
      arrow.SetActive(false);
    }
    // Show current arrow
    choiceArrows[currentChoice].SetActive(true);
  }
  void PreviousChoice()
  {
    // Return if not at the end of dialogue
    if (sentences.Count > 0)
    {
      return;
    }
    if (currentChoice > 0)
    {
      currentChoice--;
    }
    else
    {
      currentChoice = choices.Count - 1;
    }
    // Hide all arrows
    foreach (GameObject arrow in choiceArrows)
    {
      arrow.SetActive(false);
    }
    // Show current arrow
    choiceArrows[currentChoice].SetActive(true);
  }
}
