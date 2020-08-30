using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioClip[] gameSound;
    public AudioClip[] fx;

    public void playFx(int idFx)
    {
        audioSource.PlayOneShot(fx[idFx]);
    }

    public float getAudioSourceVol()
    {
        return audioSource.volume;
    }

    public void setAudioSourceVol(float newVol)
    {
        audioSource.volume = newVol;
    }
}
