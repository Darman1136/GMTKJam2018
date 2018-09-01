using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public AudioClip acShot;

    protected AudioSource asShot;
    protected float timeSinceLastShot = 999f;

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

    private float pitchRange = 0f;
    public float PitchRange {
        get {
            return pitchRange;
        }

        set {
            pitchRange = value;
        }
    }

    private float defaultPitch = 1f;
    public float DefaultPitch {
        get {
            return defaultPitch;
        }

        set {
            defaultPitch = value;
        }
    }

    [SerializeField]
    private GameObject bullet;
    public GameObject Bullet {
        get {
            return bullet;
        }

        set {
            bullet = value;
        }
    }

    [SerializeField]
    private GameObject muzzlePoint;
    public GameObject MuzzlePoint {
        get {
            return muzzlePoint;
        }

        set {
            muzzlePoint = value;
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
            asShot.pitch = defaultPitch + UnityEngine.Random.Range(-pitchRange, pitchRange);
            asShot.Play();
            AddForceToUser();
            SpawnProjectile();
            timeSinceLastShot = 0f;
        }
    }

    protected void SpawnProjectile() {
        if (bullet) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            GameObject b = Instantiate(bullet, MuzzlePoint.transform.position, newRotation);
            if (b) {
                Vector2 mousePos2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 playerPos = User.transform.position;
                var heading = mousePos2d - playerPos;
                var distance = heading.magnitude;
                var normDirection = heading / distance;

                b.GetComponent<Rigidbody2D>().AddForce(normDirection * 30, ForceMode2D.Impulse);
            }
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
