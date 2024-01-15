using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerController : MonoBehaviour
{
    private GameManager gameManager; // GameManager scriptini i�erecek de�i�ken
    private Color transparentColor = new Color(1f, 1f, 1f, 0f); // Tamamen transparan renk
    private Color eskiRenk; //objeleri eski rengine �evirmek i�in
    private Image imageComponent; //objelerin i�erisindeki image
    private string objectTag; // Objenin etiketi
    private Renderer objectRenderer; // Objeyi renklendirmek i�in Renderer bile�eni

    private void Awake()
    {
        // GameManager ad�nda bir GameObject'i bulun
        GameObject gameManagerObject = GameObject.Find("GameManager");

        // Objeyi renklendirmek i�in Renderer bile�enini al
        objectRenderer = GetComponent<Renderer>();

        // Objenin etiketini al
        objectTag = gameObject.tag;

        imageComponent = GetComponent<Image>(); // Nesnenin i�indeki Image bile�enini al�n
        if(imageComponent != null)
        {
            eskiRenk = imageComponent.color; // Ba�lang��ta rengini kaydedin
        }

        // GameManager scriptine eri�in
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //hand managerdan scriptlerin de�erlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;

        //sol el true oldu�unda
        if (isGrabbingLeft)
        {
            //suc aletine temsa edildi�inde
            if (objectTag == "SucAleti1")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Kan Topu", "Topun �zerinde Kanl� bir obje bulduk ve bu objenin kan�n� incelememiz laz�m.");

                    // Objeyi tamamen transparan hale getir
                    objectRenderer.material.color = transparentColor;
                }
            }
            //suc aletine temsa edildi�inde
            else if (objectTag == "silah")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Desert Eagle", "Ate�lenmi� bir desert eagle bulundu");
                }
            }
            //Notebook kapatma
            else if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    // Sol el ile "HandL" layer temas� oldu�unda bu blok �al���r
                    Debug.Log("Sol el ile Layer 'HandL' temas�.");
                    gameManager.ChangeButtonColor(Color.black);
                }
            }
        }

        //sa� el true oldu�unda
        if (isGrabbingRight)
        {
            if (objectTag == "SucAleti1")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Kan Topu", "Topun �zerinde Kanl� bir obje bulduk ve bu objenin kan�n� incelememiz laz�m.");

                    // Objeyi tamamen transparan hale getir
                    objectRenderer.material.color = transparentColor;
                }
            }
            //suc aletine temsa edildi�inde
            else if (objectTag == "silah")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Desert Eagle", "Ate�lenmi� bir desert eagle bulundu");
                }
            }
            else if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    // Sa� el ile "HandR" layer temas� oldu�unda bu blok �al���r
                    Debug.Log("Sa� el ile Layer 'HandR' temas�.");
                    gameManager.ChangeButtonColor(Color.black);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //hand managerdan scriptlerin de�erlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;
        //sol el true oldu�unda
        if (isGrabbingLeft)
        {
            if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    // bilgikutusu kapan�r
                    gameManager.KapatBilgiKutusu();
                }
            }
        }

        //sa� el true oldu�unda
        if (isGrabbingRight)
        {
            if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    // bilgikutusu kapan�r
                    gameManager.KapatBilgiKutusu();
                }
            }
        }

    }

}
