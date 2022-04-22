using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScripts : MonoBehaviour
{
  GameManager gameManager;
  [Header("Item")]
  public string itemName;
  public int itemID;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
  }

  public void PickupItem()
  {
    // Pick up this item
    gameManager.inventory.Add(itemName, itemID);
    Destroy(gameObject);
  }
}
