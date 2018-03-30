using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Linker : NetworkBehaviour {


	public GameObject inst;
 


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


		if (isServer) {
			print ("I am a Server");
			clientPlayer0.GetComponent<Movement> ().isMovementAllowed = false;
			clientPlayer1.GetComponent<Movement> ().isMovementAllowed = false;
			clientPlayer2.GetComponent<Movement> ().isMovementAllowed = false;
			clientPlayer3.GetComponent<Movement> ().isMovementAllowed = false;
			clientPlayer4.GetComponent<Movement> ().isMovementAllowed = false;


			serverPlayer0.GetComponent<Movement> ().isMovementAllowed = true;
			serverPlayer1.GetComponent<Movement> ().isMovementAllowed = true;
			serverPlayer2.GetComponent<Movement> ().isMovementAllowed = true;
			serverPlayer3.GetComponent<Movement> ().isMovementAllowed = true;
			serverPlayer4.GetComponent<Movement> ().isMovementAllowed = true;



		} else {

			print ("I am a client");
			clientPlayer0.GetComponent<Movement> ().isMovementAllowed = true;
			clientPlayer1.GetComponent<Movement> ().isMovementAllowed = true;
			clientPlayer2.GetComponent<Movement> ().isMovementAllowed = true;
			clientPlayer3.GetComponent<Movement> ().isMovementAllowed = true;
			clientPlayer4.GetComponent<Movement> ().isMovementAllowed = true;


			serverPlayer0.GetComponent<Movement> ().isMovementAllowed = false;
			serverPlayer1.GetComponent<Movement> ().isMovementAllowed = false;
			serverPlayer2.GetComponent<Movement> ().isMovementAllowed = false;
			serverPlayer3.GetComponent<Movement> ().isMovementAllowed = false;
			serverPlayer4.GetComponent<Movement> ().isMovementAllowed = false;
		}
	
//		print ("Server or Client has been initialized");
	}


	public 	void Send()
	{
		print ("Invoking Command");
		GameObject clientPlayer0 = GameObject.Find("ClientPlayer0");
		GameObject clientPlayer1 = GameObject.Find("ClientPlayer1");
		GameObject clientPlayer2 = GameObject.Find("ClientPlayer2");
		GameObject clientPlayer3 = GameObject.Find("ClientPlayer3");
		GameObject clientPlayer4 = GameObject.Find("ClientPlayer4");


		GameObject serverPlayer = GameObject.Find ("ServerPlayer");

		Transform state0=clientPlayer0.GetComponent<Movement> ().State;
		Transform state1=clientPlayer1.GetComponent<Movement> ().State;
		Transform state2=clientPlayer2.GetComponent<Movement> ().State;
		Transform state3=clientPlayer3.GetComponent<Movement> ().State;
		Transform state4=clientPlayer4.GetComponent<Movement> ().State;
	

		if (isClient)
			CmdEngine (state0.transform.position, state0.transform.rotation,
				state1.transform.position, state1.transform.rotation,
				state2.transform.position, state2.transform.rotation,
				state3.transform.position, state3.transform.rotation,
				state4.transform.position, state4.transform.rotation
			
			);

		print ("Invoking attempt completed");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdEngine(Vector3 x0,Quaternion y0,
		Vector3 x1,Quaternion y1,
		Vector3 x2,Quaternion y2,
		Vector3 x3,Quaternion y3,
		Vector3 x4,Quaternion y4
	){
		if (isServer)
			print ("Inside the ServerCommand");
		print (">>>>>>>>>>>Client invoked Command fucntion<<<<<<<");
		//print ("The state of client is:" + x);

		inst.transform.SetPositionAndRotation (x0,y0);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (x1,y1);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (x2,y2);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (x3,y3);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (x4,y4);
		Instantiate (inst);


	}
}
