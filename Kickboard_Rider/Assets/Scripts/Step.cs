using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
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
            player.isStep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

            // �÷��̾ ���� �ٸ� Step�� ���� ���� ��쿡�� isStep�� false�� ����
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