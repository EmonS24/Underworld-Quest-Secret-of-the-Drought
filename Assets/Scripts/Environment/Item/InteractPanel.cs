using System.Collections;
using UnityEngine;

public class InteractPanel : MonoBehaviour
{
    public GameObject interactionPrompt;
    public float interactionRange = 3f;
    public Transform player;
    public float fadeDuration = 0.1f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    void Start()
    {
        canvasGroup = interactionPrompt.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = interactionPrompt.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; 
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            if (fadeCoroutine == null && canvasGroup.alpha < 1)
            {
                interactionPrompt.SetActive(true); 
                fadeCoroutine = StartCoroutine(FadePrompt(1));
            }
        }
        else
        {
            if (fadeCoroutine == null && canvasGroup.alpha > 0)
            {
                fadeCoroutine = StartCoroutine(FadePrompt(0)); 
            }
        }
    }

    private IEnumerator FadePrompt(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0)
        {
            interactionPrompt.SetActive(false); 
        }

        fadeCoroutine = null; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
