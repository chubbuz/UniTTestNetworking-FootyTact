
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	public bool isMovementAllowed;

	private int startTime;
	private int time = 15;
	private bool isFirst = true;

	public Transform State;

	private bool hasSent=false;

	void Start(){
		isMovementAllowed = true;
	}



	void OnMouseDown(){

			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag(){

		if (isMovementAllowed) {
			Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
			transform.position = cursorPosition;
			State = this.transform;

		} else  {
			print ("Movement Disabled due to time up");
//			print (" Time is UP: Start time=" + startTime + " it is=" + DateTime.Now.Second);
			if (isFirst) {
				isFirst = false;



				//Send to the ServerEngine

			}
		}

	}
}
 