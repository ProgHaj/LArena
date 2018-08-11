using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float movementSpeed = 1f;
	float maxSpeed = 1f;
	float distance = 5f;
	float recoverySpeed = 10f; // Speed of getting back normal speed when over the max speed.

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float y, x;
		y = x = 0;

		if (Input.GetButtonDown("Jump1") && CanJump()) {
			y = Jump();
		}

		x = Input.GetAxisRaw("Horizontal1") * movementSpeed;

		//Handles extra applied force, outside walking speed
		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
			float extraX = rb.velocity.x;
			if (extraX < 0) x = Mathf.Max(extraX, extraX + x * recoverySpeed * Time.deltaTime);
			if (extraX > 0) x = Mathf.Min(extraX, extraX + x * recoverySpeed * Time.deltaTime);
			Debug.Log(x);
		}

		rb.velocity = new Vector2(x, rb.velocity.y + y);
	}

	bool CanJump() {
		int layerMaskIndex = LayerMask.NameToLayer("Ground");
		int layerMask = 1 << layerMaskIndex;

		return true;
	}

	float Jump() {
		return distance;
	}
}
