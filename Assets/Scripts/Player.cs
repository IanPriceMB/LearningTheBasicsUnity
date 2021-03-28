using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  public Transform groundCheckTransform;
  [SerializeField] private LayerMask playerMask;
  private bool jumpKeyWasPressed;
  private float horizontalInput;
  private Rigidbody rigidbodyComponent;
  private int superJumpsRemaining;
  // private bool isGrounded;

  // Start is called before the first frame update
  void Start()
  {
    rigidbodyComponent = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {

    // if (!isGrounded)
    // {
    //   return;
    // }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jumpKeyWasPressed = true;
    }

    horizontalInput = Input.GetAxis("Horizontal");
  }
  
  // FixedUpdate is called once every physics update
  void FixedUpdate()
  {

    rigidbodyComponent.velocity = new Vector3(horizontalInput,rigidbodyComponent.velocity.y, 0);

    // if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)
    if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
    {
      return;
    }

    if (jumpKeyWasPressed)
    {
      int jumpPower = 7;

      if (superJumpsRemaining > 0)
      {
        jumpPower = 10;
        superJumpsRemaining--;
      }
      rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
      jumpKeyWasPressed = false;
    }

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 9)
    {
      Destroy(other.gameObject);
      superJumpsRemaining++;
    }
  }

  // void OnCollisionEnter(Collision collision)
  // {
  //   isGrounded = true;
  // }

  // void OnCollisionExit(Collision collision)
  // {
  //   isGrounded = false;
  // }
}
