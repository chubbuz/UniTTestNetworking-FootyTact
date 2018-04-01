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

	void Start(){
		hasEngineStarted = false;

		server = new Vector3[5];
		client = new Vector3[5];
	}

	public void ClientPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos

	){

		print ("ClientPosition Recieved in Engine");
		hasClientSent = true;

		client[0]=cPos0;
		client[1]=cPos1;
		client[2]=cPos2;
		client[3]=cPos3;
		client[4]=cPos4;
//		inst.transform.SetPositionAndRotation (cPos0,cRot0);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos1,cRot1);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos2,cRot2);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos3,cRot3);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos4,cRot4);
//		Instantiate (inst);
//
//		proposedBall.transform.SetPositionAndRotation (bPos, Quaternion.identity);
//		Instantiate (proposedBall);
	}


	public void ServerPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos
	){

		print ("ServerPosition Recieved in Engine");
		hasServerSent = true;

		server[0]=cPos0;
		server[1]=cPos1;
		server[2]=cPos2;
		server[3]=cPos3;
		server[4]=cPos4;
//
//		inst.transform.SetPositionAndRotation (cPos0,cRot0);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos1,cRot1);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos2,cRot2);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos3,cRot3);
//		Instantiate (inst);
//
//		inst.transform.SetPositionAndRotation (cPos4,cRot4);
//		Instantiate (inst);
	}




	void Update(){
		if (hasServerSent && hasClientSent && !hasEngineStarted) {
			hasEngineStarted = true;

			GameObject linker = GameObject.FindWithTag ("Linker");
			linker.GetComponent<Linker>().RecieveEngineOutput(server,client);
		}
	
	}


}