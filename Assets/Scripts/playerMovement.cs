using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;
  public Rigidbody2D rb;
  public Animator animator;
  Vector2 movement;
  private GameManager gameManager;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
  }
  // Update is called once per physics frame
  void Update()
  {
    // Input
    if (gameManager.inputEnabled == false)
    {
      movement = Vector2.zero;
      animator.SetFloat("Speed", 0);
      return;
    }
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");

    // Normalize
    if (movement != Vector2.zero)
    {
      movement = movement.normalized;
    }

    animator.SetFloat("Horizontal", movement.x);
    animator.SetFloat("Vertical", movement.y);
    animator.SetFloat("Speed", movement.sqrMagnitude);
  }

  // Update is called once per physics tick
  void FixedUpdate()
  { // Movement
    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
  }
}
