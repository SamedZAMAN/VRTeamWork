using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public int numberOfChoice; //kaçýncý seçenek     
    public bool choiceSelected; //seçilip seçilmediðini anlamak için seçenek

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
        {
           
        }
    }
}
