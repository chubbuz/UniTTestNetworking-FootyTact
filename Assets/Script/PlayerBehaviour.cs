
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	public bool isMovementAllowed;
	public Transform State;
	public Transform attemptedState;
	public Plane plane;
	public bool hasBall;
	void Start(){

		hasBall = false;
		if (transform.name == "ClientPlayer0") {
			hasBall = true;
			//			print ("ServerPlayer0 start Run i.e hasBall=false");
		}
		State = this.transform;
		isMovementAllowed = false;
		attemptedState = new GameObject ().transform;
		//		attemptedState = GameObject.Find ("EmptyTransform").transform;
		attemptedState.position = this.transform.position;
		//

		GameObject ground = GameObject.Find ("Plane");
		Vector3 groundPos = ground.transform.position;
		plane = new Plane(Vector3.up,groundPos);
	}


	void OnMouseDrag(){

		if (isMovementAllowed) {
			
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance;
			if (plane.Raycast (ray, out distance)) {
				Vector3 cursorPosition = ray.GetPoint (distance);
			
				if (hasBall) {
					GameObject ball = GameObject.Find ("Ball");

					print ("Setting the position of Ball");
					ball.GetComponent<BallBehaviour> ().attemptedPos = cursorPosition;

				} else {
//				transform.position = cursorPosition;
					attemptedState.position = cursorPosition;
					State = this.transform;
				}

			}

		} 
	}
}
 