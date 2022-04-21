using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
  private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

  public void EnableInput()
  {
    gameManager.StartCoroutine(gameManager.EnableInput());
  }
}
