using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoneController : MonoBehaviour {
    [SerializeField]
    private string nextlevel;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            if (!string.IsNullOrEmpty(nextlevel)) {
                SceneManager.LoadScene(nextlevel);
            } else {
                Debug.LogError("No next level set");
            }
        }
    }
}
