using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Execution :MonoBehaviour {



	public void Execute(Vector3[] sPos,Vector3[] cPos)
	{
		//myTeam = "ClientPlayer";

		GameObject clientPlayer0 = GameObject.Find("ClientPlayer0");
		GameObject clientPlayer1 = GameObject.Find("ClientPlayer1");
		GameObject clientPlayer2 = GameObject.Find("ClientPlayer2");
		GameObject clientPlayer3 = GameObject.Find("ClientPlayer3");
		GameObject clientPlayer4 = GameObject.Find("ClientPlayer4");

		clientPlayer0.transform.position=cPos[0];
		clientPlayer1.transform.position=cPos[1];
		clientPlayer2.transform.position=cPos[2];
		clientPlayer3.transform.position=cPos[3];
		clientPlayer4.transform.position=cPos[4];

		GameObject serverPlayer0 = GameObject.Find ("ServerPlayer0");
		GameObject serverPlayer1 = GameObject.Find ("ServerPlayer1");
		GameObject serverPlayer2 = GameObject.Find ("ServerPlayer2");
		GameObject serverPlayer3 = GameObject.Find ("ServerPlayer3");
		GameObject serverPlayer4 = GameObject.Find ("ServerPlayer4");

		serverPlayer0.transform.position=sPos[0];
		serverPlayer1.transform.position=sPos[1];
		serverPlayer2.transform.position=sPos[2];
		serverPlayer3.transform.position=sPos[3];
		serverPlayer4.transform.position=sPos[4];


		//Debug.Log("Took damage:" + amount);
	}

//	Vector3 pPos0,Quaternion cRot0,
//	Vector3 pPos1,Quaternion cRot1,
//	Vector3 pPos2,Quaternion cRot2,
//	Vector3 pPos3,Quaternion cRot3,
//	Vector3 pPos4,Quaternion cRot4,
//	Vector3 bPos

}
