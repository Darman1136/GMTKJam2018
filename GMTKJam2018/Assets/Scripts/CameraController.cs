using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // default main.orthographicSize = 5
    private Camera main;

    void Awake() {
        main = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
}
