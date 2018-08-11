using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

	public float aimSpeed = 2f;
    float direction = 1f;
    float currentAngle = 50f;
    float maxAngle = 90f;
    float minAngle = -70f;

	float distance;
	float defaultDistance = 1f;
	float maxDistance = 2f;
    public float holdingSpeed = 1f;
    bool isFiring = false;

    public float bulletSpeed = 3f;
    public float cooldown = 0.2f;
    float timestampCooldown;

    Transform parent;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent;
        timestampCooldown = -cooldown;
        distance = defaultDistance;
        transform.localPosition = CalculatePosition(currentAngle, distance);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton("Fire1") && IsNotOnCooldown()) {
            if (isFiring) {
                HoldingFire();
            } else {
                distance = 0.5f;
                isFiring = true;
            }
		} else if (isFiring) {
			Fire();
            isFiring = false;
            timestampCooldown = Time.time;
        }

        SetAim();
        SetDirection();

        transform.localPosition = CalculatePosition (currentAngle, distance);
	}

    void HoldingFire() {
        if (distance < maxDistance) {
            distance += holdingSpeed * Time.deltaTime;
        }
    }

	void Fire() {
        Vector3 spawnPosition = parent.position + transform.localPosition / 2f;
        Vector3 velocity = transform.localPosition * bulletSpeed; 
		GameObject bullet = Instantiate(bulletPrefab, spawnPosition, parent.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = velocity;
        distance = defaultDistance;
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

    bool IsNotOnCooldown() {
        return timestampCooldown + cooldown < Time.time;
    }
}