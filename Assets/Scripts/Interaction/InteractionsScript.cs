using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsScript : MonoBehaviour
{
  public Interactable interactable;
  GameManager gameManager;
  [Header("Item")]
  public string itemName;
  public int itemID;
  [Header("Door")]
  public int keyID;
  [Header("Dog")]
  public bool dog = false;
  // Sounds
  public AudioClip[] dogSounds;
  public AudioSource dogAudioSource;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    if (interactable == null)
    {
      interactable = GetComponent<Interactable>();
    }
    if (keyID != 0)
    {
      interactable.alternateCondition = AlternateCondition;
    }
    if (dog)
    {
      interactable.alternateCondition = DogCondition;
    }
  }

  void Update()
  {
    if (dog)
    {
      // Play sounds at random intervals
      if (Random.Range(0, 1000) < 1)
      {
        if (dogAudioSource.isPlaying == false)
        {
          dogAudioSource.clip = dogSounds[Random.Range(0, dogSounds.Length)];
          dogAudioSource.Play();
        }
      }
    }
  }

  public void PickupItem()
  {
    // Pick up this item
    gameManager.inventory.Add(itemID, itemName);
    Destroy(gameObject);
  }

  public void OpenDoor()
  {
    // Open the door
    Destroy(gameObject);
  }
  bool AlternateCondition()
  {
    return gameManager.inventory.ContainsKey(keyID);
  }

  public void EnableDog()
  {
    gameManager.dogEnabled = true;
  }
  public bool DogCondition()
  {
    return !gameManager.dogEnabled;
  }
}
