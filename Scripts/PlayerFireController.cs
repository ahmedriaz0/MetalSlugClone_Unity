using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireController : MonoBehaviour
{
    public GameObject bullet = null;
    public float firerate = 0.2f;   
    public float sideOffset;
    public float sideVerticalOffset;
    public float upOffset;
    public float horizontalOffset;
    public float downOffset;
    public AudioSource src;
    public AudioClip gunshot_mp3;
    bool isFiring = false;
    float fireCooldown = 0f;


    GameObject torso = null;
    PlayerController playerController = null;

    void Start()
    {
        torso = GetComponent<PlayerController>().torso;
        playerController = GetComponent<PlayerController>();    
    }

    public void Update()
    {
        if(isFiring)
        {
            fireCooldown -= Time.deltaTime;     
            if(fireCooldown <= 0f)
            {
                FireBullet();
                fireCooldown = firerate;
            }
        }
    }

    void FireBullet()
    {
        Vector3 firepoint = transform.position;
        Quaternion bulletRotation = Quaternion.identity;

        switch (playerController.aimDirection)
        {
            case PlayerController.AimDirection.side:
                switch (playerController.curDirection)
                {
                    case PlayerController.PlayerDirection.right:
                        Debug.Log("Firing to right");
                        firepoint += new Vector3(sideOffset, sideVerticalOffset, 0);
                        break;
                    case PlayerController.PlayerDirection.left:
                        firepoint += new Vector3(sideOffset * -1, sideVerticalOffset, 0);
                        bulletRotation = Quaternion.Euler(0, 180f, 0);
                        Debug.Log("Firing to left");
                        break;
                    default:
                        Debug.Log("invalid playerDirection");
                        break;
                }
                break;
            case PlayerController.AimDirection.up:
                if (playerController.curDirection == PlayerController.PlayerDirection.right)
                {
                    firepoint += new Vector3(horizontalOffset - 0.12f, upOffset, 0);
                }
                else
                    firepoint += new Vector3(horizontalOffset, upOffset, 0);
                bulletRotation = Quaternion.Euler(0, 0, 90);
                Debug.Log("Firing up");
                break;
            case PlayerController.AimDirection.down:
                firepoint += new Vector3(horizontalOffset, downOffset * -1, 0);
                bulletRotation = Quaternion.Euler(0, 0, -90);
                Debug.Log("Firing down");
                break;
            
        }
        gunshot();
        Instantiate(bullet, firepoint, bulletRotation);
    }

    void gunshot()
    {
        src.PlayOneShot(gunshot_mp3);
    }
    public void Fire(InputAction.CallbackContext context)
    {
        isFiring = context.performed;
        torso.GetComponent<Animator>().SetBool("fireHeld", context.performed);
    }
}
