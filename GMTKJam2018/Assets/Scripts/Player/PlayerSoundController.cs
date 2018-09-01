using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
    public AudioClip acShot;

    private AudioSource asShot;

    void Awake() {
        asShot = AddAudioSource(acShot, false, false, 0.3f);
    }

    public void PlayShotFired() {
        asShot.Play();
    }

    private AudioSource AddAudioSource(AudioClip ac, bool loop, bool playOnAwake, float volume) {
        AudioSource newAS = gameObject.AddComponent<AudioSource>();
        newAS.clip = ac;
        newAS.loop = loop;
        newAS.playOnAwake = playOnAwake;
        newAS.volume = volume;
        return newAS;
    }
}
