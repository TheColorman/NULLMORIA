using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
    List<Vector2> previousPlayerPositions = new List<Vector2>();
    GameObject player;
    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        previousPlayerPositions.Add(player.transform.position);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Called once per physics frame
    void FixedUpdate()
    {
        // Add current player position to list ONLY if the player has moved at least 0.4 units
        if (Vector2.Distance(player.transform.position, previousPlayerPositions[previousPlayerPositions.Count - 1]) > 0.05f)
        {
            previousPlayerPositions.Add(player.transform.position);
        }
        // Remove oldest position from list if has more than 100 entries
        if (previousPlayerPositions.Count > 20)
        {
            previousPlayerPositions.RemoveAt(0);
          // Move towards first position in list
          rb.MovePosition(previousPlayerPositions[0]);
        } 
    }

    void Update()
    {
      if (previousPlayerPositions.Count < 2)
      {
        return;
      }
      // Calculate movement
      Vector2 difference = previousPlayerPositions[1] - previousPlayerPositions[0];
      // Set animator speed
      animator.SetFloat("Speed", difference.sqrMagnitude);
      // Set animator direction
      animator.SetFloat("Horizontal", difference.x);
      animator.SetFloat("Vertical", difference.y);
    }
}