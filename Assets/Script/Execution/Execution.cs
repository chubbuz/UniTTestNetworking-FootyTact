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
	private int[] scorePrev;
	public bool amIServer;

	void Start(){
		server = new GameObject[5];
		client = new GameObject[5];
		ball = GameObject.Find ("Ball");


		for (int i = 0; i < 5; i++) {
			server[i]=GameObject.Find("ServerPlayer"+i);
			client[i]=GameObject.Find("ClientPlayer"+i);

		}
		isBallWithServer = false;
		scorePrev = new int[2];
	}


	public void Execute(Vector3[] sPos,Vector3[] cPos,Vector3 bPos,int plIndex,int[] score)
	{
		//update the new position of ball and player
		for (int i = 0; i < 5; i++) {
			client[i].transform.position=cPos[i];
			client [i].GetComponent<PlayerBehaviour> ().attemptedState.transform.position = cPos [i];
			server[i].transform.position=sPos[i];
			server [i].GetComponent<PlayerBehaviour> ().attemptedState.transform.position = sPos [i];

		}
		ball.transform.position = bPos;
		ball.GetComponent<BallBehaviour> ().ballPlayerIndexPrev = ball.GetComponent<BallBehaviour> ().ballPlayerIndex;

		ball.GetComponent<BallBehaviour> ().ballPlayerIndex = plIndex;


		//score
		if (amIServer) {
			GameObject scoreUI = GameObject.Find ("ScoreBoard");
			scoreUI.GetComponent<ScoreUI> ().score.text = "[you] :  "+score[0] +"-" +score[1]+"   : [friend]";
		} else {
			GameObject scoreUI = GameObject.Find ("ScoreBoard");
			scoreUI.GetComponent<ScoreUI> ().score.text = "[you] :  "+score[1] +"-" +score[0]+"   : [friend]";
		}
//		print ("PlayeIndex on Execution:"+plIndex);


		//Command Session Manager to start next session
		ball.GetComponent<BallBehaviour> ().setBallPlayer (plIndex);
		this.GetComponent<SessionManager>().StartNextSession();

	}


	}





