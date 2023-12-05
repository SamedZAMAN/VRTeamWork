using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerButL : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konuþmaya baþlama butonu sorgulama
    public bool TalkContinueTrigger; //Konuþmaya devam etme butonu sorgulama
    public int numberOfChoice; //Dialog secenek numaralarý
    private Color originalColor; // Eski rengi tutacak deðiþken

    private static TriggerButL instance;


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
    public static TriggerButL GetInstance()
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
        if (HandManager.IsGrabbingLeft)
        {
            //Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
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
                        // Rengi deðiþtir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
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
                        // Rengi deðiþtir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenek seçildiðinde
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
                        // Rengi deðiþtir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenekler secildiðinde
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
                        // Rengi deðiþtir
                        buttonImage.color = Color.green;
                    }
                }
            }
            //ilk secenekler secildiðinde
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
                        // Rengi deðiþtir
                        buttonImage.color = Color.green;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //right hand tutuþu aktif olduðunda
        if (HandManager.IsGrabbingLeft)
        {
            ////Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri dön
                        buttonImage.color = originalColor;
                    }
                }
                TalkButtonTrigger = true;
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                Button conTalkButton = other.GetComponentInParent<Button>();

                if (conTalkButton != null)
                {
                    Image buttonImage = conTalkButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        // Orijinal rengi kullanarak geri dön
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
                        // Orijinal rengi kullanarak geri dön
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
                        // Orijinal rengi kullanarak geri dön
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
                        // Orijinal rengi kullanarak geri dön
                        buttonImage.color = originalColor;
                    }
                }
                DialogManager.GetInstance().MakeChoice(2);
            }
        }
    }
}
