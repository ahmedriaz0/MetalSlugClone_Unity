using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void LateUpdate()
    {
       if(GameManager.Instance.player)
        {
            Vector3 position =  GameManager.Instance.player.transform.position;
            position.z = -10;
            transform.position = position;

        }

    }
}
