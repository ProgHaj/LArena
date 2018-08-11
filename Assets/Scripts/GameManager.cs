using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++){
			GameObject player = Instantiate(playerPrefab, transform.position, transform.rotation);
			Player playerScript = player.GetComponent<Player>();
			playerScript.Color = new Color(Random.value, Random.value, Random.value, 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
