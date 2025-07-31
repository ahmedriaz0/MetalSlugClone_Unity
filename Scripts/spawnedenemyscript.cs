using UnityEngine;
using UnityEngine.UIElements;

public class spawnedenemyscript : MonoBehaviour
{
    public float health = 100;

    public Animator animator;
    public GameObject player;
    private Transform currentpt;
    public float speed;
    private Rigidbody2D rb;
    private bool isalive = true;
    bool facingright = true;
    private bool playerdetected = false;
    public float shootCooldown = 1f;
    public float shootTimer = 0f;
    public Transform firepoint;
    public GameObject bullet;
    public bool isfiring = false;
    public AudioSource src;
    public AudioClip gunshot_mp3;
    bool goidle=false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        animator.SetBool("isrunning", true);
    }

    public void shoot()
    {
        gunshot();
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }
    void gunshot()
    {
        src.PlayOneShot(gunshot_mp3);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {


    }
    void Update()
    {
        if (isfiring)
        {
            animator.SetBool("isfiring", true);
        }
        else
        {
            animator.SetBool("isfiring", false);
        }
        if (isalive && !playerdetected)
        {
            faceplayer();
            isfiring = false;
        }
        if (playerdetected && isalive)
        {


            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                isfiring = true;
                shoot();
                shootTimer = shootCooldown;
            }

        }

    }

    
    void translateleft() 
    { 
        rb.linearVelocity = new Vector2(-speed, 0);
    }

    public void takedamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isfiring = false;
            isalive = false;
            animator.SetBool("isrunning", false);
            die();
        }
    }
    void flip()
    {
        if (facingright)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingright = false;
        }
        else if (!facingright)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingright = true;
        }
        //Vector3 localscale = transform.localScale;
        //localscale.x *= -1;
        //transform.localScale = localscale;
    }
    void die()
    {
        animator.SetBool("enemydie", true);
        setdead();
        GetComponent<CircleCollider2D>().enabled = true;

    }

    void setdead()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        animator.SetBool("dead", true);
    }

    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        player = hitinfo.gameObject;
        if (hitinfo.CompareTag("player"))
        {
            playerdetected = true;
            rb.linearVelocity = new Vector2(0, 0);
            animator.SetBool("isrunning", false);
            player = hitinfo.gameObject;

            faceplayer();

        }
    }
    void faceplayer()
    {
        if (player == null) return;

        float direction = player.transform.position.x - transform.position.x;

        if (direction < 0)
        {
            if (facingright) flip();
            translateleft();
        }
        else if (direction > 0)
        {
            enemyspawner es = GetComponent<enemyspawner>();
            es.inrange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D hitinfo)
    {

        if (hitinfo.CompareTag("player"))
        {
            playerdetected = false;
            animator.SetBool("isrunning", true);
            
        }
    }

    


}
