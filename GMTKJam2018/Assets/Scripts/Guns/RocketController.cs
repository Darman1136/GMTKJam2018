using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {
    [SerializeField]
    private GameObject explosion;

    private bool once = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (once && collision.gameObject.CompareTag("Ground")) {
            once = false;

            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPos = player.transform.position;
            var heading = playerPos - mousePos;
            var distance = heading.magnitude;
            var normDirection = heading / distance;
            player.GetComponent<Rigidbody2D>().AddForceAtPosition(normDirection * 10, collision.transform.position, ForceMode2D.Impulse);

            if (explosion) {
                //Vector3 p = collision.gameObject.GetComponent<BoxCollider2D>().ClosestPointOnBounds(transform.position);
                Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
