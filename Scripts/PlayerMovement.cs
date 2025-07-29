using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speedMultiplier = 5f;
    public float runSpeedThreshold = 0.01f;
    public float jumpForce = 5f;

    Rigidbody2D rb;
    Animator animator;
    bool onGround = true;

    enum playerDirection { left, right};
    playerDirection curDirection = playerDirection.right;
    Vector2 movementInput;

    void Awake()
    {
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float speed = movementInput.magnitude;
        animator.SetFloat("xVelocity", movementInput.magnitude);
        rb.linearVelocityX = movementInput.x * speedMultiplier;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        curDirection = (movementInput.x >= 0)? playerDirection.right : playerDirection.left;

        switch(curDirection)
        {
            case playerDirection.right:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case playerDirection.left:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && onGround)
        {
            animator.SetBool("isJumping", true);
            rb.linearVelocityY = jumpForce;
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            animator.SetBool("isJumping", false);
            onGround = true;
        }
    }



}
