using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{
    //Input System
    public InputActionReference grabActionLeft; // XR Input System eylemini bu de�i�kende saklay�n
    public InputActionReference grabActionRight; // XR Input System eylemini bu de�i�kende saklay�n
    public static bool IsGrabbingLeft { get; private set; } = false; // Sol elin "grab" eyleminin durumunu saklamak i�in bir bool de�i�ken
    public static bool IsGrabbingRight { get; private set; } = false; // Sa� elin "grab" eyleminin durumunu saklamak i�in bir bool de�i�ken

    private static HandManager instance;

    private void Start()
    {

        // Eylemi tetikleyen d��menin durumunu takip etmek i�in bir "Started" eventi ekleyin
        grabActionLeft.action.performed += GrabActionStarted;
        // Eylemin b�rak�lma durumunu takip etmek i�in bir "Canceled" eventi ekleyin
        grabActionLeft.action.canceled += GrabActionCanceled;

        // Eylemi tetikleyen d��menin durumunu takip etmek i�in bir "Started" eventi ekleyin
        grabActionRight.action.performed += GrabActionStarted;
        // Eylemin b�rak�lma durumunu takip etmek i�in bir "Canceled" eventi ekleyin
        grabActionRight.action.canceled += GrabActionCanceled;

        //�nstane yapma
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
        // Event dinlemeyi kald�r�n
        grabActionLeft.action.performed -= GrabActionStarted;
        grabActionLeft.action.canceled -= GrabActionCanceled;

        // Event dinlemeyi kald�r�n
        grabActionRight.action.performed -= GrabActionStarted;
        grabActionRight.action.canceled -= GrabActionCanceled;
    }
    // "Grab" eylemi ba�lad���nda
    private void GrabActionStarted(InputAction.CallbackContext context)
    {
        // "Grab" eylemi ba�lad���nda bu metot �a�r�l�r
        if (context.action == grabActionLeft.action)
        {
            IsGrabbingLeft = true;
        }
        else if (context.action == grabActionRight.action)
        {
            IsGrabbingRight = true;
        }
    }
    // "Grab" eylemi b�rak�ld���nda
    private void GrabActionCanceled(InputAction.CallbackContext context)
    {
        // "Grab" eylemi b�rak�ld���nda bu metot �a�r�l�r
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

