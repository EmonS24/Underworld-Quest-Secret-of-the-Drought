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
    public GameObject interactionPrompt;
    public Monologue[] monologues;

    private int currentMonologueIndex = 0;
    private bool isPlayerInRange = false;
    private bool isMonologueActive = false;

    void Start()
    {
        monologueUI.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    void StartMonologue()
    {
        isMonologueActive = true;
        currentMonologueIndex = 0;
        monologueUI.SetActive(true);
        interactionPrompt.SetActive(false);
        UpdateMonologueUI();
    }

    void ShowNextMonologue()
    {
        currentMonologueIndex++;
        if (currentMonologueIndex < monologues.Length)
        {
            UpdateMonologueUI();
        }
        else
        {
            EndMonologue();
        }
    }

    void UpdateMonologueUI()
    {
        nameText.text = monologues[currentMonologueIndex].speakerName;
        dialogueText.text = monologues[currentMonologueIndex].text;
        characterImage.sprite = monologues[currentMonologueIndex].characterSprite;
    }

    void EndMonologue()
    {
        isMonologueActive = false;
        monologueUI.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isMonologueActive)
            {
                StartMonologue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}

[System.Serializable]
public class Monologue
{
    public string speakerName;
    public string text;
    public Sprite characterSprite;
}