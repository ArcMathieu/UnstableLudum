using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        BackgroundMusic,
        Fire,
        Cauldron,
        ItemFallingInCauldron,
        SatanicHand
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static GameObject musicGameObject;
    private static float getVolume;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        /*soundTimerDictionary[Sound.PlayerForestFT] = 0f;
        soundTimerDictionary[Sound.PlayerDesertFT] = 0f;
        soundTimerDictionary[Sound.PlayerIceFT] = 0f;
        soundTimerDictionary[Sound.PlayerRespawn] = 0f;
        soundTimerDictionary[Sound.SnowSliding] = 0f;*/
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.volume = getVolume;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }

    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.volume = getVolume;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlayLoopSound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.volume = getVolume;
            audioSource.loop = true;
            audioSource.Play();

            //Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlayMusic(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (musicGameObject == null)
            {
                musicGameObject = new GameObject("Music");
                AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.volume = getVolume;
                audioSource.loop = true;
                audioSource.Play();
            } else
            {
                AudioSource audioSource = musicGameObject.GetComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.volume = getVolume;

                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            /*case Sound.PlayerRespawn:
            case Sound.PlayerForestFT:
            case Sound.PlayerDesertFT:
            case Sound.PlayerIceFT:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = SoundObject.Instance.stepSoundSpeed;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return true;
                }
            case Sound.SnowSliding:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0;
                    if (lastTimePlayed > 0)
                    {
                        playerMoveTimerMax = 0.2f;
                    }
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }*/
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundObject.SoundAudioClip soundAudioClip in SoundObject.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                int audioClipNumber = soundAudioClip.audioClip.Length > 1 ? Random.Range(0, soundAudioClip.audioClip.Length) : 0;
                getVolume = soundAudioClip.volume;
                return soundAudioClip.audioClip[audioClipNumber];
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
