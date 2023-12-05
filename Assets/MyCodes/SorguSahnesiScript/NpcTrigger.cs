using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    [Header("Talk Start Button")]
    [SerializeField]  public GameObject objeToActivate; // Týkladýðýnda konuþmaya baþlatan nesne
    [Header("Talk Panel")]
    [SerializeField] public GameObject talkPanel; // sTORY PANELÝ
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; // inkJSON 

    private bool playerInRange; // Player rangedemi
    private bool objeActivated; // Bir kere etkinleþtirildi mi?

    private void Awake()
    {
        objeActivated = true; 
        playerInRange = true;
        objeToActivate.SetActive(false);
        talkPanel.SetActive(false);

    }

    private void Update()
    {
        //player konuþma rangeinde olduðunda
        if (playerInRange)
        {
            //Talk butonunu etkinleþtir
            if (!objeActivated)
            {
                objeToActivate.SetActive(true); // Konuþma butonunu etkinleþtir
                objeActivated = true; // Bayraðý true olarak iþaretle
            }
        }
        else{
            objeToActivate.SetActive(false);
        }
        //konuþmayý baþlat
        if (TriggerButL.GetInstance().TalkButtonTrigger || TriggerButR.GetInstance().TalkButtonTrigger)
        {
            DialogManager.GetInstance().EnterDialogueMode(inkJSON); //ÝnkJSON ile konuþmayý baþlat
            TriggerButL.GetInstance().TalkButtonTrigger = false; // bir kere çalýþtýrmak için false yapýlýyor
            TriggerButR.GetInstance().TalkButtonTrigger = false; // bir kere çalýþtýrmak için false yapýlýyor
            objeToActivate.SetActive(false); //Talk butonunu kapat
            talkPanel.SetActive(true); //StoryPaneli aç
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PLayer girdiðinde
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Rangeden çýkar
            playerInRange = false;
            //Talk Paneli sýfýrla
            talkPanel.SetActive(false);
            // Eðer obje daha önce etkinleþtirildiyse, bayraðý sýfýrla
            if (objeActivated)
            {
                objeActivated = false;
            }
        }
    }
}
