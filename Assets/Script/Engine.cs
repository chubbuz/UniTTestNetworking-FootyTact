using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	//for instantation
	public GameObject inst;
	public GameObject proposedBall;
	public GameObject ball;

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
	private Vector3 playerSize;

	//All the information of the game
	private Plane plane;


	//ball information
	private Vector3 newBallPos;
	private Vector3 prevBallPos;
	private string prevBallTeam;
	private string currBallTeam;
	private GameObject currBallPlayer;
	private GameObject prevBallPlayer;
	private float ballRadius;


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
		ball = GameObject.Find ("Ball");
		ballRadius = ball.GetComponent<SphereCollider> ().radius;

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


		if (bPos != Vector3.back)
			newBallPos = bPos;


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

		if (bPos != Vector3.back)
			newBallPos = bPos;

	}




	void Update(){
		if (hasServerSent && hasClientSent && !hasEngineStarted) {
			hasEngineStarted = true;

			ProcessBall ();




			GameObject linker = GameObject.FindWithTag ("Linker");
			linker.GetComponent<Linker>().RecieveEngineOutput(server,client);
			hasClientSent = false;
			hasServerSent = false;
			hasEngineStarted = false;
		}
	
	}


	private void ProcessBall(){

	
		//check if Ball passing lane is interrupted
//		GameObject passingArea= new GameObject();
//		passingArea.AddComponent<BoxCollider> ();
//		BoxCollider passArea = passingArea.GetComponent<BoxCollider> ();
//		float length = Vector3.Distance (newBallPos, prevBallPos);
//		float width = 2 * ballRadius + 2 * playerSize.x;
//
//		//midpoint
//		float midx =(newBallPos.x+prevBallPos.x) /2;
//		float midz = (newBallPos.z + prevBallPos.z) / 2;
//		Vector3 midPos = new Vector3 (midx, 0, midz);
//		passingArea.transform.SetPositionAndRotation (midPos, Quaternion.identity);
//
//		float angle = Vector3.Angle(newBallPos,prevBallPos);
//		passArea.size= new Vector3 (length, 2, width);
////		passingArea
//		passingArea.transform.Rotate (0, angle, 0);
//		passArea.transform.Rotate (0, angle, 0);
//
//
//
//		//Debug element
//		BoxCollider sample =inst.GetComponent<BoxCollider> ();
//		Vector3 sampleSize = new Vector3 (length, 2, width);
//		sample.size = sampleSize;
//		inst.transform.Rotate(0, angle, 0);
//		Instantiate (inst);
//
//
//		print ("ENgine delayed on BallProcessing");
//		Invoke ("delay", 10); 

		for (int i = 0; i < 10; i++) {
				
		}
	
	}

	bool isInside(Vector3[] polyPoints, Vector3  p){
		int  j = polyPoints.Length-1; 
		bool inside = false; 
		for (int i = 0; i < polyPoints.Length; j = i++) { 
			if ( ((polyPoints[i].z <= p.z && p.z < polyPoints[j].z) || (polyPoints[j].z <= p.z && p.z < polyPoints[i].z)) && 
				(p.x < (polyPoints[j].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[j].z - polyPoints[i].z) + polyPoints[i].z)) 
				inside = !inside; 
		} 
		return inside; 
	}


}