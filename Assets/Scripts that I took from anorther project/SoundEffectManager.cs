using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;
    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();  
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();  
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)  
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName); 
        if (audioClip != null)  
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        sfxSlider.onValueChanged.AddListener(OnValueChange);  
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void OnValueChange(float value) 
    {
        SetVolume(value);
    }
}
