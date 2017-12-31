using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_PlaySource : MonoBehaviour {

    public void PlayAudioSource()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
