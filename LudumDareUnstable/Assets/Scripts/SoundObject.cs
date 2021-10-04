using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundObject : MonoBehaviour
{
    #region Singleton

    private static SoundObject _instance;
    public static SoundObject Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
        SoundManager.Initialize();
    }

    #endregion

    public SoundAudioClip[] soundAudioClipArray;
    public float stepSoundSpeed;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        [Range(0, 1)] public float volume;
        public AudioClip[] audioClip;
    }
}
