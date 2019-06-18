using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPlayer : MonoBehaviour
{
    private static SFXPlayer    instance = null;
    public Slider               sfxSlider;
    public List<AudioClip>      audioClips;

    public static int SFXBUTTON         = 0;
    public static int SFXBUTTONCHOOSE   = 1;
    public static int SFXSUCCESS        = 2;

    public static SFXPlayer Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        SetListeners();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume()
    {
        AudioSource sfxAudioSource = GetComponent<AudioSource>();

        sfxAudioSource.volume = sfxSlider.value;
    }

    private void SetListeners()
    {
        sfxSlider.onValueChanged.AddListener(delegate { SetVolume(); });
    }

    private void PlaySFX(int index)
    {
        AudioSource sfxAudioSource = GetComponent<AudioSource>();

        sfxAudioSource.clip = audioClips[index];
        sfxAudioSource.Play();
    }
}
