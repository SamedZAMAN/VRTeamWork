using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denemeScript : MonoBehaviour
{

    public void CameraSelect()
    {
        Debug.Log("kameraya girildi"); //Kameray� elimize ald���m�z k�s�m bir i�leve gerek yok
    }
    public void CameraDisSelect()
    {
        Debug.Log("kameradan ��k�ld�");
    }
    public void CameraFocus()
    {
        Debug.Log("kameraya focusland�"); //gerek yok
    }
    public void CameraDisFocus()
    {
        Debug.Log("kameraya focustan c�k�ld�"); //gerek yok
    }
    public void CameraActivate()
    {
        Debug.Log("kameraya aktif oldu"); //Burada UI ekran� gelecek
    }
    public void CameraDeActive()
    {
        Debug.Log("kameraya aktiflikten ��kar�ld�");//Kameraya b�rak�ld��� anda foto�raf� �ek
    }
    void Update()
    {
        // A tu�una bas�ld���nda
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // JoystickButton0, genellikle A tu�una denk gelir
        {
            Debug.Log("A tu�una bas�ld�");
            // Buraya A tu�una bas�ld���nda yap�lacak i�lemleri ekleyebilirsin
        }

        // X tu�una bas�ld���nda
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // JoystickButton2, genellikle X tu�una denk gelir
        {
            Debug.Log("X tu�una bas�ld�");
            // Buraya X tu�una bas�ld���nda yap�lacak i�lemleri ekleyebilirsin
        }
    }
}
