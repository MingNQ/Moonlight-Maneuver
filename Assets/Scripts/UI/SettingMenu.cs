using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSliderControl;
    [SerializeField] private Slider sfxSliderControl;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        musicSliderControl.value = audioManager.musicSource.volume;
        sfxSliderControl.value = audioManager.SFXSource.volume;
    }

    private void Update()
    {
        audioManager.MusicControl(musicSliderControl.value);
        audioManager.SFXControl(sfxSliderControl.value);
    }
}
