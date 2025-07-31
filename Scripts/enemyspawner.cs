using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{

    public float spawnCooldown = 0.5f;
    public float spawnTimer = 0f;
    public GameObject spawnPrefab;
    public bool inrange = false;
    public Transform spawnposition;
    float spawnlimit = 10;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnlimit > 0)
        {
            if (inrange)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0f)
                {
                    Instantiate(spawnPrefab, transform.position, transform.rotation);
                    spawnlimit --;
                    spawnTimer = spawnCooldown;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "player")
        {
            inrange = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "player")
        {
            inrange = true;

        }
    }
    
}
