using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public AudioClip step;
    private AudioSource audioSource;
    private Renderer stepRenderer;
    public float fadeDuration = 2f;
    public float respawnDelay = 1f;
    private Color originalColor;
    public GameObject stepPrefab;
    private bool isFading = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        stepRenderer = GetComponent<Renderer>();
        originalColor = stepRenderer.material.color;

        stepRenderer.material.SetFloat("_Mode", 2);
        stepRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        stepRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        stepRenderer.material.SetInt("_ZWrite", 0);
        stepRenderer.material.DisableKeyword("_ALPHATEST_ON");
        stepRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        stepRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        stepRenderer.material.renderQueue = 3000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isFading)
        {
            audioSource.PlayOneShot(step);
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.isStep = true;

            StartCoroutine(FadeOutAndDeactivate(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
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

    private IEnumerator FadeOutAndDeactivate(PlayerMovement player)
    {
        float elapsedTime = 0f;
        isFading = true;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            stepRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        if (player != null)
        {
            player.isStep = false;
        }

        yield return new WaitForSeconds(respawnDelay);
        RespawnStep();

        isFading = false;
    }

    private void RespawnStep()
    {
        stepRenderer.material.color = originalColor;
    }
}
