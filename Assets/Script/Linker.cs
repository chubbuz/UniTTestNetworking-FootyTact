using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Linker : NetworkBehaviour {
	
	// Use this for initialization
	private GameObject[] server;
	private GameObject[] client;
	private Transform[] state;


	void Start () {
//		print ("linker started");
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		controller.GetComponent<SessionManager> ().toStartCount = true;

		//enabling respective player controls
		server = new GameObject[5];
		client = new GameObject[5];
		state = new Transform[5];


		for (int i = 0; i < 5; i++) {
			server [i] = GameObject.Find ("ServerPlayer" + i);
			client [i] = GameObject.Find ("ClientPlayer" + i);

			if (isServer) {
				//Server Sever Server
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
				controller.GetComponent<SessionManager> ().oppTeam = "ClientPlayer";
				controller.GetComponent<SessionManager> ().amIServer = true;

			} else {
				//Client Client Client
				//print ("I am a client");
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				controller.GetComponent<SessionManager> ().oppTeam = "ServerPlayer";
				controller.GetComponent<SessionManager> ().amIServer = false;

			}
		}
	}


	public 	void Send()
	{
		//print ("Invoking Command");
		GameObject ball = GameObject.Find ("Ball");
		Vector3 ballPos = Vector3.zero;



		if (isClient) {
			for (int i = 0; i < 5; i++) {
				state [i]= client[i].GetComponent<PlayerBehaviour>().attemptedState;
			}

			if (!ball.GetComponent<BallBehaviour> ().isBallWithServer) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
			}


			CmdSendToEngine (state[0].transform.position, state[0].transform.rotation,
				state[1].transform.position, state[1].transform.rotation,
				state[2].transform.position, state[2].transform.rotation,
				state[3].transform.position, state[3].transform.rotation,
				state[4].transform.position, state[4].transform.rotation,
				ballPos
			
			);
		}
		else {
			//Server to ServerEngine
			for (int i = 0; i < 5; i++) {
				state [i] = server[i].GetComponent<PlayerBehaviour>().attemptedState;
			}

			if (ball.GetComponent<BallBehaviour> ().isBallWithServer) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
			}


			GameObject Engine = GameObject.FindWithTag ("GameController");
			Engine.GetComponent<Engine> ().ServerPosition (state[0].transform.position, state[0].transform.rotation,
				state[1].transform.position, state[1].transform.rotation,
				state[2].transform.position, state[2].transform.rotation,
				state[3].transform.position, state[3].transform.rotation,
				state[4].transform.position, state[4].transform.rotation,
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


	//From Engine to the Linker ulitmately to the client
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
