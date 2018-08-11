using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ForceField : MonoBehaviour {

    public float force = 1f;

    void OnCollisionEnter2D(Collision2D collision) {
        Collider2D coll = collision.collider;
        ApplyForce(coll);
    }

    void OnTriggerStay2D(Collider2D coll) {
        ApplyForce(coll);
    }

    void ApplyForce(Collider2D coll) {
        Transform collTrans = coll.GetComponent<Transform>();
        Rigidbody2D collRB =coll.GetComponent<Rigidbody2D>();

        if (collRB != null) {
            collRB.velocity += CalculateForce(collTrans);
        }
    }

    Vector2 CalculateForce(Transform other) {
        Vector2 distance = other.position - transform.position;
        return distance.normalized * force;
    }
}