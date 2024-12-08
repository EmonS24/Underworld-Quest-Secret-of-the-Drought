using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMonologue : MonoBehaviour
{
    public GameObject monologueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image characterImage;
    public Image leftCharacterImage;
    public List<Monologue> monologues;
    public float triggerRange = 3f;
    public Transform player;
    public Button nextButton;

    private int currentMonologueIndex = 0;
    private bool isMonologueActive = false;
    private bool hasTriggered = false;

    private void Start()
    {
        monologueUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(ShowNextMonologue);
    }

    private void Update()
    {
        if (hasTriggered || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerRange && !isMonologueActive && player.CompareTag("Player"))
        {
            StartMonologue();
        }
    }

    private void StartMonologue()
    {
        if (hasTriggered) return;

        hasTriggered = true;
        isMonologueActive = true;
        currentMonologueIndex = 0;
        monologueUI.SetActive(true);
        nextButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
        UpdateMonologueUI();
    }

    public void ShowNextMonologue()
    {
        currentMonologueIndex++;
        if (currentMonologueIndex < monologues.Count)
        {
            UpdateMonologueUI();
        }
        else
        {
            EndMonologue();
        }
    }

    private void UpdateMonologueUI()
    {
        var currentMonologue = monologues[currentMonologueIndex];
        nameText.text = currentMonologue.speakerName;
        dialogueText.text = currentMonologue.text;
        characterImage.sprite = currentMonologue.characterSprite;

        if (monologues[currentMonologueIndex].isLeftSpeaker)
        {
            leftCharacterImage.sprite = monologues[currentMonologueIndex].characterSprite;
            leftCharacterImage.gameObject.SetActive(true);
            characterImage.gameObject.SetActive(false);
        }
        else
        {
            characterImage.sprite = monologues[currentMonologueIndex].characterSprite;
            characterImage.gameObject.SetActive(true);
            leftCharacterImage.gameObject.SetActive(false);
        }
    }

    private void EndMonologue()
    {
        nextButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
        CloseMonologue();
    }

    public void CloseMonologue()
    {
        monologueUI.SetActive(false);
        nextButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        characterImage.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}

[System.Serializable]
public class Monologue
{
    public string speakerName;
    public string text;
    public Sprite characterSprite;
    public bool isLeftSpeaker;
}
