using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f * 2;

    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded;

    public Vector3 velocity;

    public static bool isMoving;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;

        if(move != Vector3.zero)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

         controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
