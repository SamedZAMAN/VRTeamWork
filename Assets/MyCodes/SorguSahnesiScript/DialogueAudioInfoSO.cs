using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoSO", order = 1)]

public class DialogueAudioInfoSO : ScriptableObject
{    
    public string id;

    public AudioClip[] dialogueTypingSoundClips; //konu�ulacak audio
    [Range(0, 5)]
    public int frequencyLevel = 2;  //Konu�mada ne kadar aral�klarla ses verece�ini ayarlar
    [Range(-3, 3)]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    public float maxPitch = 3f;
    public bool stopAudioSource; //Konu�ma durdurmak i�in
}
