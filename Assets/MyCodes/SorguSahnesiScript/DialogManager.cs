using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    //-------------Dialog yaz� animasyonlar�yla alakal� olan k�s�m ----------------
    //diyalog h�z�
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;



    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    private static DialogManager instance;//script tan�mlama

    private Story currentStory; // metin

    public bool dialogueIsPlaying; //dialog �al���yor mu

    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices; //se�enek butonlar�

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo; // scriptable objeyi koymak i�in 
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable; //Konu�ma durdurmak i�in

    private DialogueAudioInfoSO currentAudioInfo;

    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;

    private AudioSource audioSource;

    private TextMeshProUGUI[] choicesText; //metini se�me
    //JPON bilgileri
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";



    private void Awake()
    {
        // E�er ba�ka bir Dialogue Manager bulunuyorsa uyar� verir
        if (instance != null)
        {
            Debug.LogWarning("Birden fazla Dialogue Manager sahnede bulundu!");
        }
        instance = this; // Bu nesneyi �u anki �rnek olarak ayarlar

        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        // Diyalog oynanm�yor olarak ba�lat�r
        dialogueIsPlaying = false;
        // Diyalog panelini gizler
        dialoguePanel.SetActive(false);

        // choicesText dizisini ba�lat
        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices)
        {
            // Her se�ene�in metnini al�r
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        InitializeAudioInfoDictionary();
    }

    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null; audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.LogWarning("Failed to find audio info for id:" + id);
        }
    }

    private void Update()
    {
        // Dialog oynanm�yorsa hemen geri d�n
        if (!dialogueIsPlaying)
        {
            return;
        }
        // Konu�ma dialoguna devam etmek i�in left el ile continue ye tu�una bas�ld�m� kontrol ediliyor
        if (canContinueToNextLine && TriggerButL.GetInstance().TalkContinueTrigger || TriggerButR.GetInstance().TalkContinueTrigger)
        {
            ContinueStory();
            TriggerButL.GetInstance().TalkContinueTrigger = false;
            TriggerButR.GetInstance().TalkContinueTrigger = false;
        }

    }

    // Diyalog moduna girmek i�in kullan�lan metot
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        // Ink JSON dosyas�n� kullanarak bir hikaye ba�lat�r
        currentStory = new Story(inkJSON.text);
        // Diyalog oynan�yor olarak i�aretler
        dialogueIsPlaying = true;
        // Diyalog panelini g�r�n�r hale getirir ve �l�eklerini ayarlar
        dialoguePanel.SetActive(true);

        // Diyalogu devam ettirir
        ContinueStory();
    }


    // Diyalog modundan ��k�� yapmak i�in kullan�lan metot
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        // Diyalog oynanm�yor olarak i�aretler
        dialogueIsPlaying = false;
        // Diyalog panelini gizler ve �l�eklerini s�f�rlar
        dialoguePanel.SetActive(false);
        // Diyalog metnini temizler
        dialogueText.text = "";

        SetCurrentAudioInfo(defaultAudioInfo.id);
    }

    //Dialogu 1 ad�m ilerlet
    private void ContinueStory()
    {
        // Metin devam ediyorsa ad�m ilerlet
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = currentStory.Continue();
            // nextLine bo� de�ilse (null de�ilse) 
            if (!string.IsNullOrEmpty(nextLine))
            {
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
            else // Bitmi�se diyalog
            {
                StartCoroutine(ExitDialogueMode());
            }
        }
        else // Devam edemiyorsa bitir
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //taglere ula��lan k�s�m
    private void HandleTags(List<string> currentTags)
    {
        //her tagi alan
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':'); //kelimeleri al�r
            //yanl�� taglerde
            if (splitTag.Length != 2)
            {
                Debug.LogError("tag could not be appared" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Tagleri yerle�tir
            switch (tagKey)
            {
                /*case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("tag came in but is  not currently being handly: " + tag);
                    break; */
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;
                default:
                    Debug.LogWarning("taglarin oldu�u k�s�mda hata var");
                    break;
            }
        }
    }


    private IEnumerator DisplayLine(string line)
    {

        // play audio for the entire line
        PlayDialogueSound(line);
        // set the text to the full Line, but set the visible characters to e
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        //hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each Letter one at a time 
        foreach (char letter in line.ToCharArray())
        {
            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        //after test is typing , display continue button
        continueIcon.SetActive(true);
        //se�enekleri g�ster
        DisplayChoices();
        canContinueToNextLine = true;
    }

    // Se�enekleri ekranda g�steren metot
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Se�enek say�s� se�enek oyun nesnelerinin say�s�ndan fazlaysa bir hata iletisi verir
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Verilen se�enek say�s�: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // Geriye kalan se�enekleri devre d��� b�rak�r
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

        // E�er en az bir se�enek mevcutsa, devam butonunu gizle
        if (currentChoices.Count > 0)
        {
            TriggerButL.GetInstance().ContinueTalkButton.SetActive(false);
        }
        else if (currentChoices.Count <= 0)
        {
            TriggerButL.GetInstance().ContinueTalkButton.SetActive(true);
        }
    }


    //sesi oynatma
    private void PlayDialogueSound(string currentLine)
    {
        // Stop any ongoing audio playback
        audioSource.Stop();

        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;

        // Choose the audio clip for the entire line (assuming it's the first clip in the array)
        AudioClip fullLineAudioClip = dialogueTypingSoundClips[0];
        audioSource.clip = fullLineAudioClip;

        // Play the selected audio clip
        audioSource.Play();
    }


    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    // �lk se�ene�i otomatik olarak se�meye yarayan metot
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    // Bir se�enek yap�ld���nda �a�r�lan metot
    public void MakeChoice(int choiceIndex)
    {
        Debug.Log("Current Number of Choices: " + currentStory.currentChoices.Count);

        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }

    }
}