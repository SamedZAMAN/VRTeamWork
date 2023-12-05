using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerButR : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konuþmaya baþlama butonu sorgulama
    public bool TalkContinueTrigger; //Konuþmaya devam etme butonu sorgulama
    public int numberOfChoice; //secenek sayýlarý
    private List<Color> originalColors = new List<Color>(); // Orijinal renkleri saklayacak liste

    private bool wasGrabbingRight = false; // El içeriye sokulduðunda grabbing býrakýldýðýný kontrol etmek için

    private static TriggerButR instance;


    private void Awake()
    {
        //Konuþmaya baþlama butonu kontrolu
        TalkButtonTrigger = false;

        //Konuþmaya devam trigger
        TalkContinueTrigger = false;
        //Ýnstane yapma
        if (instance != null)
        {
            Debug.Log("Found more than one TriggerBut");
        }
        instance = this;
    }
    //Ýnstance
    public static TriggerButR GetInstance()
    {
        return instance;
    }

    //Konuþma butonunu false yapma
    public void TalkButtonOff()
    {
        TalkButtonTrigger = false;
    }

    //Giriþ yapýldýðýnda
    private void OnTriggerEnter(Collider other)
    {
        //right hand tutuþu aktif olduðunda
        if (HandManager.IsGrabbingRight)
        {
            Debug.Log("item tag: " + other.tag);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
            Debug.Log("wasGrabbing : true");
            //Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk") && !TalkButtonTrigger)
            {
                buttonColorChange(other);
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
            else if (other.CompareTag("conTalk") && !TalkContinueTrigger)
            {
                buttonColorChange(other);
            }
            //ilk secenek seçildiðinde
            else if (other.CompareTag("firstChoice"))
            {
                buttonColorChange(other);
            }
            //ilk secenekler secildiðinde
            else if (other.CompareTag("secondChoice"))
            {
                buttonColorChange(other);
            }
            //ilk secenekler secildiðinde
            else if (other.CompareTag("thirdChoice"))
            {
                buttonColorChange(other);
            }
        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        //objeye elimi soktuumda ve içerisinde grabbing i býraktýðýmda iþlem gerçekleþsin
        if (!HandManager.IsGrabbingRight && wasGrabbingRight)
        {
            Debug.Log("elimi içerisi soktum ve grabý býraktým");
            ////Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                DialogManager.GetInstance().EnterDialogueMode(GameManagerSorgu.GetInstance().inkJSON); //ÝnkJSON ile konuþmayý baþlat
                TalkButtonTrigger = true;
                GameManagerSorgu.GetInstance().TalkPanelOn();
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                Debug.Log("konuþmaya devam edilmesi lazým");
                buttonChange(other);
                TalkContinueTrigger = true;
            }
            else if (other.CompareTag("firstChoice"))
            {
                buttonChange(other);
                DialogManager.GetInstance().MakeChoice(0);
            }
            else if (other.CompareTag("secondChoice"))
            {
                buttonChange(other);
                DialogManager.GetInstance().MakeChoice(1);
            }
            else if (other.CompareTag("thirdChoice"))
            {
                
                DialogManager.GetInstance().MakeChoice(2);
            }
            wasGrabbingRight = false; // Önceki durum sýfýrlanýyor
        }


        if (other.CompareTag("talk") && !TalkButtonTrigger && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
        }
        //Konuþmaya devam etme butonuna týklamayý kontrol etme
        else if (other.CompareTag("conTalk") && !TalkContinueTrigger && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
        }
        //ilk secenek seçildiðinde
        else if (other.CompareTag("firstChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
        }
        //ilk secenekler secildiðinde
        else if (other.CompareTag("secondChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
        }
        //ilk secenekler secildiðinde
        else if (other.CompareTag("thirdChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutuþ gerçekleþtiði için true olarak iþaretleniyor
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //right hand tutuþu aktif olduðunda ve objenin içerisinde çýkarsa
        if (HandManager.IsGrabbingRight)
        {
            ////Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                DialogManager.GetInstance().EnterDialogueMode(GameManagerSorgu.GetInstance().inkJSON); //ÝnkJSON ile konuþmayý baþlat
                TalkButtonTrigger = true;
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                buttonChange(other);
                TalkContinueTrigger = true;
            }
            else if (other.CompareTag("firstChoice"))
            {
                buttonChange(other);
                DialogManager.GetInstance().MakeChoice(0);
            }
            else if (other.CompareTag("secondChoice"))
            {
                buttonChange(other);
                DialogManager.GetInstance().MakeChoice(1);
            }
            else if (other.CompareTag("thirdChoice"))
            {
                buttonChange(other);
                DialogManager.GetInstance().MakeChoice(2);
            }
        }
    }

    // Butonlarýn rengini deðiþtirme
    private void buttonColorChange(Collider other)
    {
        Button conTalkButton = other.GetComponentInParent<Button>();

        if (conTalkButton != null)
        {
            Image buttonImage = conTalkButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                // Orijinal rengi sakla
                originalColors.Add(buttonImage.color);
                // Rengi deðiþtir
                buttonImage.color = Color.green;
            }
        }
    }

    // Butonlarýn rengini eski haline getirme
    private void buttonChange(Collider other)
    {
        Button conTalkButton = other.GetComponentInParent<Button>();

        if (conTalkButton != null)
        {
            Image buttonImage = conTalkButton.GetComponent<Image>();
            if (buttonImage != null && originalColors.Count > 0)
            {
                // Listenin son elemanýný alarak orijinal rengi geri döndür
                buttonImage.color = Color.grey;
            }
        }
    }
}
