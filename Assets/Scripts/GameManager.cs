using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool inputEnabled = true;

  public IEnumerator EnableInput()
  {
    yield return new WaitForSeconds(0.1f);
    inputEnabled = true;
  }

}
