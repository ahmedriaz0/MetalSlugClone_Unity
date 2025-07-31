using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speedMultiplier = 5f;
    public float jumpForce = 15f;
    public Rigidbody2D rb;
    public bool onGround;
    public float playerHealth = 100;
    public AudioSource src;
    public AudioClip running_audio;
    public GameObject torso = null;
    public GameObject legs = null;
    public GameObject fullbody = null;

    public enum PlayerDirection { left, right };
    public PlayerDirection curDirection = PlayerDirection.right;

    bool aimUp;
    bool aimDown;
    bool isIdle;
    public bool isDead;

    Vector2 movementInput;
    float speed;

    public enum AimDirection { side, up, down };
    public AimDirection aimDirection = AimDirection.side;

    void Start()
    {
        if (transform.localScale.x > 0)
            curDirection = PlayerDirection.right;
        else
            curDirection = PlayerDirection.left;
    }

    void Update()
    {
        isIdle = speed > 0;
        rb.linearVelocityX = movementInput.x * speedMultiplier;
        if(isDead)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;  
            torso.SetActive(false);
            legs.SetActive(false);
            fullbody.SetActive(true);
            rb.bodyType = RigidbodyType2D.Static;
        }
        if (legs.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("legs_running"))
        {
            if (!src.isPlaying)
            {
                src.clip = running_audio;
                src.loop = true;
                src.Play();
            }
        }
        else
        {
            if (src.isPlaying)
                src.Pause();
        }
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
        Debug.Log("Cur dir chagned to" + ((curDirection == PlayerDirection.left) ? "left" : "right"));

        aimUp = movementInput.y > 0.5f;
        aimDown = movementInput.y < -0.5f;

        if (aimUp)
            aimDirection = AimDirection.up;
        else if (aimDown)
            aimDirection = AimDirection.down;
        else aimDirection = AimDirection.side;

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
    public void takedamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        isDead = true;
    }
}
