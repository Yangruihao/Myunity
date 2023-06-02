using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SoundManager:Singleton<SoundManager>
{
    private float volume = -1;
    private float musicVolume = -1;
    private float voiceVolume = -1;

    private AudioSource _Music;
    private AudioSource _Voice;
    public void Init(AudioSource music, AudioSource voice)
    {
        this._Music = music;
        this._Voice = voice;

        _Music.volume = Volume * MusicVolume;
        _Voice.volume = Volume * VoiceVolume;
        PlayBG();
    }
    public float Volume
    {
        get 
        {
            if(volume == -1)
                volume = PlayerPrefs.GetFloat("volume", 1.0f);
            return volume;
        }
        set 
        {
            volume = value;
            PlayerPrefs.SetFloat("volume", value);
            _Music.volume = Volume * MusicVolume;
            _Voice.volume = Volume * VoiceVolume;
        }
    }

    public float MusicVolume
    {
        get
        {
            if (musicVolume == -1)
                musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            PlayerPrefs.SetFloat("musicVolume", value);
            _Music.volume = Volume * MusicVolume;
        }
    }

    public float VoiceVolume
    {
        get
        {
            if (voiceVolume == -1)
                voiceVolume = PlayerPrefs.GetFloat("voiceVolume", 1.0f);
            return voiceVolume;
        }
        set
        {
            voiceVolume = value;
            PlayerPrefs.SetFloat("voiceVolume", value);
            _Voice.volume = Volume * VoiceVolume;
        }
    }

    public void PlayBG()
    {
        _Music.clip = Resources.Load<AudioClip>("Audio/BG/bgm");
        _Music.Play();
    }

    public void PlayVoice(string name)
    {
        _Voice.Stop();
        var clip = Resources.Load<AudioClip>($"Audio/Voice/{name}");
        _Voice.PlayOneShot(clip);
    }

}