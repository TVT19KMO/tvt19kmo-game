using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial used for movement/jumping https://www.youtube.com/watch?v=j111eKN8sJw

public class PlayerPlatformer : MonoBehaviour
{
    public float speed;
    private float moveInput;
    public float jumpForce;

    private float jumptimeCounter;
    public float jumptime;
    private bool isJumping;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask ground;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumptimeCounter = jumptime;
            rb2d.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumptimeCounter > 0) 
            {
                rb2d.velocity = Vector2.up * jumpForce;
                jumptimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
}
