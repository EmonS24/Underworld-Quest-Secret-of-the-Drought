using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IntroLine
{
    public string characterName;
    public string dialogueText;
    public Sprite characterImage;
}

public class IntroDialog : MonoBehaviour
{
    public GameObject introPanel;
    public TMPro.TextMeshProUGUI characterNameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public Image characterImage;
    public List<IntroLine> introLines;

    private void Start()
    {
        StartIntroDialog();
    }

    public void StartIntroDialog()
    {
        Time.timeScale = 0;
        introPanel.SetActive(true);
        StartCoroutine(PlayIntroDialog());
    }

    private IEnumerator PlayIntroDialog()
    {
        foreach (var line in introLines)
        {
            characterNameText.text = line.characterName;
            dialogueText.text = line.dialogueText;
            characterImage.sprite = line.characterImage;
            yield return new WaitForSecondsRealtime(3f);
        }

        EndIntroDialog();
    }

    public void EndIntroDialog()
    {
        introPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
