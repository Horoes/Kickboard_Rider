using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public AudioClip step;
    private bool isSoundPlay;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(step);
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.isStep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

            // 플레이어가 아직 다른 Step에 있지 않은 경우에만 isStep을 false로 설정
            Collider[] colliders = Physics.OverlapSphere(other.transform.position, 0.2f);
            bool isStillOnStep = false;
            foreach (var col in colliders)
            {
                if (col.gameObject.CompareTag("Step"))
                {
                    isStillOnStep = true;
                    break;
                }
            }

            if (!isStillOnStep)
            {
                player.isStep = false;
            }
        }
    }
}