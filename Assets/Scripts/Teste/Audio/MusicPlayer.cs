using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    public Slider masterSlider;

    public AudioClip[] adClips;

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

        StartCoroutine(playAudioSequentially());
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

    IEnumerator playAudioSequentially()
    {
        AudioSource adSource = GetComponent<AudioSource>();
        //yield return null;

        int i = 0;
        //1.Loop through each AudioClip
        while (true)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = adClips[i];

            //3.Play Audio
            adSource.Play();

            //4.Wait for it to finish playing
            while (adSource.isPlaying)
                yield return null;

            if (i < adClips.Length)
                i++;
            else
                i = 0;
            //5. Go back to #2 and play the next audio in the adClips array
        }
    }
}