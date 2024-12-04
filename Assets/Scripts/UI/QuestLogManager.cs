using UnityEngine;
using TMPro;

public class QuestLogManager : MonoBehaviour
{
    public GameObject questMenu;
    public TextMeshProUGUI questText;

    private int questProgress = 0;

    void Start()
    {
        UpdateQuest("No active quests.");
    }

    public void UpdateQuest(string newObjective)
    {
        questText.text = newObjective;
    }

    public int GetQuestProgress()
    {
        return questProgress;
    }

    public void SetQuestProgress(int progress)
    {
        questProgress = progress;
        UpdateQuest($"Collect Items: {questProgress}/5"); 
    }

    public void LoadQuestProgress(int progress)
    {
        questProgress = progress;
        UpdateQuest($"Collect Items: {questProgress}/5");
    }
}
