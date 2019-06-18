using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    public Slider masterSlider;

    public static MusicPlayer Instance
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
        AudioSource masterAudioSource = GetComponent<AudioSource>();

        masterAudioSource.volume = masterSlider.value;
    }

    private void SetListeners()
    {
        masterSlider.onValueChanged.AddListener(delegate { SetVolume(); });
    }
}