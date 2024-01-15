using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denemeScript : MonoBehaviour
{

    public void CameraSelect()
    {
        Debug.Log("kameraya girildi"); //Kamerayý elimize aldýðýmýz kýsým bir iþleve gerek yok
    }
    public void CameraDisSelect()
    {
        Debug.Log("kameradan çýkýldý");
    }
    public void CameraFocus()
    {
        Debug.Log("kameraya focuslandý"); //gerek yok
    }
    public void CameraDisFocus()
    {
        Debug.Log("kameraya focustan cýkýldý"); //gerek yok
    }
    public void CameraActivate()
    {
        Debug.Log("kameraya aktif oldu"); //Burada UI ekraný gelecek
    }
    public void CameraDeActive()
    {
        Debug.Log("kameraya aktiflikten çýkarýldý");//Kameraya býrakýldýðý anda fotoðrafý çek
    }
    void Update()
    {
        // A tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // JoystickButton0, genellikle A tuþuna denk gelir
        {
            Debug.Log("A tuþuna basýldý");
            // Buraya A tuþuna basýldýðýnda yapýlacak iþlemleri ekleyebilirsin
        }

        // X tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // JoystickButton2, genellikle X tuþuna denk gelir
        {
            Debug.Log("X tuþuna basýldý");
            // Buraya X tuþuna basýldýðýnda yapýlacak iþlemleri ekleyebilirsin
        }
    }
}
