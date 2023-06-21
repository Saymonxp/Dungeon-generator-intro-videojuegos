using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    private AudioSource audioController;

    private void Awake()
    {
        audioController = GetComponent<AudioSource>();
    }

    public void selectAudio(int index, float volume) 
    {
        audioController.PlayOneShot(audios[index], volume);
    }
}
