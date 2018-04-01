using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SessionManager : MonoBehaviour {

	// Use this for initialization
	private bool hasSent = false;
	public int time = 8;
	public  int startTime;
	public string timeUI;
	public bool toStartCount;
	public bool isTimeUp;

	Text timeRem;
	void Start () {
		toStartCount = false;
		timeUI = "Game Hasn't Started Yet";
		isTimeUp = false;
	}
	
	// Update is called once per frame
	void Update(){


		//if time Ended	
		if (!hasSent && toStartCount) {
			if (isTimeUp) {

				GameObject clientPlayer0 = GameObject.Find("ClientPlayer0");
				GameObject clientPlayer1 = GameObject.Find("ClientPlayer1");
				GameObject clientPlayer2 = GameObject.Find("ClientPlayer2");
				GameObject clientPlayer3 = GameObject.Find("ClientPlayer3");
				GameObject clientPlayer4 = GameObject.Find("ClientPlayer4");

				GameObject serverPlayer0 = GameObject.Find ("ServerPlayer0");
				GameObject serverPlayer1 = GameObject.Find ("ServerPlayer1");
				GameObject serverPlayer2 = GameObject.Find ("ServerPlayer2");
				GameObject serverPlayer3 = GameObject.Find ("ServerPlayer3");
				GameObject serverPlayer4 = GameObject.Find ("ServerPlayer4");

			//	//print ("Time is Up");
				timeUI = "Time is Up";	
				toStartCount = false;
				hasSent = true;

//				//print ("deactivating server movements");
				clientPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				clientPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				clientPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				clientPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				clientPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;


				serverPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				serverPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				serverPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				serverPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				serverPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;

				//print ("Calling Send Function from SessionManager");
				GameObject linker = GameObject.FindWithTag("Linker");
				linker.GetComponent<Linker>().Send();

			} else {
				timeUI="Time Remaining:-" + (time - DateTime.Now.Second + startTime);
				if (DateTime.Now.Second - startTime > time) {
					isTimeUp = true;
				}
			}

		}
	}
}
