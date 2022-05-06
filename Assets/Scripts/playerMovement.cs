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

  public void EnterHouse()
  {
    gameManager.inputEnabled = false;
    StartCoroutine(EnterHouseAnimation());
  }
  IEnumerator EnterHouseAnimation()
  {
    // Move from current position
    // Start: transform.position
    // Step 1: 14.5, -48.5
    // Step 2: 16.7, -47.2
    float increment = 1.0f;

    // Step 1
    Vector2 start = transform.position;
    Vector2 end = new Vector2(14.5f, -48.5f);
    float time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }

    // Step 2
    start = end;
    end = new Vector2(16.7f, -47.2f);
    time = 0.0f;
    while (time < 1.0f)
    {
      time += increment * Time.deltaTime;
      transform.position = Vector2.Lerp(start, end, time);
      yield return null;
    }
  }
}
