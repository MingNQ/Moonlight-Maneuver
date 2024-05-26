using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreData : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("Music", audioManager.musicSource.volume);
        PlayerPrefs.SetFloat("SFX", audioManager.SFXSource.volume);
    }

    public void LoadPrefs()
    {
        audioManager.MusicControl(PlayerPrefs.GetFloat("Music", 0));
        audioManager.SFXControl(PlayerPrefs.GetFloat("SFX", 0));
    }
}
