using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxPanel : MonoBehaviour
{
    public GameObject interactionPrompt;
    [SerializeField] private PlayerVar playerVar;

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
        canvasGroup.alpha = 0f;
        interactionPrompt.SetActive(true);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange && !playerVar.isGrabbing)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f));
    }

    private void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }

        cg.alpha = endAlpha;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
