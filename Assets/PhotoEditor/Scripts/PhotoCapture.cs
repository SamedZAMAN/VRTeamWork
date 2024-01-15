using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{ 
    
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private float photoClose;
    [SerializeField] private TextMeshProUGUI photoInformation; // TextMeshProUGUI nesnesi


    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;

    [Header("PhotoScreen")]
    [SerializeField] private GameObject cameraScreen;

    [Header("Raycast For Camera")]
    [SerializeField] private Camera raycastCamera; // Raycast için kamera deðiþkeni



    private bool viewingPhoto;
    private Texture2D screenCapture;

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }


    IEnumerator CapturePhoto() 
    {
        //Kaydetmeden kamera çizgileri gözükmesin diye kapat
        cameraScreen.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        ShowPhoto(); // Ekran görüntüsünü göster
        //Camera UI set false
        // --- RAYCAST AL --- \\
        PerformRaycast(); // Raycast'ý çalýþtýr
    }

    void ShowPhoto() //Photo Area
    {
        //Camera Ekranýný Kaydet
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);

        //Do flash
        StartCoroutine(CameraFlashEffect());
        //Ekraný kapat
        StartCoroutine(CameraCloseTime());
        fadingAnimation.Play("PhotoFade");
    }

    IEnumerator CameraFlashEffect()
    {
        //Play Some audio
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }
    IEnumerator CameraCloseTime()
    {
        //Play Some audio
        yield return new WaitForSeconds(photoClose);
        RemovePhoto();
        viewingPhoto = false;
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        //CameraUI ture
    }

    void PerformRaycast()
    {
        RaycastHit hit;
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);

        // Raycast iþlemi gerçekleþtirilir
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Çarpýlan nesnenin ismi alýnýr ve yazdýrýlýr
            Debug.Log("Çarpýlan Nesne: " + hit.collider.gameObject.name);

            // --- Çarpýþan nesnelerin ne olduðuna göre iþlem yapar
            if(hit.collider.gameObject.name == "EnemyBackground")
            {
                photoInformation.text = "ZOMBÝE'nin sýrtýný görmüþ bulunmaktasýn";
            }
        }
    }
}

