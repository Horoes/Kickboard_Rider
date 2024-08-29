using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    public AudioSource carSound;
    private float timer = 0f;
    public float soundInterval = 1f;
    public float soundProbability = 0.2f;
    private static int playingSoundCount = 0;
    public int maxSimultaneousSounds = 2;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= soundInterval && playingSoundCount < maxSimultaneousSounds)
        {
            timer = 0f;
            if (Random.value <= soundProbability)
            {
                carSound.Play();
                playingSoundCount++;
                StartCoroutine(ResetPlayingSoundCount());
            }
        }
    }

    private IEnumerator ResetPlayingSoundCount()
    {
        yield return new WaitForSeconds(carSound.clip.length);
        playingSoundCount--;
    }
}
