using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float movementSpeed = 2f;
	float maxSpeed;
	float jumpForce = 5f;
	float recoverySpeed = 5f; // Speed of getting back normal speed when over the max speed.
	private Color color;

    public Color Color { 
		get {
			return color;
		}
		set {
			if (value != null) {
				SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
				foreach(SpriteRenderer sprite in sprites) {
					sprite.color = value;
				}

				SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
				if (thisSprite != null) {
					thisSprite.color = value;
				}
			}

			color = value;
		}
	}

	private int playerId;

	public int PlayerId {
		get {
			return playerId;
		}
		set {
			IPlayerId[] scriptsWithId = GetComponentsInChildren<IPlayerId>();
			foreach (IPlayerId script in scriptsWithId) {
				script.playerId = value;
			}

			playerId = value;	
		}
	}

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		maxSpeed = movementSpeed;
        Color = this.color;
	}
	
	void FixedUpdate () {
		float y, x;
		y = x = 0;

		if (Inputs.AButton(playerId) && CanJump() && rb.velocity.y <= 0f) {
			y = Jump();
		}

		x = Inputs.Horizontal(playerId) * movementSpeed;

		//Handles extra applied force, outside walking speed
		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
			float extraX = rb.velocity.x;
			if (extraX < 0) x = Mathf.Max(extraX, extraX + x * recoverySpeed * Time.deltaTime);
			if (extraX > 0) x = Mathf.Min(extraX, extraX + x * recoverySpeed * Time.deltaTime);
		}

		rb.velocity = new Vector2(x, rb.velocity.y + y);
	}

	bool CanJump() {
		int layerMaskIndex = LayerMask.NameToLayer("Ground");
		int layerMaskIndex2 = LayerMask.NameToLayer("Player");
		int layerMask = (1 << layerMaskIndex) + (1 << layerMaskIndex2);
		Vector3 groundCheck = new Vector3(0, -0.3f, 0);
		Vector3 underPlayer = new Vector3(0, -0.27f, 0);
		bool canJump = Physics2D.Linecast(transform.position + underPlayer, transform.position + groundCheck, layerMask);
		Debug.Log(canJump);
		return canJump;
	}

	float Jump() {
		return jumpForce;
	}

	public void TriggerDeath() {
		GameManager.playersAlive -= 1;
		Destroy(this.gameObject);
	}
}
