using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHouse : MonoBehaviour
{
    // ========= MOVEMENT =================
    public float speed = 5;
    Rigidbody2D rigidbody2d;
    Vector2 currentInput;
    Vector2 lookDirection;
    private Vector2 newPosition;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        newPosition = transform.position;
    }
   
    // Update is called once per frame
    void Update()
    {
        // ============== MOVEMENT ======================
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        currentInput = move;

        if (Input.GetMouseButton(0))
        {
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //Mobile movement
        if (Input.touchCount > 0)
        {
            newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }

        /*Aktivoi alla oleva else mobiiliin ja deaktivoi tietokone testauksessa
        else
        {
            animator.SetBool("isRunning", false);
        }*/

        // ======== INTERACT ==========
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, 1 << LayerMask.NameToLayer("box"));
            if (hit.collider != null)
            {
                Debug.Log("Box hit" + hit.collider.gameObject);
                //GameControllerHouse.instance.LoadPlatformer();
            }
        }
    }

    void FixedUpdate()
    {

       Vector2 position = rigidbody2d.position;
       position = position + currentInput * speed * Time.deltaTime;
       rigidbody2d.MovePosition(position);
    }
}
