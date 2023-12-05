using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public GameObject visualCue;
    private bool dialogueStart = false;
    public GameObject devamButton;

    private static DialogueTrigger instance;

    private void Awake()
    {
        instance = this;
        devamButOff();
    }

    public static DialogueTrigger GetInstance()
    {
        return instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        //hand managerdan scriptlerin deðerlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;

        if (isGrabbingLeft)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
            {
                //renk deðiþtirmeyi koy butona
            }
        }
        if (isGrabbingRight)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
            {
                //renk deðiþtirmeyi koy butona
            }
        }
    }

    private void Update()
    {
        if (dialogueStart && !DialogueManager.GetInstance().dialogueIsPlaying)
        {

            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            dialogueStart = false;
        }
        else
        {
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        //hand managerdan scriptlerin deðerlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;
        //sol el true olduðunda
        if (isGrabbingRight)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
            {
                dialogueStart = true;
            }
        }
        if (isGrabbingLeft)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
            {
                dialogueStart = true;
            }
        }
    }

    public void devamButOff()
    {
        devamButton.SetActive(false);
    }
    public void devamButOn()
    {
        devamButton.SetActive(true);
    }
}
