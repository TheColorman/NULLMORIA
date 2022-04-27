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


  // Animation for dog to open door
  public void OpenDoor()
  {
    StartCoroutine(OpenDoorAnimation());
  }
  public IEnumerator OpenDoorAnimation()
  {
    // Move from current position
    // Start: transform.position
    // Step 1: 39.5, -1.5
    // Step 2: 39.5, -3.5
    // Step 3: 41.5, -3.5
    // Step 4: 41.5, -2.5
    float increment = 1.0f;

    // Step 1
    Vector2 start = transform.position;
    Vector2 end = new Vector2(39.5f, -1.5f);
    float time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }
    // Step 2
    start = end;
    end = new Vector2(39.5f, -3.5f);
    time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }
    // Step 3
    start = end;
    end = new Vector2(41.5f, -3.5f);
    time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }
    // Step 4
    start = end;
    end = new Vector2(41.5f, -2.5f);
    time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }

    // Open the door
    GameObject metalDoor = GameObject.FindGameObjectWithTag("MetalDoor");
    if (metalDoor != null)
    {
      Destroy(metalDoor);
    }
  }
}