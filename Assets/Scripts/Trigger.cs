using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
  public UnityEvent OnTriggerEnter;
  public UnityEvent OnTriggerExit;

  void OnTriggerEnter2D(Collider2D other)
  {
    OnTriggerEnter.Invoke();
  }

  void OnTriggerExit2D(Collider2D other)
  {
    OnTriggerExit.Invoke();
  }
}
