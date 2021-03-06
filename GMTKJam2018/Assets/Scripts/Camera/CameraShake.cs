﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    private Transform camTransform;

    public float shakeDuration = 0f;

    public float shakeAmount = 0.3f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    public Vector3 OriginalPos {
        get {
            return originalPos;
        }

        set {
            originalPos = value;
        }
    }

    void Awake() {
        if (camTransform == null) {
            camTransform = Camera.main.GetComponent(typeof(Transform)) as Transform;
            OriginalPos = camTransform.position;
        }
    }

    void Update() {
        if (shakeDuration > 0) {
            camTransform.localPosition = new Vector3(0f, 0f, camTransform.localPosition.z) + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            camTransform.localPosition = new Vector3(0f, 0f, camTransform.localPosition.z);
        }
    }
    public void Shake(float duration) {
        OriginalPos = camTransform.localPosition;
        shakeDuration = duration;
    }
}