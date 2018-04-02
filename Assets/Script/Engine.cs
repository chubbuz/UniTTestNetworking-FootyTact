using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	//for instantation
	public GameObject inst;
	public GameObject proposedBall;

	//Engine switching on and off  parameters
	private bool hasServerSent;
	private bool hasClientSent;
	private bool hasEngineStarted;

	//proposed tactics info
	private Vector3[] server;
	private Vector3[] client;
	//previous position
	private Vector3[] serverPrev;
	private Vector3[] clientPrev;

	//All the information of the game
	private Plane plane;
	private Vector3 playerSize;

	void Start(){
		hasEngineStarted = false;

		//init new Position array
		server = new Vector3[5];
		client = new Vector3[5];

		//init sarting pos of players
		serverPrev = new Vector3[5];
		clientPrev = new Vector3[5];
		for (int i = 0; i < 5; i++) {
			serverPrev[i] = GameObject.Find ("ServerPlayer" + i).transform.position;
			clientPrev [i] = GameObject.Find ("ClientPlayer"+i).transform.position;
		}
		//init size of player
		GameObject sample = GameObject.Find ("ServerPlayer0");
		playerSize = sample.GetComponent<Collider> ().bounds.size;


		//init ground plane
		GameObject ground = GameObject.Find ("Plane");
		Vector3 groundPos = ground.transform.position;
		plane = new Plane(Vector3.up,groundPos);


	}

	public void ClientPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos

	){

//		print ("ClientPosition Recieved in Engine");
		hasClientSent = true;

		client[0]=cPos0;
		client[1]=cPos1;
		client[2]=cPos2;
		client[3]=cPos3;
		client[4]=cPos4;
	}


	public void ServerPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos
	){

//		print ("ServerPosition Recieved in Engine");
		hasServerSent = true;

		server[0]=cPos0;
		server[1]=cPos1;
		server[2]=cPos2;
		server[3]=cPos3;
		server[4]=cPos4;
//
	}




	void Update(){
		if (hasServerSent && hasClientSent && !hasEngineStarted) {
			hasEngineStarted = true;






			GameObject linker = GameObject.FindWithTag ("Linker");
			linker.GetComponent<Linker>().RecieveEngineOutput(server,client);
			hasClientSent = false;
			hasServerSent = false;
			hasEngineStarted = false;
		}
	
	}



}