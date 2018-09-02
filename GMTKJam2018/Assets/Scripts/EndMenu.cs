using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {
    [SerializeField]
    private Text t;

    void Start() {
        t.text = "You died " + PlayerData.Deaths + " times! Nice!";
    }
}
