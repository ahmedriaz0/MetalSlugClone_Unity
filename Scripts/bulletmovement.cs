using UnityEngine;

public class bulletmovement : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody2D rb;
    public float playerdamage = 25;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;

        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemyscript enemy = hitInfo.GetComponent<enemyscript>();
        if(enemy != null)
        {
            enemy.takedamage(playerdamage);
        }
        spawnedenemyscript spawnedenemy = hitInfo.GetComponent<spawnedenemyscript>();
        if (spawnedenemy != null)
        {
            spawnedenemy.takedamage(playerdamage);
        }
        Destroy(gameObject);    
    }

    // Update is called once per frame

}
