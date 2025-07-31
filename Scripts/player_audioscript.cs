using UnityEngine;

public class player_audioscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource src;
    public AudioClip gunshot;

    public void fire()
    {
        src.clip = gunshot;
        src.Play();
    }
}
