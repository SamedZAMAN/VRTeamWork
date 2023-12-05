using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerButL : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konu�maya ba�lama butonu sorgulama
    public bool TalkContinueTrigger; //Konu�maya devam etme butonu sorgulama
    public int numberOfChoice; //Dialog secenek numaralar�
    private Color originalColor; // Eski rengi tutacak de�i�ken

    private static TriggerButL instance;


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
    public static TriggerButL GetInstance()
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
        if (HandManager.IsGrabbingLeft)
        {
            //Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk") && !TalkButtonTrigger)
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kaydet
                        originalColor = buttonImage.color;
                        // Rengi de�i�tir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
            else if (other.CompareTag("conTalk") && !TalkContinueTrigger)
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kaydet
                        originalColor = buttonImage.color;
                        // Rengi de�i�tir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenek se�ildi�inde
            else if (other.CompareTag("firstChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kaydet
                        originalColor = buttonImage.color;
                        // Rengi de�i�tir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenekler secildi�inde
            else if (other.CompareTag("secondChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kaydet
                        originalColor = buttonImage.color;
                        // Rengi de�i�tir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenekler secildi�inde
            else if (other.CompareTag("thirdChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kaydet
                        originalColor = buttonImage.color;
                        // Rengi de�i�tir
                        buttonImage.color = Color.green;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //right hand tutu�u aktif oldu�unda
        if (HandManager.IsGrabbingLeft)
        {
            ////Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri d�n
                        buttonImage.color = originalColor;
                    }
                }
                TalkButtonTrigger = true;
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri d�n
                        buttonImage.color = originalColor;
                    }
                }
                TalkContinueTrigger = true;
            }
            else if (other.CompareTag("firstChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri d�n
                        buttonImage.color = originalColor;
                    }
                }
                DialogManager.GetInstance().MakeChoice(0);
            }
            else if (other.CompareTag("secondChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri d�n
                        buttonImage.color = originalColor;
                    }
                }
                DialogManager.GetInstance().MakeChoice(1);
            }
            else if (other.CompareTag("thirdChoice"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri d�n
                        buttonImage.color = originalColor;
                    }
                }
                DialogManager.GetInstance().MakeChoice(2);
            }
        }
    }
}
