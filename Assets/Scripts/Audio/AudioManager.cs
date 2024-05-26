using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    public AudioClip background;
    public AudioClip jump;
    public AudioClip collectHeart;
    public AudioClip hurt;
    public AudioClip press;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySound(AudioClip _clip)
    {
        SFXSource.PlayOneShot(_clip);
    }

    public void MuteMusic(bool _status)
    {
        musicSource.mute = _status;
    }

    public void MuteSFX(bool _status)
    {
        SFXSource.mute = _status;
    }

    public void MusicControl(float _value)
    {
        musicSource.volume = _value;
    }

    public void SFXControl(float _value)
    {
        SFXSource.volume = _value;
    }
}
