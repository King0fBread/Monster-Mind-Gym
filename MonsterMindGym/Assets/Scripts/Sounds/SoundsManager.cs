using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundEffectsAudioSource;
    [SerializeField] private AudioSource _backgroundMusicAudioSource;

    private bool _isRequestedSoundBackgroundMusic = false;

    private AudioClip _currentBackgroundMusicSoundClip;

    public AudioClipPairedToSound[] _audioClipsPairedToSound;

    [System.Serializable]
    public class AudioClipPairedToSound
    {
        public AudioClip clip;
        public Sounds sound;
        public bool isBackgroundMusic = false;
    }

    private static SoundsManager _instance;
    public static SoundsManager Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        PlaySound(Sounds.BackgroundMusicLab);

    }
    private void Update()
    {
        if(!_backgroundMusicAudioSource.isPlaying && _currentBackgroundMusicSoundClip != null)
        {
            _backgroundMusicAudioSource.PlayOneShot(_currentBackgroundMusicSoundClip);
        }
    }
    public enum Sounds
    {
        BackgroundMusicLab,
        CoinsCollect,
        Countdown,
        MinigameButtonClick,
        MinigameTargetShot,
        MonsterUpgradeBasic,
        MonsterUpgradeEvolution,
        UIButtonClick,
        UIButtonSelect,

    }
    public void PlaySound(Sounds sound)
    {
        PlayClipInAppropriateAudioSource(GetRequestedAudioClipBySound(sound));
    }
    private AudioClip GetRequestedAudioClipBySound(Sounds sound)
    {
        foreach(var clipToSoundPair in _audioClipsPairedToSound)
        {
            if(clipToSoundPair.sound == sound)
            {
                _isRequestedSoundBackgroundMusic = clipToSoundPair.isBackgroundMusic;
                return clipToSoundPair.clip;
            }
        }
        return null;
    }
    private void PlayClipInAppropriateAudioSource(AudioClip clip)
    {
        if (_isRequestedSoundBackgroundMusic)
        {
            _currentBackgroundMusicSoundClip = clip;

            _backgroundMusicAudioSource.Stop();
            _backgroundMusicAudioSource.PlayOneShot(clip);
        }
        else
        {
            _soundEffectsAudioSource.PlayOneShot(clip);
        }
    }
}
