using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firepoint;
    public GameObject bullet;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bullet,firepoint.position , firepoint.rotation);
    }
}
