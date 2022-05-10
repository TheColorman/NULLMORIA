using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
  public UnityEvent OnTriggerEnter;
  public UnityEvent OnTriggerExit;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      OnTriggerEnter.Invoke();
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      OnTriggerExit.Invoke();
    }
  }
  public void DisableTrigger()
  {
    gameObject.SetActive(false);
  }
}
