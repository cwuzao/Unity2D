using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public static AudioManager _instance;
    public static AudioManager Instance
    {
        get {
            return _instance;
        }
    }
    public AudioSource bgmAudioSource;
    public AudioClip seaWaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;
    public AudioClip fireClip;
    public AudioClip changeClip;
    public AudioClip lvUpClip;
    //public AudioClip dieClip;
    private bool isMute;

    public bool IsMute
    {
        get {
            return isMute;
        }
    }

    void Awake() {
        _instance = this;
        isMute = !(PlayerPrefs.GetInt("mute", 0) == 0);
        DOMute();
    }
    private void Start()
    {
    }

    public void SwitchMuteState(bool isON)
    {
        isMute = !isON;
        DOMute();
    }

    public void PlayEffectSound(AudioClip clip)
    {
        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip,new Vector3(0,0,-5));
           }
    }

    void DOMute()
    {
        if (isMute)
        {
            bgmAudioSource.Pause();
        }
        else
        {
            bgmAudioSource.Play();
        }
    }


}
