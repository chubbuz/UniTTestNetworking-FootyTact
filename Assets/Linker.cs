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
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<SessionManager> ().toStartCount = true;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<SessionManager> ().startTime=DateTime.Now.Second;
	
	}


	public 	void Send()
	{
		print ("Invoking Command");

		state = GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().State;

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
