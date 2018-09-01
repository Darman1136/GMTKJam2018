using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPreviousTileCollider : MonoBehaviour {

    private CameraController cc;

    void Awake() {
        cc = GetComponentInParent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            cc.PreviousTile();
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            cc.PreviousTile();
        }
    }
}
