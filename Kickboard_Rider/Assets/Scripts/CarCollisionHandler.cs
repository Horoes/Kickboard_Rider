using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is false");
                GameManager.Instance.uiManager.GameOver();
            }
            else if (player != null && player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is true");
                player.isHelmet = false;
            }
        }

        Destroy(gameObject);
    }

}
