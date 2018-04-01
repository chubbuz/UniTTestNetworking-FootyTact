using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {


	public GameObject ballPlayer;
	public bool isBallWithServer;
	public GameObject passAttemptedTO;
	public Vector3 attemptedPos;
	// Use this for initialization
	void Start () {
		//print ("Ball start run");
		ballPlayer=GameObject.Find ("ClientPlayer0");
		ballPlayer.GetComponent<PlayerBehaviour> ().hasBall = true;
		isBallWithServer = false;

		attemptedPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
