using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    public GameObject explosionPrefab;
    public float time = 3f;

	// Use this for initialization
	void Start () {
        Invoke("CreateExplosion", time);
	}
	
    void CreateExplosion () {
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
