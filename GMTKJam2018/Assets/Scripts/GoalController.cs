using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer doorOpen;
    [SerializeField]
    private SpriteRenderer doorClosed;

    public AudioClip acOpen;
    public AudioClip acClose;

    private AudioSource asOpen;
    private AudioSource asClose;

    void Awake() {
        asOpen = AddAudioSource(acOpen, false, false, 0.7f);
        asClose = AddAudioSource(acClose, false, false, 0.7f);
    }

    public void Open() {
        asOpen.Play();
        doorOpen.enabled = true;
        doorClosed.enabled = false;
    }

    public void Close() {
        asClose.Play();
        doorClosed.enabled = true;
        doorOpen.enabled = false;
    }

    private AudioSource AddAudioSource(AudioClip ac, bool loop, bool playOnAwake, float volume) {
        AudioSource newAS = gameObject.AddComponent<AudioSource>();
        newAS.clip = ac;
        newAS.loop = loop;
        newAS.playOnAwake = playOnAwake;
        newAS.volume = volume;
        return newAS;
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Open();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Close();
        }
    }
}
