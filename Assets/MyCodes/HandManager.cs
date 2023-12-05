using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{
    //Input System
    public InputActionReference grabActionLeft; // XR Input System eylemini bu deðiþkende saklayýn
    public InputActionReference grabActionRight; // XR Input System eylemini bu deðiþkende saklayýn
    public static bool IsGrabbingLeft { get; private set; } = false; // Sol elin "grab" eyleminin durumunu saklamak için bir bool deðiþken
    public static bool IsGrabbingRight { get; private set; } = false; // Sað elin "grab" eyleminin durumunu saklamak için bir bool deðiþken

    private static HandManager instance;

    private void Start()
    {

        // Eylemi tetikleyen düðmenin durumunu takip etmek için bir "Started" eventi ekleyin
        grabActionLeft.action.performed += GrabActionStarted;
        // Eylemin býrakýlma durumunu takip etmek için bir "Canceled" eventi ekleyin
        grabActionLeft.action.canceled += GrabActionCanceled;

        // Eylemi tetikleyen düðmenin durumunu takip etmek için bir "Started" eventi ekleyin
        grabActionRight.action.performed += GrabActionStarted;
        // Eylemin býrakýlma durumunu takip etmek için bir "Canceled" eventi ekleyin
        grabActionRight.action.canceled += GrabActionCanceled;

        //Ýnstane yapma
        if (instance != null)
        {
            Debug.Log("Found more than one TriggerBut");
        }
        instance = this;
    }
    //instance yapma
    public static HandManager GetInstance()
    {
        return instance;
    }

    //Eylemler
    private void OnDestroy()
    {
        // Event dinlemeyi kaldýrýn
        grabActionLeft.action.performed -= GrabActionStarted;
        grabActionLeft.action.canceled -= GrabActionCanceled;

        // Event dinlemeyi kaldýrýn
        grabActionRight.action.performed -= GrabActionStarted;
        grabActionRight.action.canceled -= GrabActionCanceled;
    }
    // "Grab" eylemi baþladýðýnda
    private void GrabActionStarted(InputAction.CallbackContext context)
    {
        // "Grab" eylemi baþladýðýnda bu metot çaðrýlýr
        if (context.action == grabActionLeft.action)
        {
            IsGrabbingLeft = true;
        }
        else if (context.action == grabActionRight.action)
        {
            IsGrabbingRight = true;
        }
    }
    // "Grab" eylemi býrakýldýðýnda
    private void GrabActionCanceled(InputAction.CallbackContext context)
    {
        // "Grab" eylemi býrakýldýðýnda bu metot çaðrýlýr
        if (context.action == grabActionLeft.action)
        {
            IsGrabbingLeft = false;
        }
        else if (context.action == grabActionRight.action)
        {
            IsGrabbingRight = false;
        }
    }

}

