using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBut : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konuþmaya baþlama butonu sorgulama
    public bool TalkContinueTrigger; //Konuþmaya devam etme butonu sorgulama
    public int numberOfChoice; //secenek sayýlarý
    private Color originalColor; // Eski rengi tutacak deðiþken



    private static TriggerBut instance;


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
    public static TriggerBut GetInstance()
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


    private void OnTriggerStay(Collider other)
    {

            
            ////Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                TalkButtonTrigger = true;
                Debug.Log("konuþma basladý");
            }
            //Konuþmaya devam etme butonuna týklamayý kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                buttonChange(other);
                TalkContinueTrigger = true;
                Debug.Log("konuþma devam etti");
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

    private void OnTriggerExit(Collider other)
    {
            ////Konuþmaya baþlamayý baþlatmak için , talk adlý tage týkladýðýnda , DialogueManager Kýsmýnda kullanýlýyor update kýsmýnda ( dialogu baþlatmak için)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
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

    //butonlarýn rengini eski haline getirme
    private void buttonChange(Collider other)
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
    }

    //butonlarýn rengini deðiþtirme
    private void buttonColorChange(Collider other)
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
