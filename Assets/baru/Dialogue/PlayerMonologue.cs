using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonologuePlayer : MonoBehaviour
{
    public GameObject monologueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
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
        float distance = Vector3.Distance(transform.position, player.position);

        // Trigger monologue if player is in range
        if (distance <= triggerRange && !isMonologueActive && !hasTriggered && player.CompareTag("Player"))
        {
            StartMonologue();
        }
    }

    private void StartMonologue()
    {
        hasTriggered = true;
        isMonologueActive = true;
        currentMonologueIndex = 0;
        monologueUI.SetActive(true);
        nextButton.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        UpdateMonologueUI();
    }

    public void ShowNextMonologue()
    {
        if (currentMonologueIndex < monologues.Count - 1)
        {
            currentMonologueIndex++;
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

        if (currentMonologue.isLeftSpeaker)
        {
            leftCharacterImage.sprite = currentMonologue.characterSprite;
            leftCharacterImage.gameObject.SetActive(true);
            rightCharacterImage.gameObject.SetActive(false);
        }
        else
        {
            rightCharacterImage.sprite = currentMonologue.characterSprite;
            rightCharacterImage.gameObject.SetActive(true);
            leftCharacterImage.gameObject.SetActive(false);
        }
    }

    private void EndMonologue()
    {
        isMonologueActive = false;
        monologueUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
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
