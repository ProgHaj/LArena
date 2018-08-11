using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeathOnTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.GetComponent<Player>();

		if (player != null) {
			player.TriggerDeath();
		}
    }


}
