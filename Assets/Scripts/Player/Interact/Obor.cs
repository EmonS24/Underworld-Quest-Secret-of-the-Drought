using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Obor : MonoBehaviour
{
    private PlayerVar player;

    void Start()
    {
        player = player = GetComponent<PlayerVar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            Light2D checkpointLight = collision.GetComponentInChildren<Light2D>();
            if (checkpointLight != null)
                StartCoroutine(FadeInLight(checkpointLight));
        }
    }

    private IEnumerator FadeInLight(Light2D light)
    {
        float timeElapsed = 0f;
        float fadeDuration = 1f;
        float startIntensity = 0f;
        float targetIntensity = light.intensity;

        light.intensity = startIntensity;
        light.enabled = true;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, timeElapsed / fadeDuration);
            yield return null;
        }

        light.intensity = targetIntensity;
    }
}