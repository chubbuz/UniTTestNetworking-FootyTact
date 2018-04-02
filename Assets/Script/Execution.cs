using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Execution :MonoBehaviour {
	private GameObject[] server;
	private GameObject[] client;

	void Start(){
		server = new GameObject[5];
		client = new GameObject[5];


		for (int i = 0; i < 5; i++) {
			server[i]=GameObject.Find("ServerPlayer"+i);
			client[i]=GameObject.Find("ClientPlayer"+i);

		}
	}


	public void Execute(Vector3[] sPos,Vector3[] cPos)
	{
		//update the new position of ball and player
		for (int i = 0; i < 5; i++) {
			client[i].transform.position=cPos[i];
			server[i].transform.position=sPos[i];
		}

		//Command Session Manager to start next session
		this.GetComponent<SessionManager>().StartNextSession();

	}


}
