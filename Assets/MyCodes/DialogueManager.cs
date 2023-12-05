using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    [SerializeField] private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private bool continueStory;

    public bool dialogueIsPlaying;

    private static DialogueManager instance;

    private void Awake()
    {
        continueStory = false;
        if (instance != null)
        {
            Debug.Log("Found more than one DÝalogue Manager");
        }
        instance = this;
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choices[index].GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }



    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (continueStory)
        {
            Debug.Log("continue story yapýldý");
            ContinueStory();
            continueStory = false;
        }
        
        
    }

        
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        DialogueTrigger.GetInstance().devamButOn();
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        DialogueTrigger.GetInstance().devamButOff();
        Debug.Log("dialog bitti");
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(true);
        }

        List<Choice> currentChoices = currentStory.currentChoices;                                                                                                       

        //verilen seçeneklerle uý'ýn sayýsý eþdeðer mi?
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("more choices were given than UI , put less choice or more button!!");
        }
        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for(int i = index; i< choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void firstChoice()
    {
        if (currentStory.canContinue)
        {
            Debug.Log("ilk secenek secildi");
            currentStory.ChooseChoiceIndex(1);
            ContinueStory();
        }
        ContinueStory();
    }

    public void StoryContinue()
    {
        continueStory = true;
    }
}