using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFoodTrigger : MonoBehaviour
{
  GameManager manager;

  void Start()
  {
    manager = FindObjectOfType<GameManager>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      manager.DogFindFood();
    }
  }
}
