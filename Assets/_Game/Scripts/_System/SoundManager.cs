using UnityEngine;

public class SoundManager : SingletonObject<SoundManager>
{
    [SerializeField] private AudioSource soundAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    public void PlaySound(SoundTypeEnum soundTypeEnum)
    {
        SoundConfigData soundData = SoundConfig.Ins.GetSoundData(soundTypeEnum);
        if (soundData != null && soundAudioSource != null)
        {
            soundAudioSource.PlayOneShot(soundData.Clip);
        }
    }

    public void PlayMusic(MusicTypeEnum musicTypeEnum)
    {
        MusicConfigData musicData = SoundConfig.Ins.GetMusicData(musicTypeEnum);
        if (musicData != null && musicAudioSource != null)
        {
            musicAudioSource.clip = musicData.Clip;
            musicAudioSource.Play();
        }
    }
}
