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
  // Sounds

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
    if (keyID == 2)
    {
      interactable.alternateCondition = () => gameManager.dogEnabled;
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

}
