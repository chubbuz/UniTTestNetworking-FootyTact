using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionManager : MonoBehaviour {

	// Use this for initialization
	private bool hasSent = false;
	private int time = 8;
	public  int startTime;

	public bool toStartCount;


	void Start () {
		
		toStartCount = false;
		//print ("start time is set to:" + startTime);
	}
	
	// Update is called once per frame
	void Update(){
		//		print ("Updting frame");
		if (!hasSent && toStartCount) {
			if (DateTime.Now.Second - startTime > time) {
				print ("Time is Up");

				hasSent = true;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().isMovementAllowed = false;
				GameObject.FindGameObjectWithTag ("Linker").GetComponent<Linker> ().Send();

			} else {
				print ("time remaining =" + (time - DateTime.Now.Second + startTime));
			}

		}
	}
}
