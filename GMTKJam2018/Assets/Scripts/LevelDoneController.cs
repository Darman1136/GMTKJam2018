using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoneController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Debug.Log("You reached the finish");
        }
    }
}
