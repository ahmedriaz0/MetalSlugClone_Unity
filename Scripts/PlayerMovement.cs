using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D player;
    public Animator animator;
    public float jumpheight = 10;
    private bool onground = true;
    private float movement;
    
    public float movespeed = 5;
    private bool facingright = true;
    public float playerhealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if(movement < 0 && facingright)
        {
            transform.eulerAngles = new Vector3(0 ,-180 ,0);
            facingright = false;

        }
        else if(movement > 0 && facingright ==false )
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingright = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onground)
        {
            jump();
            onground = false;   
        }
        if (Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("run", 1);
        }
        else if (movement < 0.1f)
        {
            animator.SetFloat("run", 0);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0, 0) * Time.fixedDeltaTime * movespeed; 
    }

    void jump()
    {
        animator.SetBool("jump" , true);
        player.AddForce(new Vector2(0, jumpheight) , ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            onground = true; 
            animator.SetBool("jump", false); 
        }
    }

    public void takedamage(float damage)
    {
        playerhealth -= damage;
        if (playerhealth <= 0)
        {
            
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
