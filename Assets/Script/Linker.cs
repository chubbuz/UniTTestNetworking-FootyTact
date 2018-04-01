using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Linker : NetworkBehaviour {
	
	// Use this for initialization
	void Start () {
		print ("linker started");

//		//print ("setting controller");
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
//		//print ("setting on component-SessionManager");
		controller.GetComponent<SessionManager> ().startTime=DateTime.Now.Second;
	//	controller.GetComponent<Execution>().amIServer=true;

//		//print ("enabling counting From linker");



			controller.GetComponent<SessionManager> ().toStartCount = true;
//		//print ("counting Enabled");

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
			//Server Sever Server
			//print ("I am a Server");
			clientPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			clientPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			clientPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			clientPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			clientPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;


			serverPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			serverPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			serverPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			serverPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			serverPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;

			//controller.GetComponent<Execution>().amIServer=true;


		} else {
			//Client Client Client
			//print ("I am a client");
			clientPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			clientPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			clientPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			clientPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
			clientPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = true;


			serverPlayer0.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			serverPlayer1.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			serverPlayer2.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			serverPlayer3.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
			serverPlayer4.GetComponent<PlayerBehaviour> ().isMovementAllowed = false;

			//controller.GetComponent<Execution>().amIServer=false;
		}
	
//		//print ("Server or Client has been initialized");
	}


	public 	void Send()
	{
		//print ("Invoking Command");
		GameObject ball = GameObject.Find ("Ball");
		Vector3 ballPos = Vector3.zero;



		if (isClient) {
			GameObject clientPlayer0 = GameObject.Find("ClientPlayer0");
			GameObject clientPlayer1 = GameObject.Find("ClientPlayer1");
			GameObject clientPlayer2 = GameObject.Find("ClientPlayer2");
			GameObject clientPlayer3 = GameObject.Find("ClientPlayer3");
			GameObject clientPlayer4 = GameObject.Find("ClientPlayer4");

			Transform state0=clientPlayer0.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state1=clientPlayer1.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state2=clientPlayer2.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state3=clientPlayer3.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state4=clientPlayer4.GetComponent<PlayerBehaviour>().attemptedState;


			if (!ball.GetComponent<BallBehaviour> ().isBallWithServer) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
			}


			CmdSendToEngine (state0.transform.position, state0.transform.rotation,
				state1.transform.position, state1.transform.rotation,
				state2.transform.position, state2.transform.rotation,
				state3.transform.position, state3.transform.rotation,
				state4.transform.position, state4.transform.rotation,
				ballPos
			
			);
		}
		else {
			//Server to ServerEngine

			GameObject serverPlayer0 = GameObject.Find ("ServerPlayer0");
			GameObject serverPlayer1 = GameObject.Find ("ServerPlayer1");
			GameObject serverPlayer2 = GameObject.Find ("ServerPlayer2");
			GameObject serverPlayer3 = GameObject.Find ("ServerPlayer3");
			GameObject serverPlayer4 = GameObject.Find ("ServerPlayer4");



			Transform state0=serverPlayer0.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state1=serverPlayer1.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state2=serverPlayer2.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state3=serverPlayer3.GetComponent<PlayerBehaviour>().attemptedState;
			Transform state4=serverPlayer4.GetComponent<PlayerBehaviour>().attemptedState;

			if (ball.GetComponent<BallBehaviour> ().isBallWithServer) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
			}


			GameObject Engine = GameObject.FindWithTag ("GameController");
			Engine.GetComponent<Engine> ().ServerPosition (state0.transform.position, state0.transform.rotation,
				state1.transform.position, state1.transform.rotation,
				state2.transform.position, state2.transform.rotation,
				state3.transform.position, state3.transform.rotation,
				state4.transform.position, state4.transform.rotation,
				ballPos

			);

		}


		//print ("Invoking attempt completed");

	}

	[Command]
	public void CmdSendToEngine(Vector3 x0,Quaternion y0,
		Vector3 x1,Quaternion y1,
		Vector3 x2,Quaternion y2,
		Vector3 x3,Quaternion y3,
		Vector3 x4,Quaternion y4,
		Vector3 bPos
	){
		if (isServer) {
			//print ("Inside the ServerCommand");
			//print (">>>>>>>>>>>Client invoked Command fucntion<<<<<<<");
			////print ("The state of client is:" + x);

		}


		GameObject Engine = GameObject.FindWithTag ("GameController");
		Engine.GetComponent<Engine> ().ClientPosition (x0,y0,x1,y1,x2,y2,x3,y3,x4,y4,bPos);

	}


	public void RecieveEngineOutput(Vector3[] serverUpdate , Vector3[] clientUpdate){
		RpcClientEngineOutput (serverUpdate,clientUpdate);
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");	
		controller.GetComponent<Execution> ().Execute (serverUpdate,clientUpdate);
	}
		

	[ClientRpc]
	public void RpcClientEngineOutput(Vector3[] serverUpdate ,Vector3[] clientUpdate){
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");	
		controller.GetComponent<Execution> ().Execute (serverUpdate, clientUpdate);
	}
}
