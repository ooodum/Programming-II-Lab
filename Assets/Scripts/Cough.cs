using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : MonoBehaviour{
    private Vector3 direction;
    private float speed, area, lifetime;
   private void Start() {
        var mat = GetComponent<Renderer>().material.color;
        mat.a = 0;
        GetComponent<Renderer>().material.color = mat;
    }

    /// <summary>
    /// Initiates a cough
    /// </summary>
    /// <param name="dir">The direction of the cough</param>
    /// <param name="speed">The speed of the cough</param>
    /// <param name="area">The area of the cough</param>
    /// <param name="lifetime">The lifetime of the cough in seconds</param>
    public void StartCough(Vector3 dir, float speed, float area, float lifetime) {
        direction = dir.normalized;
        this.speed = speed;
        this.area = area;
        this.lifetime = lifetime;
    }

    private void Update() {
        transform.localScale = Vector3.one * area;
        transform.position += direction * speed * Time.deltaTime;

        Destroy(gameObject, lifetime);
    }
}
