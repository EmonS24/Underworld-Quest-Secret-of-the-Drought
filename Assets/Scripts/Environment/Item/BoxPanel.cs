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

    void Start()
    {
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange && !playerVar.isGrabbing)
        {
            interactionPrompt.SetActive(true);

        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
