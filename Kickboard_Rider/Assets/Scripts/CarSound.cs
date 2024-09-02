using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    public AudioClip hornSound; 
    public AudioClip passingCarSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¤±¤±");
            bool hornSoundTriggered = Random.Range(0f, 1f) <= 0.1f; 
            bool passingCarSoundTriggered = Random.Range(0f, 1f) <= 0.3f;
            Debug.Log(hornSoundTriggered);
            Debug.Log(passingCarSoundTriggered);
            if (hornSoundTriggered && passingCarSoundTriggered)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    PlaySound(hornSound);
                }
                else
                {
                    PlaySound(passingCarSound);
                }
            }
            else if (hornSoundTriggered)
            {
                PlaySound(hornSound);
            }
            else if (passingCarSoundTriggered)
            {
                PlaySound(passingCarSound);
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
