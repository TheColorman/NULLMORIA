using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
  List<Vector2> previousPlayerPositions = new List<Vector2>();
  GameObject player;
  Rigidbody2D rb;
  Animator animator;
  GameManager gameManager;
  public AudioClip[] dogSounds;
  public AudioSource dogAudioSource;
  Interactable interactable;

  // Start is called before the first frame update
  void Start()
  {
    interactable = GetComponentInChildren<Interactable>();
    player = GameObject.FindGameObjectWithTag("Player");
    previousPlayerPositions.Add(player.transform.position);
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    gameManager = FindObjectOfType<GameManager>();
    dogAudioSource = GetComponent<AudioSource>();
    interactable.alternateCondition = DogCondition;
  }

  // Called once per physics frame
  void FixedUpdate()
  {
    if (gameManager.dogEnabled == false)
    {
      return;
    }
    // Add current player position to list ONLY if the player has moved at least 0.4 units
    if (Vector2.Distance(player.transform.position, previousPlayerPositions[previousPlayerPositions.Count - 1]) > 0.05f)
    {
      previousPlayerPositions.Add(player.transform.position);
    }
    // Remove oldest position from list if has more than 100 entries
    if (previousPlayerPositions.Count > 20)
    {
      previousPlayerPositions.RemoveAt(0);
      // Move towards first position in list
      rb.MovePosition(previousPlayerPositions[0]);
    }
  }

  void Update()
  {
    if (gameManager.dogEnabled == false)
    {
      return;
    }
    if (previousPlayerPositions.Count < 2)
    {
      return;
    }
    // Calculate movement
    Vector2 difference = previousPlayerPositions[1] - previousPlayerPositions[0];
    // Set animator speed
    animator.SetFloat("Speed", difference.sqrMagnitude);
    // Set animator direction
    animator.SetFloat("Horizontal", difference.x);
    animator.SetFloat("Vertical", difference.y);
  }

  public void Bark()
  {
    if (dogAudioSource.isPlaying == false)
    {
      dogAudioSource.clip = dogSounds[Random.Range(0, dogSounds.Length)];
      dogAudioSource.Play();
    }

  }
  public bool DogCondition()
  {
    return !gameManager.dogEnabled;
  }
  public void EnableDog()
  {
    gameManager.dogEnabled = true;
  }

}