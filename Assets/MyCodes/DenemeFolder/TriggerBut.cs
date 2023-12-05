using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBut : MonoBehaviour
{
    [Header("Dokunulacak Butonlar")]
    [SerializeField] public GameObject ContinueTalkButton; //Konumaya devam etme butonu
    public bool TalkButtonTrigger; //Konu�maya ba�lama butonu sorgulama
    public bool TalkContinueTrigger; //Konu�maya devam etme butonu sorgulama
    public int numberOfChoice; //secenek say�lar�
    private Color originalColor; // Eski rengi tutacak de�i�ken



    private static TriggerBut instance;


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
    public static TriggerBut GetInstance()
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


    private void OnTriggerStay(Collider other)
    {

            
            ////Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
                TalkButtonTrigger = true;
                Debug.Log("konu�ma baslad�");
            }
            //Konu�maya devam etme butonuna t�klamay� kontrol etme
            else if (other.CompareTag("conTalk"))
            {
                buttonChange(other);
                TalkContinueTrigger = true;
                Debug.Log("konu�ma devam etti");
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

    private void OnTriggerExit(Collider other)
    {
            ////Konu�maya ba�lamay� ba�latmak i�in , talk adl� tage t�klad���nda , DialogueManager K�sm�nda kullan�l�yor update k�sm�nda ( dialogu ba�latmak i�in)
            if (other.CompareTag("talk"))
            {
                buttonChange(other);
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

    //butonlar�n rengini eski haline getirme
    private void buttonChange(Collider other)
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
    }

    //butonlar�n rengini de�i�tirme
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
                // Rengi de�i�tir
                buttonImage.color = Color.green;
            }
        }
    }
}
