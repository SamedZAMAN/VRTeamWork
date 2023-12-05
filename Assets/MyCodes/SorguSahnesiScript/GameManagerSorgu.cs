using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSorgu : MonoBehaviour
{
    [Header("Story Panel")]
    [SerializeField] public GameObject talkPanel; // sTORY PANELÝ
    [SerializeField] public GameObject startStory; // Týkladýðýnda storiyi baþlatan nesne
    [SerializeField] public TextAsset inkJSON; // inkJSON 

    [Header("Dialog")]
    public GameObject DialogueCanvas; // Dialog canvasý
    public GameObject DialogueAnchor; // dialog için anchor

    private static GameManagerSorgu instance;

    private void Awake()
    {
        //Ýnstane yapma
        if (instance != null)
        {
            Debug.Log("Found more than one TriggerBut");
        }
        instance = this;
    }

    //Ýnstance
    public static GameManagerSorgu GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (DialogManager.GetInstance().dialogueIsPlaying) //konuþma baþladýðýnda açýldýðýnda
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
