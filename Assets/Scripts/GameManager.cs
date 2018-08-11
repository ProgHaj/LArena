using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public List<GameObject> playerObjects = new List<GameObject>();
	public int players = 0;
	public static int playersAlive = 0;
	public static Dictionary<int, int> controllerId = new Dictionary<int, int>();
	public List<int> controllers = new List<int>();
	public bool isPlayingGame = false;
	public bool isSettingUp = true;

	
	// Update is called once per frame
	void Update () {
		if (isSettingUp) {
			SetupControllers();
		} else if (!isPlayingGame) {
			SpawnPlayers();
		} else if (isPlayingGame) {
			//CheckWinConditions();
			if (playersAlive <= 1) {
				Debug.Log("Victory!");
			}
		}
	}

	void SetupControllers() {
		for (int i = -1; i <= 29; i++) {
			if (i >= 10 && i <= 20) continue;

			if (Input.GetButton(Inputs.AButtonCID(i)) && !controllers.Contains(i)) {
				controllers.Add(i);
				players += 1;                                                                     
			}

			if (Input.GetButton(Inputs.BButtonCID(i)) && controllers.Contains(i)) {
				controllers.Remove(i);
				players -= 1;
			}
		}

		if (Input.GetButton(Inputs.StartButtonCID())) {
			if (players >= 1) {
				for (int i = 0; i < players; i++) {
					controllerId[i] = controllers[i];
				}

				playersAlive = players;
				isSettingUp = false;
			}
		}
	}

	void SpawnPlayers () {
		for (int i = 0; i < players; i++) {
			GameObject player = Instantiate(playerPrefab, transform.position, transform.rotation);
			Player playerScript = player.GetComponent<Player>();
			playerScript.PlayerId = i;
			playerScript.Color = new Color(Random.value, Random.value, Random.value, 1f);
			playerObjects.Add(player);
		}


		isPlayingGame = true;
		GameObject ground = GameObject.FindWithTag("Ground");
		if (ground != null) {
			ShrinkOverTime script = ground.GetComponent<ShrinkOverTime>();
			if (script != null) {
				script.enabled = true;
			}
		}
	}
}
