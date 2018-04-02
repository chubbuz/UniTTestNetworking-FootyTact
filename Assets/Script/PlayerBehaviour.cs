
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

	void Start(){

		hasBall = false;
		if (transform.name == "ClientPlayer0") {
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

	}


	void OnMouseDrag(){

		if (isMovementAllowed) {
			
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance;
			if (plane.Raycast (ray, out distance)) {
				Vector3 cursorPosition = ray.GetPoint (distance);
			
				if (hasBall) {
					Vector3 correctPos = new Vector3 (cursorPosition.x,initialBallYpos,cursorPosition.z);
					ball.GetComponent<BallBehaviour> ().attemptedPos = correctPos;
					LineRenderer line = this.GetComponent<LineRenderer>();
					line.material = new Material (material);
					//line.SetColors (Color.green, Color.green);
					line.SetPosition(0,this.transform.position);
					line.SetPosition(1, cursorPosition);
					//line.SetWidth (0.5f, 0.1f);

//					line.startColor=Color.yellow;
//					line.endColor = Color.blue;

					line.startWidth = 0.5f;
					line.endWidth = 0.1f;


				} else {
					Vector3 correctPos = new Vector3 (cursorPosition.x,initialYPos,cursorPosition.z);
					attemptedState.position = correctPos;
					State = this.transform;

					LineRenderer line = this.GetComponent<LineRenderer>();
					if (line == null)	
//					print ("lne is  null"); else print("line is complete");
					line.material = new Material (ballMaterial);

					//line.SetColors (Color.white, Color.white);
					line.SetPosition(0,this.transform.position);//start
					line.SetPosition(1, cursorPosition);//end
//					line.startColor=Color.white;
//					line.endColor = Color.black;

					line.startWidth = 0.5f;
					line.endWidth = 0.1f;
	

				}

			}

		} 
	}



}
 