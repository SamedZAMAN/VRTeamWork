using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public int numberOfChoice; //ka��nc� se�enek     
    public bool choiceSelected; //se�ilip se�ilmedi�ini anlamak i�in se�enek

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
        {
           
        }
    }
}
