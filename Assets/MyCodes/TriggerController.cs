using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerController : MonoBehaviour
{
    private GameManager gameManager; // GameManager scriptini içerecek deðiþken
    private Color transparentColor = new Color(1f, 1f, 1f, 0f); // Tamamen transparan renk
    private Color eskiRenk; //objeleri eski rengine çevirmek için
    private Image imageComponent; //objelerin içerisindeki image
    private string objectTag; // Objenin etiketi
    private Renderer objectRenderer; // Objeyi renklendirmek için Renderer bileþeni

    private void Awake()
    {
        // GameManager adýnda bir GameObject'i bulun
        GameObject gameManagerObject = GameObject.Find("GameManager");

        // Objeyi renklendirmek için Renderer bileþenini al
        objectRenderer = GetComponent<Renderer>();

        // Objenin etiketini al
        objectTag = gameObject.tag;

        imageComponent = GetComponent<Image>(); // Nesnenin içindeki Image bileþenini alýn
        if(imageComponent != null)
        {
            eskiRenk = imageComponent.color; // Baþlangýçta rengini kaydedin
        }

        // GameManager scriptine eriþin
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //hand managerdan scriptlerin deðerlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;

        //sol el true olduðunda
        if (isGrabbingLeft)
        {
            //suc aletine temsa edildiðinde
            if (objectTag == "SucAleti1")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Kan Topu", "Topun Üzerinde Kanlý bir obje bulduk ve bu objenin kanýný incelememiz lazým.");

                    // Objeyi tamamen transparan hale getir
                    objectRenderer.material.color = transparentColor;
                }
            }
            //suc aletine temsa edildiðinde
            else if (objectTag == "silah")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Desert Eagle", "Ateþlenmiþ bir desert eagle bulundu");
                }
            }
            //Notebook kapatma
            else if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    // Sol el ile "HandL" layer temasý olduðunda bu blok çalýþýr
                    Debug.Log("Sol el ile Layer 'HandL' temasý.");
                    gameManager.ChangeButtonColor(Color.black);
                }
            }
        }

        //sað el true olduðunda
        if (isGrabbingRight)
        {
            if (objectTag == "SucAleti1")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Kan Topu", "Topun Üzerinde Kanlý bir obje bulduk ve bu objenin kanýný incelememiz lazým.");

                    // Objeyi tamamen transparan hale getir
                    objectRenderer.material.color = transparentColor;
                }
            }
            //suc aletine temsa edildiðinde
            else if (objectTag == "silah")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    gameManager.AcBilgiKutusu();
                    gameManager.GuncelleBilgi("Desert Eagle", "Ateþlenmiþ bir desert eagle bulundu");
                }
            }
            else if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    // Sað el ile "HandR" layer temasý olduðunda bu blok çalýþýr
                    Debug.Log("Sað el ile Layer 'HandR' temasý.");
                    gameManager.ChangeButtonColor(Color.black);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //hand managerdan scriptlerin deðerlerini kontrol etme
        bool isGrabbingLeft = HandManager.IsGrabbingLeft;
        bool isGrabbingRight = HandManager.IsGrabbingRight;
        //sol el true olduðunda
        if (isGrabbingLeft)
        {
            if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandL"))
                {
                    // bilgikutusu kapanýr
                    gameManager.KapatBilgiKutusu();
                }
            }
        }

        //sað el true olduðunda
        if (isGrabbingRight)
        {
            if (objectTag == "noteBook")
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("HandR"))
                {
                    // bilgikutusu kapanýr
                    gameManager.KapatBilgiKutusu();
                }
            }
        }

    }

}
