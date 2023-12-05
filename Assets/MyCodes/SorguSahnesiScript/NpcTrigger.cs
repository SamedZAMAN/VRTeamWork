using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    [Header("Talk Start Button")]
    [SerializeField]  public GameObject objeToActivate; // T�klad���nda konu�maya ba�latan nesne
    [Header("Talk Panel")]
    [SerializeField] public GameObject talkPanel; // sTORY PANEL�
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; // inkJSON 

    private bool playerInRange; // Player rangedemi
    private bool objeActivated; // Bir kere etkinle�tirildi mi?

    private void Awake()
    {
        objeActivated = true; 
        playerInRange = true;
        objeToActivate.SetActive(false);
        talkPanel.SetActive(false);

    }

    private void Update()
    {
        //player konu�ma rangeinde oldu�unda
        if (playerInRange)
        {
            //Talk butonunu etkinle�tir
            if (!objeActivated)
            {
                objeToActivate.SetActive(true); // Konu�ma butonunu etkinle�tir
                objeActivated = true; // Bayra�� true olarak i�aretle
            }
        }
        else{
            objeToActivate.SetActive(false);
        }
        //konu�may� ba�lat
        if (TriggerButL.GetInstance().TalkButtonTrigger || TriggerButR.GetInstance().TalkButtonTrigger)
        {
            DialogManager.GetInstance().EnterDialogueMode(inkJSON); //�nkJSON ile konu�may� ba�lat
            TriggerButL.GetInstance().TalkButtonTrigger = false; // bir kere �al��t�rmak i�in false yap�l�yor
            TriggerButR.GetInstance().TalkButtonTrigger = false; // bir kere �al��t�rmak i�in false yap�l�yor
            objeToActivate.SetActive(false); //Talk butonunu kapat
            talkPanel.SetActive(true); //StoryPaneli a�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PLayer girdi�inde
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Rangeden ��kar
            playerInRange = false;
            //Talk Paneli s�f�rla
            talkPanel.SetActive(false);
            // E�er obje daha �nce etkinle�tirildiyse, bayra�� s�f�rla
            if (objeActivated)
            {
                objeActivated = false;
            }
        }
    }
}
