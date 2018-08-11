using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Aim : MonoBehaviour {

	float aimSpeed = 2f;
    float direction = 1f;
    float currentAngle = 50f;
    float maxAngle = 90f;
    float minAngle = -70f;

	float distance;
	float defaultDistance = 1f;
	float maxDistance = 2f;
    float holdingSpeed = 3f;
    bool isFiring = false;

    float bulletSpeed = 3f;
    float cooldown = 0.3f;
    float timestampCooldown;

    Transform parent;
    public GameObject bulletPrefab;

	void Start () {
		parent = this.transform.parent;
        timestampCooldown = -cooldown;
        distance = defaultDistance;
        transform.localPosition = CalculatePosition(currentAngle, distance);
	}
	
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
        Vector3 spawnPosition = parent.position + transform.localPosition.normalized * defaultDistance / 2f;
        Vector3 velocity = transform.localPosition * bulletSpeed; 
		GameObject bullet = Instantiate(bulletPrefab, spawnPosition, parent.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null) {
            bulletRb.velocity = velocity;
        }
        distance = defaultDistance;

        SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = this.GetComponent<SpriteRenderer>().color;
        }
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