using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Execution :MonoBehaviour {
	private GameObject[] server;
	private GameObject[] client;
	private GameObject ball;
	private bool isBallWithServer;
	public int ballPlayerIndexPrev;


	void Start(){
		server = new GameObject[5];
		client = new GameObject[5];
		ball = GameObject.Find ("Ball");


		for (int i = 0; i < 5; i++) {
			server[i]=GameObject.Find("ServerPlayer"+i);
			client[i]=GameObject.Find("ClientPlayer"+i);

		}
		isBallWithServer = false;
	}


	public void Execute(Vector3[] sPos,Vector3[] cPos,Vector3 bPos,int plIndex)
	{
		//update the new position of ball and player
		for (int i = 0; i < 5; i++) {
			client[i].transform.position=cPos[i];
			server[i].transform.position=sPos[i];
		}
		ball.transform.position = bPos;
		ball.GetComponent<BallBehaviour> ().ballPlayerIndexPrev = ball.GetComponent<BallBehaviour> ().ballPlayerIndex;

		ball.GetComponent<BallBehaviour> ().ballPlayerIndex = plIndex;

//		print ("PlayeIndex on Execution:"+plIndex);


		//Command Session Manager to start next session
		ball.GetComponent<BallBehaviour> ().setBallPlayer (plIndex);
		this.GetComponent<SessionManager>().StartNextSession();

	}


	}





