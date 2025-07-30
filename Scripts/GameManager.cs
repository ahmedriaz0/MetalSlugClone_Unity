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
}
