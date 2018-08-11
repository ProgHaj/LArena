using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float movementSpeed = 1f;
	float distance = 5f;

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
		rb.velocity = new Vector2(x, rb.velocity.y + y);
	}

	bool CanJump() {
		return true;
	}

	float Jump() {
		return distance;
	}
}
