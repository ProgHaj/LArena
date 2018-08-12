using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject playerUIPrefab;
	public List<GameObject> playerObjects = new List<GameObject>();
	public int players = 0;
	public static int playersAlive = 0;
	public static Dictionary<int, int> controllerId = new Dictionary<int, int>();
	public List<int> controllers = new List<int>();
	public bool isSettingUp = true;
	public bool isSpawningPlayers = false;
	public bool isPlayingGame = false;
	public Dictionary<int, Color> colors = new Dictionary<int, Color>();
	public Dictionary<int, int> score = new Dictionary<int, int>();
	public GameObject UIOverlay;
	public Dictionary<int, GameObject> UIElements = new Dictionary<int, GameObject>();

	void Start() {
		UIOverlay = GameObject.FindWithTag("UI");
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayingGame) {
			if (playersAlive <= 1) {
				WonRound();
			}
		} else if (isSettingUp) {
			SetupControllers();
		} else if (isSpawningPlayers) {
			SpawnPlayers();
		}
	}

	void SetupControllers() {
		for (int i = -1; i <= 16; i++) {
			ControllerCheck(i);
		}

		for (int i = 21; i <= 29; i++) {
			ControllerCheck(i);
		}

		for (int i = 210; i <= 216; i++) {
			ControllerCheck(i);
		}

		if (Input.GetButton(Inputs.StartButtonCID())) {
			if (players >= 1) {
				for (int i = 0; i < players; i++) {
					controllerId[i] = controllers[i];
				}

				playersAlive = players;
				isSettingUp = false;
				isSpawningPlayers = true;
			}
		}
	}

	void ControllerCheck(int i) { 
		// i = id of controller which is currntly checked.
		if (Input.GetButton(Inputs.AButtonCID(i)) && !controllers.Contains(i)) {
			controllers.Add(i);
			colors[players] = new Color(Random.value, Random.value, Random.value);
			score[players] = 0;
			CreatePortrait(players);
			players += 1;
		}
		/* 
		if (Input.GetButton(Inputs.BButtonCID(i)) && controllers.Contains(i)) {
			controllers.Remove(i);
			players -= 1;
		}*/
	}

	void CreatePortrait(int playerId) {
			Transform UITrans = UIOverlay.GetComponent<Transform>();
			GameObject playerPortrait = Instantiate(playerUIPrefab, UITrans.position, UITrans.rotation);
			Transform playerPortraitT = playerPortrait.GetComponent<Transform>();
			RectTransform rectTransform = playerPortraitT.GetComponent<RectTransform>();

			playerPortraitT.SetParent(UITrans);

			rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
			rectTransform.anchoredPosition = new Vector3(50 + 70 * players, -30f, 0);

			UIElements[playerId] = playerPortrait;
			UpdatePortrait(playerId);
	}

	void UpdatePortrait(int playerId) {
			Transform playerPortraitT = UIElements[playerId].GetComponent<Transform>();
			Transform scoreTextGO = playerPortraitT.Find("Score");
			Transform playerTextGO = playerPortraitT.Find("PlayerText");
			Transform playerColorGO = playerPortraitT.Find("Image");
			Text scoreText = scoreTextGO.GetComponent<Text>();
			Text playerText = playerTextGO.GetComponent<Text>();
			Image playerColor = playerColorGO.GetComponent<Image>();

			scoreText.text = "" + score[playerId];
			playerText.text = "Player " + playerId;
			playerColor.color = colors[playerId];
	}

	void SpawnPlayers () {
		for (int i = 0; i < players; i++) {
			GameObject player = Instantiate(playerPrefab, transform.position, transform.rotation);
			Player playerScript = player.GetComponent<Player>();
			playerScript.PlayerId = i;
			playerScript.Color = colors[i];
			playerObjects.Add(player);
		}

		GameObject ground = GameObject.FindWithTag("Ground");
		if (ground != null) {
			ShrinkOverTime script = ground.GetComponent<ShrinkOverTime>();
			if (script != null) {
				script.active = true;
			}
		}

		isSpawningPlayers = false;
		isPlayingGame = true;
	}

	void ResetGame() {
		GameObject ground = GameObject.FindWithTag("Ground");
		if (ground != null) {
			ShrinkOverTime script = ground.GetComponent<ShrinkOverTime>();
			if (script != null) {
				script.active = false;
			}

			Transform gTrans = ground.GetComponent<Transform>();
			if(gTrans != null) {
				gTrans.localScale = new Vector3(3, 1, 1);
			}
		}

		foreach(GameObject player in playerObjects) {
			if (player != null) {
				Destroy(player.gameObject);
			}
		}

		foreach(KeyValuePair<int, GameObject> element in UIElements) {
			if (element.Value != null) {
				Destroy(element.Value.gameObject);
			}
		}

		playersAlive = 0;
		playerObjects = new List<GameObject>();
		UIElements = new Dictionary<int, GameObject>();
	}

	void SoftResetGame () {
		ResetGame();
		playersAlive = players;
		SpawnPlayers();
	}

	void HardResetGame () {
		ResetGame();
		players = 0;
		controllerId = new Dictionary<int, int>();
		controllers = new List<int>();
		isSettingUp = true;
		isSpawningPlayers = false;
		isPlayingGame = false;
		colors = new Dictionary<int, Color>();
		score = new Dictionary<int, int>();
	}


	void WonRound() {
		isPlayingGame = false;
		int winningPlayer = -1;
		foreach(GameObject player in playerObjects) {
			if (player != null) {
				Player playerScript = player.GetComponent<Player>();
				winningPlayer = playerScript.PlayerId;
			}
		}

		if (winningPlayer == -1) {
			// draw
		} else {
			score[winningPlayer] += 1;
			UpdatePortrait(winningPlayer);
		}

		if (score[winningPlayer] >= 3) {
			Victory(winningPlayer);
		} else {
			Invoke("SoftResetGame", 3f);
		}
	}

	void Victory(int winner) {
		Debug.Log("Player " + winner + " has won the game!");
		Invoke("HardResetGame", 5f);
	}
}
