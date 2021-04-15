using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed;

    public VariableJoystick variableJoystick;
    public Rigidbody2D rb;
    public float JumpForce;
    public float MovementSpeed;
    public float MaxSpeed;
    public float AirSpeed;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask collisionlayer;
    public Button jumpButton;

    public void Start()
    {
        jumpButton.onClick.AddListener(JumpOnClick);
    }

    public void FixedUpdate()
    {
      //  Vector2 dire = variableJoystick.Vertical * Vector2.up + variableJoystick.Horizontal * Vector2.right;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, collisionlayer);
        if(isGrounded)
        {
            rb.velocity = new Vector2(variableJoystick.Horizontal * speed, rb.velocity.y);
 //           rb.AddForce(Vector2.right* variableJoystick.Horizontal * MovementSpeed, ForceMode2D.Impulse);
 //           rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        }
        else
        {
//            rb.AddForce(Vector2.right* variableJoystick.Horizontal * AirSpeed, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up* variableJoystick.Vertical * AirSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(variableJoystick.Horizontal * speed, rb.velocity.y);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        }
    }
    void JumpOnClick()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}