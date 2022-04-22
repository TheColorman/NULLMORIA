using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool inputEnabled = false;
  public Animator animator;
  public Dictionary<string, int> inventory = new Dictionary<string, int>();
  public bool dogEnabled = false;

  public IEnumerator EnableInput()
  {
    yield return new WaitForSeconds(0.1f);
    inputEnabled = true;
  }

  public void DisableInput()
  {
    inputEnabled = false;
  }
}
