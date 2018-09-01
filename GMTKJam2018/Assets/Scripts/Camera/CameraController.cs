using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // default main.orthographicSize = 5
    private Camera main;

    private BoxCollider2D nextTileActivator;

    private CameraShake cs;

    private Vector3 startPosition;

    private float delayLastTileChange = 2f;
    private float timeSinceLastTileChange = 999f;

    void Awake() {
        main = Camera.main;
        nextTileActivator = GetComponentInChildren<BoxCollider2D>();
        cs = GetComponent<CameraShake>();
        startPosition = main.transform.position;
    }

    void Update() {
        timeSinceLastTileChange += Time.deltaTime;
    }

    internal void PreviousTile() {
        if (timeSinceLastTileChange > delayLastTileChange) {
            float width = TileChange();
            main.transform.position = main.transform.position - new Vector3((width / 1.1f), 0f, 0f);
            cs.OriginalPos = main.transform.position;
        }
    }

    private float TileChange() {
        float height = 2f * main.orthographicSize;
        float width = height * main.aspect;
        timeSinceLastTileChange = 0f;
        return width;
    }

    internal void NextTile() {
        if (timeSinceLastTileChange > delayLastTileChange) {
            float width = TileChange();
            main.transform.position = main.transform.position + new Vector3((width / 1.1f), 0f, 0f);
            cs.OriginalPos = main.transform.position;
        }
    }

    public void PlayerDeath() {
        main.transform.position = startPosition;
        Shake();
    }

    public void Shake() {
        cs.Shake(.1f);
    }
}
