using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	public Transform Server0;
	public GameObject inst;


	public void ClientPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4
	){

		print ("ClientPosition Recieved in Engine");

		inst.transform.SetPositionAndRotation (cPos0,cRot0);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos1,cRot1);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos2,cRot2);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos3,cRot3);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos4,cRot4);
		Instantiate (inst);
	}


	public void ServerPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4
	){

		print ("ServerPosition Recieved in Engine");


		inst.transform.SetPositionAndRotation (cPos0,cRot0);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos1,cRot1);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos2,cRot2);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos3,cRot3);
		Instantiate (inst);

		inst.transform.SetPositionAndRotation (cPos4,cRot4);
		Instantiate (inst);
	}

}