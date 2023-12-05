using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Bilgi Kutusu ELementleri
    [Header("UI Bilgi Kutusu ELEMENTLERÝ")]
    [SerializeField]
    private Canvas bilgiKutusu;   // Bilgi kutusu için Canvas bileþeni

    [SerializeField]
    private TMP_Text baslikText;       // Baþlýk metni için Text bileþeni

    [SerializeField]
    private TMP_Text bilgiText;     // Bilgi metni için Text bileþeni
    [SerializeField]
    private GameObject bilgiButtonColliderý;     // Bilgi butonu için collider
    private bool bilgiKutusuActive;
    [SerializeField]
    private Button buttonBilgi;         // Button bileþeni
    [SerializeField]
    private GameObject dialoguePanel;       // Baþlýk metni için Text bileþeni
    public GameObject BilgiKutusuAnchor; //Bilgi Kutusu Yer Ayarlama
    public GameObject DialogueAnchor; // dialog için anchor


    private void Start()
    {
        // Baþlangýçta bilgi kutusunu kapalý yap
        bilgiKutusu.enabled = false;
        bilgiKutusuActive = false;
    }

    // Bilgi kutusunu kapatmak için kullanýlacak fonksiyon
    public void KapatBilgiKutusu()
    {
        bilgiKutusu.enabled = false;
        bilgiKutusuActive = false;
    }

    // Bilgi kutusunu açmak için kullanýlacak fonksiyon
    public void AcBilgiKutusu()
    {
        bilgiKutusu.enabled = true;
        bilgiKutusuActive = true;
    }

    // Baþlýk ve bilgi metnini güncellemek için kullanýlacak fonksiyon
    public void GuncelleBilgi(string yeniBaslik, string yeniBilgi)
    {
        baslikText.text = yeniBaslik;
        bilgiText.text = yeniBilgi;
    }
    //Butonun rengini deðiþtirme 
    public void ChangeButtonColor(Color newColor)
    {
        if (buttonBilgi != null)
        {
            Image image = buttonBilgi.image; // Button bileþeninden Image bileþenini al
            if (image != null)
            {
                image.color = newColor; // Image bileþeninin rengini deðiþtir
            }
            else
            {
                Debug.LogError("Button does not have an Image component. Make sure to add an Image component to the Button in the Unity Inspector.");
            }
        }
        else
        {
            Debug.Log("Renk deðiþtirilemedi");
        }
    }

    private void Update()
    {
        if (bilgiKutusuActive) //kitap açýldýðýnda
        {
            bilgiKutusu.transform.position = BilgiKutusuAnchor.transform.position;
            bilgiKutusu.transform.eulerAngles = new Vector3(BilgiKutusuAnchor.transform.eulerAngles.x + 15, BilgiKutusuAnchor.transform.eulerAngles.y, 0);
        }
        dialoguePanel.transform.position = DialogueAnchor.transform.position;
        dialoguePanel.transform.eulerAngles = new Vector3(DialogueAnchor.transform.eulerAngles.x + 15, DialogueAnchor.transform.eulerAngles.y, 0);
    }

}
