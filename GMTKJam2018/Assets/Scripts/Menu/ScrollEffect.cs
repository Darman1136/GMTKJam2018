using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollEffect : MonoBehaviour {
    [SerializeField]
    private GameObject bg1;
    [SerializeField]
    private GameObject bg2;

    private Vector3 speed = new Vector3(1f, 0f);
    private float maxX = 19.9f;
    private float resetX = -19.98f;

    void Update() {
        Debug.Log(bg1.transform.position);
        bg1.transform.position += speed * Time.deltaTime;
        bg2.transform.position += speed * Time.deltaTime;

        if (bg1.transform.position.x >= maxX) {
            Vector3 pos = bg1.transform.position;
            pos.x = resetX;
            bg1.transform.position = pos;
        }
        if (bg2.transform.position.x >= maxX) {
            Vector3 pos = bg2.transform.position;
            pos.x = resetX;
            bg2.transform.position = pos;
        }
    }
}
