using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerButR : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konu�maya ba�lama butonu sorgulama
    public bool TalkContinueTrigger; //Konu�maya devam etme butonu sorgulama
    public int numberOfChoice; //secenek say�lar�
    private List<Color> originalColors = new List<Color>(); // Orijinal renkleri saklayacak liste

    private bool wasGrabbingRight = false; // El i�eriye sokuldu�unda grabbing b�rak�ld���n� kontrol etmek i�in

    private static TriggerButR instance;


    private void Awake()
    {
        //Konu�maya ba�lama butonu kontrolu
        TalkButtonTrigger = false;

        //Konu�maya devam trigger
        TalkContinueTrigger = false;
        //�nstane yapma
        if (instance != null)
        {
            Debug.Log("Found more than one TriggerBut");
        }
        instance = this;
    }
    //�nstance
    public static TriggerButR GetInstance()
    {
        return instance;
    }

    //Konu�ma butonunu false yapma
    public void TalkButtonOff()
    {
        TalkButtonTrigger = false;
    }

    //Giri� yap�ld���nda
    private void OnTriggerEnter(Collider other)
    {
        //right hand tutu�u aktif oldu�unda
        if (HandManager.IsGrabbingRight)
        {
            Debug.Log("item tag: " + other.tag);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
            Debug.Log("wasGrabbing : true");
            //Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk") && !TalkButtonTrigger)
            {
                buttonColorChange(other);
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
            else if (other.CompareTag("conTalk") && !TalkContinueTrigger)
            {
                buttonColorChange(other);
            }
            //ilk secenek se�ildi�inde
            else if (other.CompareTag("firstChoice"))
            {
                buttonColorChange(other);
            }
            //ilk secenekler secildi�inde
            else if (other.CompareTag("secondChoice"))
            {
                buttonColorChange(other);
            }
            //ilk secenekler secildi�inde
            else if (other.CompareTag("thirdChoice"))
            {
                buttonColorChange(other);
            }
        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        //objeye elimi soktuumda ve i�erisinde grabbing i b�rakt���mda i�lem ger�ekle�sin
        if (!HandManager.IsGrabbingRight && wasGrabbingRight)
        {
            Debug.Log("elimi i�erisi soktum ve grab� b�rakt�m");
            ////Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                DialogManager.GetInstance().EnterDialogueMode(GameManagerSorgu.GetInstance().inkJSON); //�nkJSON ile konu�may� ba�lat
                TalkButtonTrigger = true;
                GameManagerSorgu.GetInstance().TalkPanelOn();
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                Debug.Log("konu�maya devam edilmesi laz�m");
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
            wasGrabbingRight = false; // �nceki durum s�f�rlan�yor
        }


        if (other.CompareTag("talk") && !TalkButtonTrigger && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
        }
        //Konu�maya devam etme butonuna t�klamay� kontrol etme
        else if (other.CompareTag("conTalk") && !TalkContinueTrigger && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
        }
        //ilk secenek se�ildi�inde
        else if (other.CompareTag("firstChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
        }
        //ilk secenekler secildi�inde
        else if (other.CompareTag("secondChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
        }
        //ilk secenekler secildi�inde
        else if (other.CompareTag("thirdChoice") && HandManager.IsGrabbingRight)
        {
            buttonColorChange(other);
            wasGrabbingRight = true; // Tutu� ger�ekle�ti�i i�in true olarak i�aretleniyor
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //right hand tutu�u aktif oldu�unda ve objenin i�erisinde ��karsa
        if (HandManager.IsGrabbingRight)
        {
            ////Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                DialogManager.GetInstance().EnterDialogueMode(GameManagerSorgu.GetInstance().inkJSON); //�nkJSON ile konu�may� ba�lat
                TalkButtonTrigger = true;
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
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

    // Butonlar�n rengini de�i�tirme
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
                // Rengi de�i�tir
                buttonImage.color = Color.green;
            }
        }
    }

    // Butonlar�n rengini eski haline getirme
    private void buttonChange(Collider other)
    {
        Button conTalkButton = other.GetComponentInParent<Button>();

        if (conTalkButton != null)
        {
            Image buttonImage = conTalkButton.GetComponent<Image>();
            if (buttonImage != null && originalColors.Count > 0)
            {
                // Listenin son eleman�n� alarak orijinal rengi geri d�nd�r
                buttonImage.color = Color.grey;
            }
        }
    }
}
