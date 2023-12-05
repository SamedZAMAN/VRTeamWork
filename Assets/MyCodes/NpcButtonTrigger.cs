using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcButtonTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;

    private static NpcButtonTrigger instance;

    private void Awake()
    {
        instance = this;
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public static NpcButtonTrigger GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
            DialogueManager.GetInstance().ExitDialogueMode();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
