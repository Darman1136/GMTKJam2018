using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc) {
                pc.Death();
            }
        }
    }
}
