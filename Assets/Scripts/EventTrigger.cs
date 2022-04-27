using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
  private GameManager manager;

  void Start()
  {
    manager = FindObjectOfType<GameManager>();
  }

  // Check trigger
  void OnTriggerEnter2D(Collider2D other)
  {
    // If player enters trigger
    if (other.tag == "Player")
    {
      manager.Escape();
    }
  }
}
