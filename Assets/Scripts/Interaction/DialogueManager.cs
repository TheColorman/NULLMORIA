using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
  public Text titleText;
  public Text dialogueText;

  public Animator animator;
  public AudioSource audioSource;

  private Queue<string> sentences;
  private GameManager gameManager;

  // Initialization
  void Start()
  {
    sentences = new Queue<string>();
    gameManager = FindObjectOfType<GameManager>();
  }
  void Update()
  {
    // Dialogue skip keybind ("E")
    if (Input.GetKeyDown(KeyCode.E) && gameManager.inputEnabled == false)
    {
      DisplayNextSentence();
    }
  }

  public void StartDialogue(Dialogue dialogue)
  {
    animator.SetBool("IsOpen", true);

    gameManager.inputEnabled = false;

    titleText.text = dialogue.title;

    sentences.Clear();

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (sentences.Count == 0)
    {
      EndDialogue();
      return;
    }

    string sentence = sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  IEnumerator TypeSentence(string sentence)
  {
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      audioSource.PlayOneShot(audioSource.clip);
      yield return null;
    }
  }

  void EndDialogue()
  {
    animator.SetBool("IsOpen", false);
    StartCoroutine(gameManager.EnableInput());
  }
}
