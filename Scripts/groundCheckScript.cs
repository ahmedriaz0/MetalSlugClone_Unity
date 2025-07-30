using System.Diagnostics;
using UnityEngine;

public class groundCheckScript : MonoBehaviour
{
    int contactCount = 0;
    Animator animator;
    bool onGround;

    private void Start()
    {
       animator  = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "enemy")
        {
            GameManager.Instance.player.GetComponent<PlayerController>().onGround = true;
            contactCount++;
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        contactCount--;
        if(contactCount <= 0)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().onGround = false;
            animator.SetBool("isGrounded", false);
        }
    }

}
