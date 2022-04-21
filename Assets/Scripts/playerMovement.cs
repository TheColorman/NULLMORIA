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
  private Camera cam;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    cam = Camera.main;
  }
  // Update is called once per physics frame
  void Update()
  {
    // Move camera if player gets close to edge of screen
    if (transform.position.x < cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
    {
      cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
    if (transform.position.x > cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
    {
      cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
    if (transform.position.y < cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
    {
      cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
    }
    if (transform.position.y > cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
    {
      cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
    }

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
