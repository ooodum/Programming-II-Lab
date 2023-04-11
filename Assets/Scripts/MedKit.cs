using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private Collectible medKit;
    private void Start() {
        medKit = new Collectible("MedKit", 10, Color.green);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            medKit.Heal();
            Destroy(gameObject);
        }
    }
}
