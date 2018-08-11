using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkOverTime : MonoBehaviour {

    bool isShrinking = false;
    Transform trans;
    float speed = 0.01f;
    float minSize = 0.1f;
    float intervall = 0.2f;
    public bool active = false;
    public IEnumerator coroutine { get; set; }

	void Start () {
        trans = this.GetComponent<Transform>();
        InvokeRepeating("Shrink", 0f, intervall);
	}

    void Shrink() {
        if (!active || trans.localScale.x <= minSize) return;

        trans.localScale -= new Vector3(speed, 0, 0);
    }
}
