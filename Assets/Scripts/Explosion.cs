using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float time = 2f;
    public float growthSpeed = 1f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, time);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.localScale += new Vector3(1, 1, 0) * growthSpeed * Time.deltaTime;
	}

}
