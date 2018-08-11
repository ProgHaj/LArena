using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkOverTime : MonoBehaviour {

    bool isShrinking = false;
    Transform trans;
    float speed = 0.01f;
    float minSize = 0.1f;
    float intervall = 0.2f;
    float startTime = 5f;

	// Use this for initialization
	IEnumerator Start () {
        trans = this.GetComponent<Transform>();
        yield return new WaitForSeconds(startTime);
        StartCoroutine(Shrink(intervall, minSize));
	}

    /* Shrinks every "intervall" untill "minSize" is reached */
    IEnumerator Shrink(float intervall, float minSize) {
        while (trans.localScale.x > minSize) {
            yield return new WaitForSeconds(intervall);
            trans.localScale -= new Vector3(speed, 0, 0);
        }
    }
}
