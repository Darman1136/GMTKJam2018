using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSpike : MonoBehaviour {
    [SerializeField]
    private float speed = 120f;

    void Update() {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
