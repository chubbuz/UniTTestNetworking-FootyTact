using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Linker : NetworkBehaviour {


	public GameObject inst;
	public Transform state;


	// Use this for initialization
	void Start () {
		print ("linker started");

//		print ("setting controller");
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
//		print ("setting on component-SessionManager");
		controller.GetComponent<SessionManager> ().startTime=DateTime.Now.Second;


//		print ("enabling counting From linker");



			controller.GetComponent<SessionManager> ().toStartCount = true;
//		print ("counting Enabled");

		//enabling respective player controls
		GameObject clientPlayer = GameObject.Find("ClientPlayer");
		GameObject serverPlayer = GameObject.Find ("ServerPlayer");


		if (isServer) {
			print ("I am a Server");

			
			clientPlayer.GetComponent<Movement> ().isMovementAllowed = false;
//			print ("Disabling Clients movements");
			serverPlayer.GetComponent<Movement>().isMovementAllowed = true;
		} else {

			print ("I am a client");
			clientPlayer.GetComponent<Movement> ().isMovementAllowed = true;
//			print ("disabling server movements");
			serverPlayer.GetComponent<Movement>().isMovementAllowed = false;
		}
	
//		print ("Server or Client has been initialized");
	}


	public 	void Send()
	{
		print ("Invoking Command");
		GameObject clientPlayer = GameObject.Find("ClientPlayer");
		GameObject serverPlayer = GameObject.Find ("ServerPlayer");

		state = clientPlayer.GetComponent<Movement> ().State;

		if(isClient)
			CmdEngine (state.transform.position,state.transform.rotation);

		print ("Invoking attempt completed");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdEngine(Vector3 x,Quaternion y){
		if (isServer)
			print ("Inside the ServerCommand");
		print (">>>>>>>>>>>Client invoked Command fucntion<<<<<<<");
		print ("The state of client is:" + x);

		inst.transform.SetPositionAndRotation (x,y);
		Instantiate (inst);


	}
}
