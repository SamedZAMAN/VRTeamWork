using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    //-------------Dialog yazý animasyonlarýyla alakalý olan kýsým ----------------
    //diyalog hýzý
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;



    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    private static DialogManager instance;//script tanýmlama

    private Story currentStory; // metin

    public bool dialogueIsPlaying; //dialog çalýþýyor mu

    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices; //seçenek butonlarý

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo; // scriptable objeyi koymak için 
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable; //Konuþma durdurmak için

    private DialogueAudioInfoSO currentAudioInfo;

    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;

    private AudioSource audioSource;

    private TextMeshProUGUI[] choicesText; //metini seçme
    //JPON bilgileri
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";



    private void Awake()
    {
        // Eðer baþka bir Dialogue Manager bulunuyorsa uyarý verir
        if (instance != null)
        {
            Debug.LogWarning("Birden fazla Dialogue Manager sahnede bulundu!");
        }
        instance = this; // Bu nesneyi þu anki örnek olarak ayarlar

        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        // Diyalog oynanmýyor olarak baþlatýr
        dialogueIsPlaying = false;
        // Diyalog panelini gizler
        dialoguePanel.SetActive(false);

        // choicesText dizisini baþlat
        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices)
        {
            // Her seçeneðin metnini alýr
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
        // Dialog oynanmýyorsa hemen geri dön
        if (!dialogueIsPlaying)
        {
            return;
        }
        // Konuþma dialoguna devam etmek için left el ile continue ye tuþuna basýldýmý kontrol ediliyor
        if (canContinueToNextLine && TriggerButL.GetInstance().TalkContinueTrigger || TriggerButR.GetInstance().TalkContinueTrigger)
        {
            ContinueStory();
            TriggerButL.GetInstance().TalkContinueTrigger = false;
            TriggerButR.GetInstance().TalkContinueTrigger = false;
        }

    }

    // Diyalog moduna girmek için kullanýlan metot
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        // Ink JSON dosyasýný kullanarak bir hikaye baþlatýr
        currentStory = new Story(inkJSON.text);
        // Diyalog oynanýyor olarak iþaretler
        dialogueIsPlaying = true;
        // Diyalog panelini görünür hale getirir ve ölçeklerini ayarlar
        dialoguePanel.SetActive(true);

        // Diyalogu devam ettirir
        ContinueStory();
    }


    // Diyalog modundan çýkýþ yapmak için kullanýlan metot
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        // Diyalog oynanmýyor olarak iþaretler
        dialogueIsPlaying = false;
        // Diyalog panelini gizler ve ölçeklerini sýfýrlar
        dialoguePanel.SetActive(false);
        // Diyalog metnini temizler
        dialogueText.text = "";

        SetCurrentAudioInfo(defaultAudioInfo.id);
    }

    //Dialogu 1 adým ilerlet
    private void ContinueStory()
    {
        // Metin devam ediyorsa adým ilerlet
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = currentStory.Continue();
            // nextLine boþ deðilse (null deðilse) 
            if (!string.IsNullOrEmpty(nextLine))
            {
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
            else // Bitmiþse diyalog
            {
                StartCoroutine(ExitDialogueMode());
            }
        }
        else // Devam edemiyorsa bitir
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //taglere ulaþýlan kýsým
    private void HandleTags(List<string> currentTags)
    {
        //her tagi alan
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':'); //kelimeleri alýr
            //yanlýþ taglerde
            if (splitTag.Length != 2)
            {
                Debug.LogError("tag could not be appared" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Tagleri yerleþtir
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
                    Debug.LogWarning("taglarin olduðu kýsýmda hata var");
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
        //seçenekleri göster
        DisplayChoices();
        canContinueToNextLine = true;
    }

    // Seçenekleri ekranda gösteren metot
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Seçenek sayýsý seçenek oyun nesnelerinin sayýsýndan fazlaysa bir hata iletisi verir
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Verilen seçenek sayýsý: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // Geriye kalan seçenekleri devre dýþý býrakýr
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

        // Eðer en az bir seçenek mevcutsa, devam butonunu gizle
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

    // Ýlk seçeneði otomatik olarak seçmeye yarayan metot
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    // Bir seçenek yapýldýðýnda çaðrýlan metot
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