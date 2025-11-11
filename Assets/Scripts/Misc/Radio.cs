using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Radio : MonoBehaviour
{
    public AudioSource staticSource;
    public AudioSource musicSource;
    public List<AudioClip> audioClips;
    public float switchTime;
    float timer;
    public GameObject cam;
    bool fadeOut = true;
    private int currentTrack;
    public bool isPlaying = true;

    void Start()
    {
        musicSource.clip = audioClips[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out var hit, 3f)) // i know im running three raycasts at once to do very similar things but i need this done fast lol
            {
                if (hit.collider.gameObject.tag == "RadioDial")
                {
                    timer = Time.time + switchTime;
                    fadeOut = true;
                    StartCoroutine(SwitchTracks());
                }
                if (hit.collider.gameObject.tag == "RadioPower")
                {
                    StopAllCoroutines();
                    if (isPlaying)
                    {
                        isPlaying = false;
                        staticSource.Pause();
                        musicSource.volume = 0;
                    }
                    else
                    {
                        isPlaying = true;
                        staticSource.Play();
                        musicSource.volume = 1;
                        if (!musicSource.isPlaying)
                            musicSource.Play();
                    }
                }
            }
        }
        if (!musicSource.isPlaying)
        {
            currentTrack++;
            if (currentTrack >= audioClips.Count)
                currentTrack = 0;
                musicSource.clip = audioClips[currentTrack];
            musicSource.Play();
        }
    }

    IEnumerator SwitchTracks()
    {
        while (musicSource.volume > 0 && fadeOut)
        {
            musicSource.volume -= Time.deltaTime / (switchTime / 2f);
            staticSource.volume += Time.deltaTime / (switchTime / 2f);
            yield return null;
        }
        if (fadeOut && musicSource.volume <= 0)
        {
            fadeOut = false;
            currentTrack++;
            if (currentTrack >= audioClips.Count)
                currentTrack = 0;
            musicSource.clip = audioClips[currentTrack];
            if (!musicSource.isPlaying)
                musicSource.Play();
            yield return null;
        }
        while (musicSource.volume < 1 && !fadeOut)
        {
            musicSource.volume += Time.deltaTime / (switchTime / 2f);
            staticSource.volume -= Time.deltaTime / (switchTime / 2f);
            yield return null;
        }
        while (timer < Time.time)
        {
            musicSource.volume = 1;
            staticSource.volume = 0;
            yield break;
        }
    }
}
