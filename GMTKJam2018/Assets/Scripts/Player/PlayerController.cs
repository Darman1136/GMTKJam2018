using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private PlayerSoundController psc;
    private Gun gun;
    private Animator animator;

    [SerializeField]
    private GameObject handLeft;
    [SerializeField]
    private GameObject handRight;

    private float handSwitchCooldown = .5f;
    private float lastHandSwitch = 1f;

    [SerializeField]
    private GameObject psBlood;
    [SerializeField]
    private Text deathText;
    private bool alive;

    private RocketLauncher rl;
    private M4 m4;

    private int currentHand = 0;

    private float lastDeathTime = 999f;
    private float lastDeathDelay = 0.1f;

    void Awake() {
        rl = FindObjectOfType<RocketLauncher>();
        m4 = FindObjectOfType<M4>();
        rb = GetComponent<Rigidbody2D>();
        psc = GetComponent<PlayerSoundController>();
        animator = GetComponent<Animator>();
        PickUpGun(m4);

        SetToSpawnPoint();
    }

    void Start() {
        UpdateUI();
        alive = true;
    }

    void Update() {
        if(Input.GetButtonUp("Cancel")) {
            SceneManager.LoadScene("MainMenu");
        }

        bool m1Down = Input.GetButton("Fire1");
        if (m1Down && gun) {
            gun.Fire();
        }

        UpdateRBGravity(m1Down);
        UpdateGunPositionAndRotation();

        if (Input.GetButtonDown("Fire2")) {
            PickUpGun(rl);
            UpdateGunPositionAndRotation();
            rl.Fire();
            PickUpGun(m4);
        }

        animator.SetFloat("speed", rb.velocity.x);
        lastHandSwitch += Time.deltaTime;
        lastDeathTime += Time.deltaTime;
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
            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gun.transform.position) * Quaternion.Euler(0f, 0f, 90f);

            SpriteRenderer sr = gun.GetComponent<SpriteRenderer>();
            bool prevFlipY = sr.flipY;
            if (lastHandSwitch >= handSwitchCooldown) {
                sr.flipY = gun.transform.eulerAngles.z > 90 && gun.transform.eulerAngles.z < 270;
                if (prevFlipY != sr.flipY) {
                    if (sr.flipY) {
                        BindToLeft();
                        gun.MuzzlePoint.transform.localPosition = new Vector3(.848f, -.192f);
                    } else {
                        BindToRight();
                        gun.MuzzlePoint.transform.localPosition = new Vector3(.848f, .192f);
                    }
                    lastHandSwitch = 0;
                }
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
            gun.GetComponent<SpriteRenderer>().enabled = false;
            //Vector3 tmpPosition = gun.transform.position;
            //gun.transform.parent = null;
            //gun.GetComponent<Rigidbody2D>().simulated = true;
            //gun.transform.position = tmpPosition;
            //gun = null;
        }
    }

    private void BindGunToPlayer() {
        if (currentHand == 0) {
            BindToRight();
        } else {
            BindToLeft();
        }
        gun.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void BindToRight() {
        currentHand = 0;
        gun.transform.position = handRight.transform.position;
        gun.transform.parent = handRight.transform;
    }

    private void BindToLeft() {
        currentHand = 1;
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

    public void Death(GameObject killer) {
        if (alive && lastDeathTime >= lastDeathDelay) {
            alive = false;
            lastDeathTime = 0;
            PlayerData.Deaths++;
            psc.Splat();
            BloodSplatter(killer.transform);
            Reset();
            Camera.main.GetComponent<CameraController>().PlayerDeath();
            UpdateUI();
        }
    }

    private void UpdateUI() {
        deathText.text = "Deaths: " + PlayerData.Deaths;
    }

    private void Reset() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        SetToSpawnPoint();
        alive = true;
    }

    private void BloodSplatter(Transform t) {
        Instantiate(psBlood, t.position, psBlood.transform.rotation * Quaternion.Euler(-t.rotation.eulerAngles.z, 0, 0));
    }
}
