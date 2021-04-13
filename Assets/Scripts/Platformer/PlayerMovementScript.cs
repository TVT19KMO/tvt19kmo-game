using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public VariableJoystick variableJoystick;
    public Button jumpButton;

    public float speed;
    public float jumpForce;
    public float MovementSpeed;
    public float MaxSpeed;
    public float AirSpeed;

    public float jumptime;
    private bool isJumping;
    public float checkRadius;
    private bool isGrounded;
    private float jumptimeCounter;

    public Transform feetPos;
    public LayerMask collisionLayer;

    private bool buttonDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //jumpButton.onClick.AddListener(JumpOnClick);
    }

    void Update()
    {
        if (GameControllerPlatformer.instance.gamePaused == false)
        {
            if (isGrounded == true && buttonDown)
            {
                isJumping = true;
                jumptimeCounter = jumptime;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (buttonDown && isJumping == true)
            {
                if (jumptimeCounter > 0)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    jumptimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (!buttonDown)
            {
                isJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (GameControllerPlatformer.instance.gamePaused == false)
        {
            //  Vector2 dire = variableJoystick.Vertical * Vector2.up + variableJoystick.Horizontal * Vector2.right;
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, collisionLayer);
            if (isGrounded)
            {
                rb.velocity = new Vector2(variableJoystick.Horizontal * speed, rb.velocity.y);
                //           rb.AddForce(Vector2.right* variableJoystick.Horizontal * MovementSpeed, ForceMode2D.Impulse);
                //           rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
            }
            else
            {
                //            rb.AddForce(Vector2.right* variableJoystick.Horizontal * AirSpeed, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * variableJoystick.Vertical * AirSpeed, ForceMode2D.Impulse);
                rb.velocity = new Vector2(variableJoystick.Horizontal * speed, rb.velocity.y);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
            }
        }
    }

    public void JumpOnClick()
    {
        if (isGrounded)
        {
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnPointerDown()
    {
        buttonDown = true;
    }

    public void OnPointerUp()
    {
        buttonDown = false;
    }
}