using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private PlayerSoundController psc;
    private Gun gun;

    [SerializeField]
    private GameObject handLeft;
    [SerializeField]
    private GameObject handRight;

    private float handSwitchCooldown = 1f;
    private float lastHandSwitch = 1f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        psc = GetComponent<PlayerSoundController>();
        PickUpGun(FindObjectOfType<M4>());

        SetToSpawnPoint();
    }

    void Start() {

    }

    void Update() {
        bool m1Down = Input.GetMouseButton(0);
        if (m1Down && gun) {
            gun.Fire();
        }

        UpdateRBGravity(m1Down);
        UpdateGunPositionAndRotation();

        if (Input.GetMouseButtonDown(1)) {
            UnbindGunFromPlayer();
        }

        lastHandSwitch += Time.deltaTime;
    }

    private void UpdateRBGravity(bool m1Down) {
        if (rb.velocity.y < 0 && !m1Down) {
            rb.gravityScale = 3;
        } else {
            rb.gravityScale = 1;
        }
    }

    private void UpdateGunPositionAndRotation() {
        if (gun) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gun.transform.position) * Quaternion.Euler(0f, 0f, 90f); ;

            // Debug.Log(gun.transform.rotation.eulerAngles);

            SpriteRenderer sr = gun.GetComponent<SpriteRenderer>();
            bool prevFlipY = sr.flipY;
            sr.flipY = gun.transform.eulerAngles.z > 90 && gun.transform.eulerAngles.z < 270;
            if (prevFlipY != sr.flipY) {
                Vector3 offset = new Vector3(2f, 0f, 0f);
                if (sr.flipY) {
                    BindToLeft();
                } else {
                    BindToRight();
                }
                lastHandSwitch = 0;
            }
        }
    }

    private void PickUpGun(Gun gun) {
        UnbindGunFromPlayer();
        if (gun) {
            this.gun = gun;
            gun.User = gameObject;
        }
        BindGunToPlayer();
    }

    private void UnbindGunFromPlayer() {
        if (gun) {
            Vector3 tmpPosition = gun.transform.position;
            gun.transform.parent = null;
            gun.GetComponent<Rigidbody2D>().simulated = true;
            gun.transform.position = tmpPosition;
            gun = null;
        }
    }

    private void BindGunToPlayer() {
        BindToRight();
    }

    private void BindToRight() {
        gun.transform.position = handRight.transform.position;
        gun.transform.parent = handRight.transform;
    }

    private void BindToLeft() {
        gun.transform.position = handLeft.transform.position;
        gun.transform.parent = handLeft.transform;
    }

    private void SetToSpawnPoint() {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
        if (spawn) {
            transform.position = spawn.transform.position;
        } else {
            Debug.LogError("No spawn found (Prefabs/Spawn)");
        }
    }

    public void Death() {
        Reset();
        Camera.main.GetComponent<CameraController>().PlayerDeath();
    }

    private void Reset() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        SetToSpawnPoint();
    }
}
