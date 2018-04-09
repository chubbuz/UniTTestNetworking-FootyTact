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
	private Vector3[] allPlayer;
	//previous position
	private Vector3[] serverPrev;
	private Vector3[] clientPrev;
	private Vector3 playerSize;
	private Vector3[] allPlayerPrev;


	//private int ballPlayerIndex;
	//All the information of the game
	private Plane plane;
	private float groundPlane;
//	private

	private GameObject messageUI;





	//sub-engines
	private BallEngine bEngine;
	void Start(){
		hasEngineStarted = false;

		//init new Position array
		server = new Vector3[5];
		client = new Vector3[5];

		//init sarting pos of players
		serverPrev = new Vector3[5];
		clientPrev = new Vector3[5];
		allPlayer = new Vector3[10];
		allPlayerPrev = new Vector3[10];

		for (int i = 0; i < 5; i++) {
			serverPrev[i] = GameObject.Find ("ServerPlayer" + i).transform.position;
			clientPrev [i] = GameObject.Find ("ClientPlayer"+i).transform.position;

			allPlayerPrev [i] = serverPrev [i];
			allPlayerPrev [i + 5] = clientPrev [i];
		}
		//init size of player
		GameObject sample = GameObject.Find ("ServerPlayer0");
		playerSize = sample.GetComponent<Collider> ().bounds.size;



		//init ground plane
		GameObject ground = GameObject.Find ("Plane");
		Vector3 groundPos = ground.transform.position;
		groundPlane = 0;//groundPos.y;
		plane = new Plane(Vector3.up,groundPos);

		bEngine = new BallEngine ();
		bEngine.near = playerSize.x;
		bEngine.sourcePlayer = 0;
		bEngine.UpdateInfo ();



		messageUI = GameObject.Find ("EngineMessage");

		//bEngine.near=playerSize.x+

	}

	public void ClientPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos,bool isBallMoved

	){

//		print ("ClientPosition Recieved in Engine");
		hasClientSent = true;

		client[0]=cPos0;
		client[1]=cPos1;
		client[2]=cPos2;
		client[3]=cPos3;
		client[4]=cPos4;

		client [4].y = client [3].y = client [2].y = client [1].y = client [0].y = -groundPlane;

//		bEngine.hasBallMoved = false;
		//bEngine.hasBallMoved = isBallMoved;

		if (bPos != Vector3.back) {
//			print ("Udating ball info form Client"+bPos);
			bEngine.targetBall = bPos;
			bEngine.isBallWithServer = false;
			bEngine.hasBallMoved = isBallMoved;


		}

		for (int i = 0; i < 5; i++) {
			allPlayer [i + 5] = client [i];

		}

	}


	public void ServerPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos,bool isBallMoved
	){

//		print ("ServerPosition Recieved in Engine");
		hasServerSent = true;

		server[0]=cPos0;
		server[1]=cPos1;
		server[2]=cPos2;
		server[3]=cPos3;
		server[4]=cPos4;

		if (bPos != Vector3.back) {
			bEngine.hasBallMoved = isBallMoved;

			//print ("Udating ball info form Server: "+bPos);

			bEngine.targetBall = bPos;
//			print ("hasBallmoved set in bENinge :" + bEngine.hasBallMoved);
			bEngine.isBallWithServer = true;

		}

		for (int i = 0; i < 5; i++) {
			server [i].y = -groundPlane;
			allPlayer [i] = server [i];


		}


	}




	void Update(){
		
		if (hasServerSent && hasClientSent && !hasEngineStarted) {
			hasEngineStarted = true;


		//	print ("bEngine.targetPlayer" + bEngine.targetPlayer);
//				print ("bEngine.targetBall" + bEngine.targetBall);
			proposedBall.transform.position = bEngine.targetBall;


			//ballProcessing
			Vector3 accurateBallPos;


			int passedPlayerIndex=bEngine.sourcePlayer;


			if (bEngine.hasBallMoved ) {
				passedPlayerIndex = bEngine.NearestPlayer (allPlayer);

				if (passedPlayerIndex > -1) {
					bEngine.targetPlayer = passedPlayerIndex;
					bEngine.targetBall = allPlayer [passedPlayerIndex];
					int intrIndex =  bEngine.LaneInterruption (allPlayer, passedPlayerIndex);

//					for (int i = 0; i < 4; i++) {
//						inst.transform.name = "Vertice" + i;
//						inst.transform.position = bEngine.passZoneVertices [i];
//						Instantiate (inst);
//					}

					if (intrIndex > -1) {
						bEngine.targetPlayer = intrIndex;
						inst.transform.position = allPlayer [bEngine.targetPlayer];
						Instantiate (inst);
						print ("interupted by:" + intrIndex);
						messageUI.GetComponent<EngineUI> ().Display ("interupted by:" + intrIndex);

					} else {
						print ("No interruption on Pass");
						messageUI.GetComponent<EngineUI> ().Display ("No interruption on pass");

					}
					accurateBallPos = bEngine.LocateBall (allPlayer);
//					print("Accurate Ball just sent from BallENgine:"+accurateBallPos);
				} else {
					print ("The ball is misspassed");
					messageUI.GetComponent<EngineUI> ().Display ("The ball is MissPassed");

					bEngine.targetPlayer = bEngine.sourcePlayer;
					//print("passedPlayerIndex  just after misspassing="
					accurateBallPos = bEngine.sourceBall;//this should be to the opponent later
				}
			} else {
				Debug.Log ("No pass attempted");
				messageUI.GetComponent<EngineUI> ().Display ("NO pass is attempted");

				accurateBallPos = bEngine.targetBall;
				bEngine.targetPlayer = passedPlayerIndex;

			}

			bEngine.sourcePlayer=bEngine.targetPlayer;
			bEngine.sourceBall = accurateBallPos;






			//print ("accurateBallPos sent from ENigne =" + accurateBallPos);


			GameObject linker = GameObject.FindWithTag ("Linker");
			linker.GetComponent<Linker>().RecieveEngineOutput(server,client,accurateBallPos,bEngine.targetPlayer);
//			print ("accurateBallPos sent from ENigne =" + accurateBallPos);
			bEngine.targetPlayer = -1;
			bEngine.targetBall =  new Vector3();

			hasClientSent = false;
			hasServerSent = false;
			hasEngineStarted = false;
		}
	
	}

}