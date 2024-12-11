using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueNPC : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
    public Button nextButton;
    public GameObject interactionPrompt;

    public Dialogue[] dialogues;
    private int currentDialogueIndex = 0;

    public float interactionRange = 3f;
    public Transform player;

    private bool isPlayerInRange = false;

    void Start()
    {
        nextButton.onClick.AddListener(ShowNextDialogue);
        nextButton.gameObject.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            isPlayerInRange = true;
            if (!dialogueUI.activeInHierarchy)
            {
                interactionPrompt.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !dialogueUI.activeInHierarchy)
            {
                StartDialogue();
            }
        }
        else
        {
            isPlayerInRange = false;
            interactionPrompt.SetActive(false);
            dialogueUI.SetActive(false);
        }
    }

    void StartDialogue()
    {
        currentDialogueIndex = 0;
        dialogueUI.SetActive(true);
        nextButton.gameObject.SetActive(true);
        interactionPrompt.SetActive(false);
        UpdateDialogueUI();
    }

    void ShowNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            UpdateDialogueUI();
        }
        else
        {
            EndDialogue();
        }
    }

    void UpdateDialogueUI()
    {
        nameText.text = dialogues[currentDialogueIndex].speakerName;
        dialogueText.text = dialogues[currentDialogueIndex].text;

        if (dialogues[currentDialogueIndex].isLeftSpeaker)
        {
            leftCharacterImage.sprite = dialogues[currentDialogueIndex].characterSprite;
            leftCharacterImage.gameObject.SetActive(true);
            rightCharacterImage.gameObject.SetActive(false);
        }
        else
        {
            rightCharacterImage.sprite = dialogues[currentDialogueIndex].characterSprite;
            rightCharacterImage.gameObject.SetActive(true);
            leftCharacterImage.gameObject.SetActive(false);
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        if (isPlayerInRange)
        {
            interactionPrompt.SetActive(true);
        }
        ResetDialogueState();
    }

    void ResetDialogueState()
    {
        nextButton.gameObject.SetActive(true);
        currentDialogueIndex = 0;
        leftCharacterImage.gameObject.SetActive(false);
        rightCharacterImage.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    public string text;
    public Sprite characterSprite;
    public bool isLeftSpeaker;
}