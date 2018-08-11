using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Explode : MonoBehaviour {
    public GameObject explosionPrefab;
    public float time = 3f;
    public Color color { get; set; }

	// Use this for initialization
	void Start () {
        Invoke("CreateExplosion", time);
	}
	
    void CreateExplosion () {
        GameObject explosion = Instantiate (explosionPrefab, transform.position, transform.rotation);
        SpriteRenderer sprite = explosion.GetComponent<SpriteRenderer>();
        if (sprite != null && this.color != null) {
            sprite.color = this.GetComponent<SpriteRenderer>().color;
        }

        Destroy(this.gameObject);
    }
}
