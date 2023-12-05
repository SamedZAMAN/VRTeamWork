using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSorgu : MonoBehaviour
{
    [Header("Story Panel")]
    [SerializeField] public GameObject talkPanel; // sTORY PANEL�
    [SerializeField] public GameObject startStory; // T�klad���nda storiyi ba�latan nesne
    [SerializeField] public TextAsset inkJSON; // inkJSON 

    [Header("Dialog")]
    public GameObject DialogueCanvas; // Dialog canvas�
    public GameObject DialogueAnchor; // dialog i�in anchor

    private static GameManagerSorgu instance;

    private void Awake()
    {
        //�nstane yapma
        if (instance != null)
        {
            Debug.Log("Found more than one TriggerBut");
        }
        instance = this;
    }

    //�nstance
    public static GameManagerSorgu GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (DialogManager.GetInstance().dialogueIsPlaying) //konu�ma ba�lad���nda a��ld���nda
        {
            DialogueCanvas.transform.position = DialogueAnchor.transform.position;
            DialogueCanvas.transform.eulerAngles = new Vector3(DialogueAnchor.transform.eulerAngles.x + 15, DialogueAnchor.transform.eulerAngles.y, 0);
        }
    }

    public void TalkPanelOff()
    {
        talkPanel.SetActive(false);
    }
    public void TalkPanelOn()
    {
        talkPanel.SetActive(true);
    }

    public void startStoryOn()
    {
        startStory.SetActive(true);
    }
    public void startStoryOff()
    {
        startStory.SetActive(false);
    }
}
