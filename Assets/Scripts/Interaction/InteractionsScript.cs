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

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    if (keyID != 0)
    {
      interactable.alternateCondition = alternateCondition;
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
  bool alternateCondition()
  {
    return gameManager.inventory.ContainsKey(0);
  }
}
