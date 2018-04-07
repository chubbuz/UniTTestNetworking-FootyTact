
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private GameObject ball;
	public float  initialYPos;
	public float initialBallYpos;

	public bool isMovementAllowed;
	public Transform State;
	public Transform attemptedState;
	public Plane plane;
	public bool hasBall;
	public Material material,ballMaterial;
	public LineRenderer line;

	void Start(){

		hasBall = false;
		if (transform.name == "ServerPlayer0") {
			hasBall = true;
			//			print ("ServerPlayer0 start Run i.e hasBall=false");
		}
		State = this.transform;
		initialYPos = State.position.y;
		isMovementAllowed = false;
		attemptedState = new GameObject ("attemptedState").transform;
		//		attemptedState = GameObject.Find ("EmptyTransform").transform;
		attemptedState.position = this.transform.position;
		//

		GameObject ground = GameObject.Find ("Plane");
		Vector3 groundPos = ground.transform.position;
		plane = new Plane(Vector3.up,groundPos);
		ball = GameObject.Find ("Ball");
		initialBallYpos = ball.transform.position.y;

		line  =this.GetComponent<LineRenderer>();
		line.startWidth = 0.5f;
		line.endWidth = 0.1f;
		line.SetPosition(0,this.transform.position);
		line.SetPosition(1,this.transform.position);


	}


	void OnMouseDrag(){

		if (isMovementAllowed) {
			
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance;
			if (plane.Raycast (ray, out distance)) {
				Vector3 cursorPosition = ray.GetPoint (distance);
				line.startWidth = 0.5f;
				line.endWidth = 0.1f;

				if (this.hasBall) {
					Vector3 correctPos = new Vector3 (cursorPosition.x,initialBallYpos,cursorPosition.z);
					ball.GetComponent<BallBehaviour> ().attemptedPos = correctPos;
					ball.GetComponent<BallBehaviour> ().hasBallmoved = true;

					//line.material = new Material (material);
					line.SetPosition(0,this.transform.position);
//					sline.SetColors (Color.white, Color.white);

					line.SetPosition(1, cursorPosition);
					//line.SetWidth (0.5f, 0.1f);

//					print (this.transform.name + "has ball");
//					print ("passing ball by :"+this.transform.name );
					line.startColor=Color.white;
					line.endColor = Color.black;


//					


				} else {
					Vector3 correctPos = new Vector3 (cursorPosition.x,initialYPos,cursorPosition.z);
					attemptedState.position = correctPos;
					State = this.transform;


					line.SetPosition(0,this.transform.position);
//					line.SetColors (Color.green, Color.green);
					line.SetPosition(1, cursorPosition);//end

					line.startColor=Color.yellow;
					line.endColor = Color.blue;

	
//					print ("moving:"+this.transform.name );

				}

			}

		} 
	}



}
 