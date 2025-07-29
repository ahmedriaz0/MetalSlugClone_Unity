using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player = null;
    public static GameManager Instance { get; private set;}


    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
