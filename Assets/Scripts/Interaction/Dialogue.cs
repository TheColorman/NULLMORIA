using UnityEngine;
using UnityEngine.Events;

// Object with a string and a UnityEvent
[System.Serializable]
public class Dialogue
{
  public string title;

  [TextArea(3, 10)]
  public string[] sentences;
  [TextArea(2, 10)]
  public string[] choices;
  public UnityEvent onEnd;


  public Dialogue(string[] lines, string title = null, UnityEvent onEnd = null)
  {
    this.title = title;
    this.sentences = lines;
  }
}
