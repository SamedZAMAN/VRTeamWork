using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Bilgi Kutusu ELementleri
    [Header("UI Bilgi Kutusu ELEMENTLER�")]
    [SerializeField]
    private Canvas bilgiKutusu;   // Bilgi kutusu i�in Canvas bile�eni

    [SerializeField]
    private TMP_Text baslikText;       // Ba�l�k metni i�in Text bile�eni

    [SerializeField]
    private TMP_Text bilgiText;     // Bilgi metni i�in Text bile�eni
    [SerializeField]
    private GameObject bilgiButtonCollider�;     // Bilgi butonu i�in collider
    private bool bilgiKutusuActive;
    [SerializeField]
    private Button buttonBilgi;         // Button bile�eni
    [SerializeField]
    private GameObject dialoguePanel;       // Ba�l�k metni i�in Text bile�eni
    public GameObject BilgiKutusuAnchor; //Bilgi Kutusu Yer Ayarlama
    public GameObject DialogueAnchor; // dialog i�in anchor


    private void Start()
    {
        // Ba�lang��ta bilgi kutusunu kapal� yap
        bilgiKutusu.enabled = false;
        bilgiKutusuActive = false;
    }

    // Bilgi kutusunu kapatmak i�in kullan�lacak fonksiyon
    public void KapatBilgiKutusu()
    {
        bilgiKutusu.enabled = false;
        bilgiKutusuActive = false;
    }

    // Bilgi kutusunu a�mak i�in kullan�lacak fonksiyon
    public void AcBilgiKutusu()
    {
        bilgiKutusu.enabled = true;
        bilgiKutusuActive = true;
    }

    // Ba�l�k ve bilgi metnini g�ncellemek i�in kullan�lacak fonksiyon
    public void GuncelleBilgi(string yeniBaslik, string yeniBilgi)
    {
        baslikText.text = yeniBaslik;
        bilgiText.text = yeniBilgi;
    }
    //Butonun rengini de�i�tirme 
    public void ChangeButtonColor(Color newColor)
    {
        if (buttonBilgi != null)
        {
            Image image = buttonBilgi.image; // Button bile�eninden Image bile�enini al
            if (image != null)
            {
                image.color = newColor; // Image bile�eninin rengini de�i�tir
            }
            else
            {
                Debug.LogError("Button does not have an Image component. Make sure to add an Image component to the Button in the Unity Inspector.");
            }
        }
        else
        {
            Debug.Log("Renk de�i�tirilemedi");
        }
    }

    private void Update()
    {
        if (bilgiKutusuActive) //kitap a��ld���nda
        {
            bilgiKutusu.transform.position = BilgiKutusuAnchor.transform.position;
            bilgiKutusu.transform.eulerAngles = new Vector3(BilgiKutusuAnchor.transform.eulerAngles.x + 15, BilgiKutusuAnchor.transform.eulerAngles.y, 0);
        }
        dialoguePanel.transform.position = DialogueAnchor.transform.position;
        dialoguePanel.transform.eulerAngles = new Vector3(DialogueAnchor.transform.eulerAngles.x + 15, DialogueAnchor.transform.eulerAngles.y, 0);
    }

}
