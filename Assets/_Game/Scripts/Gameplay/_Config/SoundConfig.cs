using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundConfig : SingletonScriptableObject<SoundConfig>
{
    [TableList]
    [SerializeField] private List<SoundConfigData> soundData = new List<SoundConfigData>();
    [TableList]
    [SerializeField] private List<MusicConfigData> musicData = new List<MusicConfigData>();

    private Dictionary<SoundTypeEnum, SoundConfigData> soundDict = new Dictionary<SoundTypeEnum, SoundConfigData>();
    private Dictionary<MusicTypeEnum, MusicConfigData> musicDict = new Dictionary<MusicTypeEnum, MusicConfigData>();

    private bool isInitialized = false;

    private void InitSound()
    {
        for (int i = 0; i < soundData.Count; i++)
        {
            if (!soundDict.ContainsKey(soundData[i].Type))
            {
                soundDict.Add(soundData[i].Type, soundData[i]);
            }
        }
    }

    private void InitMusic()
    {
        for (int i = 0; i < musicData.Count; i++)
        {
            if (!musicDict.ContainsKey(musicData[i].Type))
            {
                musicDict.Add(musicData[i].Type, musicData[i]);
            }
        }
    }

    public SoundConfigData GetSoundData(SoundTypeEnum type)
    {
        if (!isInitialized) InitSound();
        if (soundDict.TryGetValue(type, out SoundConfigData result))
        {
            return result;
        }
        return null;
    }

    public MusicConfigData GetMusicData(MusicTypeEnum type)
    {
        if (!isInitialized) InitMusic();
        if (musicDict.TryGetValue(type, out MusicConfigData result))
        {
            return result;
        }
        return null;
    }
}

[Serializable]
public class SoundConfigData
{
    [SerializeField] private SoundTypeEnum type;
    [SerializeField] private AudioClip clip;
    public SoundTypeEnum Type => type;
    public AudioClip Clip => clip;
}

[Serializable]
public class MusicConfigData
{
    [SerializeField] private MusicTypeEnum type;
    [SerializeField] private AudioClip clip;
    public MusicTypeEnum Type => type;
    public AudioClip Clip => clip;
}