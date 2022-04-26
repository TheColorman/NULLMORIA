using System.Collections;
using System.Collections.Generic;
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
}
