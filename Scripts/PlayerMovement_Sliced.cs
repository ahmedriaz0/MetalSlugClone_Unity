  using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Sliced : MonoBehaviour
{
    public float speedMultiplier = 5f;
    public float jumpForce = 15f;
    public Rigidbody2D rb;
    public bool onGround;

    [SerializeField] GameObject fullbody = null;
    [SerializeField] GameObject torso = null;
    [SerializeField] GameObject legs = null;

    enum PlayerDirection { left, right };
    PlayerDirection curDirection;

    Vector2 movementInput;
    float speed;
    bool isIdle;

    enum aimDirection { side, up, down };

    void Update()
    {
        isIdle = speed > 0;
        rb.linearVelocityX = movementInput.x * speedMultiplier;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        speed = Mathf.Abs(movementInput.x);

        if (movementInput.x > 0)
            curDirection = PlayerDirection.right;
        if(movementInput.x < 0)
            curDirection = PlayerDirection.left;

        switch(curDirection)
        {
            case PlayerDirection.right:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case PlayerDirection.left:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }


        bool aimUp = movementInput.y > 0.5f;
        bool aimDown = movementInput.y < -0.5f;

        legs.GetComponent<Animator>().SetFloat("speed", speed);
        legs.GetComponent<Animator>().SetBool("aiming", aimUp || aimDown);

        torso.GetComponent<Animator>().SetBool("aimUp", aimUp);
        torso.GetComponent<Animator>().SetBool("aimDown", aimDown);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && onGround)
        {
            rb.linearVelocityY = jumpForce;
            onGround = false;
        }
    }
}
