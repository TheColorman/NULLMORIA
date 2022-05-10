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
  [Header("Wolves")]
  public bool wolves = false;
  [Header("House")]
  public bool house = false;
  [Header("Shelf")]
  public bool shelf = false;
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
    if (wolves)
    {
      interactable.alternateCondition = () => !gameManager.dogEnabled && !gameManager.ateMeat;
    }
    if (house)
    {
      interactable.alternateCondition = () => gameManager.ateMeat;
    }
    if (shelf)
    {
      interactable.alternateCondition = () => gameManager.ateMeat;
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
