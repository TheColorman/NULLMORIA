using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesHandler : MonoBehaviour
{
  List<Interactable> interactables = new List<Interactable>();
  private GameManager gameManager;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
  }

  // Add interactable to list if entered trigger
  void OnTriggerEnter2D(Collider2D other)
  {
    Interactable i = other.GetComponent<Interactable>();
    if (i != null)
    {
      interactables.Add(i);
    }
  }
  // Remove interactable from list if left trigger
  void OnTriggerExit2D(Collider2D other)
  {
    Interactable i = other.GetComponent<Interactable>();
    if (i != null)
    {
      interactables.Remove(i);
    }
  }

  // When play presses interact
  public void Interact()
  {
    // If there is an interactable in the list
    if (interactables.Count > 0)
    {
      // Find closest interactable
      Interactable closest = interactables[0];
      foreach (Interactable i in interactables)
      {
        if (Vector3.Distance(transform.position, i.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
        {
          closest = i;
        }
      }
      // Interact with closest interactable
      closest.Interact();
      Debug.Log("interacting with ");
    }
  }

  void Update()
  {
    // If play presses "E"
    if (Input.GetKeyDown(KeyCode.E))
    {
      if (gameManager.inputEnabled == false)
      {
        return;
      }
      Debug.Log(gameManager.inputEnabled);
      Interact();
    }
  }
}
