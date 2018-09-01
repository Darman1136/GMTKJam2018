using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public AudioClip acShot;

    private AudioSource asShot;
    private float timeSinceLastShot = 999f;

    private float forceMultipler = 3;
    public float ForceMultipler {
        get {
            return forceMultipler;
        }

        set {
            forceMultipler = value;
        }
    }

    private float fireRate = 0.17f;
    public float FireRate {
        get {
            return fireRate;
        }

        set {
            fireRate = value;
        }
    }

    private GameObject user;
    public GameObject User {
        get {
            return user;
        }

        set {
            user = value;
        }
    }

    protected virtual void Awake() {
        asShot = AddAudioSource(acShot, false, false, 0.3f);
    }

    protected virtual void Update() {
        timeSinceLastShot += Time.deltaTime;
        
    }

    public virtual void Fire() {
        if (CanFire()) {
            asShot.Play();
            AddForceToUser();
            timeSinceLastShot = 0f;
        }
    }

    protected bool CanFire() {
        return (timeSinceLastShot >= fireRate);
    }

    protected void AddForceToUser() {
        Rigidbody2D rb = User.GetComponent<Rigidbody2D>();
        if (rb) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPos = User.transform.position;

            var heading = playerPos - mousePos;
            var distance = heading.magnitude;
            var normDirection = heading / distance;

            rb.AddForce(normDirection * ForceMultipler, ForceMode2D.Impulse);
        }
    }

    protected AudioSource AddAudioSource(AudioClip ac, bool loop, bool playOnAwake, float volume) {
        AudioSource newAS = gameObject.AddComponent<AudioSource>();
        newAS.clip = ac;
        newAS.loop = loop;
        newAS.playOnAwake = playOnAwake;
        newAS.volume = volume;
        return newAS;
    }
}
