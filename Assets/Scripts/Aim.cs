using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

	float aimSpeed = 1f;
	float distance = 1f;
    float currentAngle = 50f;
    float bulletSpeed = 3f;
    float maxAngle = 80f;
    float minAngle = -80f;
    float direction = 1f;
    Transform parent;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent;
        transform.localPosition = CalculatePosition(currentAngle, distance);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}

        SetAim();
        SetDirection();

        transform.localPosition = CalculatePosition (currentAngle, distance);
	}

	void Shoot() {
        Vector3 spawnPosition = parent.position + transform.localPosition / 2f;
        Vector3 velocity = transform.localPosition.normalized * bulletSpeed; 
		GameObject bullet = Instantiate(bulletPrefab, spawnPosition, parent.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = velocity;
	}

    void SetAim() {
        currentAngle %= 180;

		float change = Input.GetAxisRaw("Vertical1") * aimSpeed;
        if (Mathf.Abs(change) <= 0.2f) return;
        currentAngle += change;

        if (currentAngle > maxAngle || currentAngle < minAngle) {
            currentAngle -= change;
            return;
        }
    }

    void SetDirection() {
        float faceDirection = Input.GetAxisRaw("Horizontal1");
        if (Mathf.Abs(faceDirection) > 0.8f) {
            direction = faceDirection/Mathf.Abs(faceDirection);
        }
    }

    Vector2 CalculatePosition(float angle, float distance) {
        return new Vector2(Mathf.Cos(angle * Mathf.PI/180f) * direction, Mathf.Sin(angle * Mathf.PI/180f)) * distance;
    }
}