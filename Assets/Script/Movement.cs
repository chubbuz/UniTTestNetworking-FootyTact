
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	public bool isMovementAllowed;
	public Transform State;


	void Start(){
		State = this.transform;
		isMovementAllowed = false;
	}




	void OnMouseDown(){
		if (isMovementAllowed) {
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}else  {
			print (this.transform.name+ " ->Movement Disabled ");
		}

	}

	void OnMouseDrag(){

		if (isMovementAllowed) {
			Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
			transform.position = cursorPosition;
			State = this.transform;

		} 
	}
}
 