using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusicManager:MonoBehaviour
{
    public static BkMusicManager Instance;
    public AudioSource audioSource;
    private void Awake()
    {

        if (Instance==null)
        {
            
            Instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource= GetComponent<AudioSource>();
       MusicData musicData= GameDataManager.Instance.GetData<MusicData>();
       SetMusicVolume(musicData.musicVolume);
       SetMusicMute(musicData.isMusicMute);
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
         Instance=null;
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void SetMusicVolume(float volume)
    {
        audioSource.volume=volume;
    }
    public void SetMusicMute(bool isMute)
    {
        audioSource.mute=!isMute;
    }
    public void SetMusic(AudioClip clip)
    {
        audioSource.clip=clip;
    }
    
}
