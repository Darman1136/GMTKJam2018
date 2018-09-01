using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableSpike : MonoBehaviour {
    [SerializeField]
    private float distanceX = 1f;
    [SerializeField]
    private float distanceY = 1f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float initialDelay = 0f;
    private float sinceStart = 0f;

    private Vector3 start;
    private Vector3 end;

    private Vector3 currentGoal;

    void Start() {
        start = transform.position;
        end = start + new Vector3(distanceX, distanceY);
        currentGoal = end;
    }

    void Update() {
        if (sinceStart < initialDelay) {
            sinceStart += Time.deltaTime;
            return;
        }

        Vector3 newPos = Vector3.Lerp(transform.position, currentGoal, speed * Time.deltaTime);
        if (Mathf.Abs(Vector3.Distance(currentGoal, newPos)) > 0.1) {
            transform.position = newPos;
        } else {
            if (currentGoal.Equals(end)) {
                currentGoal = start;
            } else {
                currentGoal = end;
            }
        }
    }
}
