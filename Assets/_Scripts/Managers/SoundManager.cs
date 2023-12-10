using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] private Sound[] _soundData;
    [SerializeField] private AudioSource _bgmSound;
    [SerializeField] private AudioSource _sfxSound;
    [SerializeField] private AudioSource _stepSound;
    [SerializeField] private AudioSource _birdSound;

    private bool _isEven;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);

        EventManager.OnSoundPlayOnce += PlaySoundOnce;
        EventManager.OnBGMPlay += PlaySound;
        EventManager.OnFootStep += PlayStepSound;
        EventManager.OnSoulUsed += OnSoulUsed;
        EventManager.OnBirdSound += PlayBirdSound;
        EventManager.StopBirdSound += StopBirdSound;
    }

    private void OnDestroy()
    {
        EventManager.OnSoundPlayOnce -= PlaySoundOnce;
        EventManager.OnBGMPlay -= PlaySound;
        EventManager.OnFootStep -= PlayStepSound;
        EventManager.OnSoulUsed -= OnSoulUsed;
        EventManager.StopBirdSound -= StopBirdSound;
    }

    private void OnSoulUsed()
    {
        PlaySoundOnce(12);
    }


    public void PlaySoundOnce(int ID)
    {
        Debug.Log("Play: "+ ID);
        Sound s = Array.Find(_soundData, Sound => Sound.audioID == ID);
        if (s == null)
        {
            Debug.LogError("Sound " + ID + "Not Found");
        }
        else
        {
            
            _sfxSound.PlayOneShot(_soundData[ID].clip);
            Debug.Log("Play: "+ ID);
            
        }
    }
        
    /// <summary>
    /// used to play sound multiple time without caring the it already being play
    /// </summary>
    /// <param name="ID"></param>
    public void PlaySound(int ID)
    {
            
        Sound s = Array.Find(_soundData, Sound => Sound.audioID == ID);
        if (s == null)
        {
            Debug.LogError("Sound " + ID + "Not Found");
        }
        else
        {
            _bgmSound.clip = _soundData[ID].clip;
            _bgmSound.Play();
                
        }
            
    }

    public void PlayStepSound()
    {
        _stepSound.PlayOneShot(_isEven ? _soundData[11].clip : _soundData[10].clip);

        _isEven = !_isEven;
    }

    public void PlayBirdSound()
    {
        _birdSound.clip = _soundData[13].clip;
        _birdSound.Play();
    }

    public void StopBirdSound()
    {
        _birdSound.Stop();
    }

    public void SetSoundVolume(int vol, SoundType type)
    {
        foreach (Sound s in _soundData)
        {
            if (s._soundType == type)
            {
                s.source.volume = vol;
            }
        }
    }

    

}


[System.Serializable]
public class Sound
{
    /// <summary>
    /// name of the audio
    /// </summary>
    public string audioName;
        
    /// <summary>
    /// audio ID clip
    /// </summary>
    public int audioID;

    /// <summary>
    /// the clip of the audio 
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// audio volume
    /// </summary>
    [Range(0f, 1f)] public float volume;
        
    /// <summary>
    /// audio pitch
    /// </summary>
    [Range(.1f, 3f)] public float pitch;

    /// <summary>
    /// should the audio clip looping 
    /// </summary>
    public bool loop;

    /// <summary>
    /// audio sources
    /// </summary>
    [HideInInspector] public AudioSource source;

    public SoundType _soundType;
}

public enum SoundType
{
    Music,
    SoundFX,
    Footstep
}
