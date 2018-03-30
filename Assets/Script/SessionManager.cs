﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionManager : MonoBehaviour {

	// Use this for initialization
	private bool hasSent = false;
	public int time = 8;
	public  int startTime;

	public bool toStartCount;


	void Start () {
		
		toStartCount = false;
	}
	
	// Update is called once per frame
	void Update(){


		//if time Ended	
		if (!hasSent && toStartCount) {
			if (DateTime.Now.Second - startTime > time) {

				GameObject clientPlayer = GameObject.Find("ClientPlayer");
				GameObject serverPlayer = GameObject.Find ("ServerPlayer");

				print ("Time is Up");
				toStartCount = false;
				hasSent = true;

				print ("deactivating server movements");
				clientPlayer.GetComponent<Movement> ().isMovementAllowed = false;
				serverPlayer.GetComponent<Movement> ().isMovementAllowed = false;

				print ("Calling Send Function from SessionManager");
				GameObject netMan = GameObject.FindWithTag("Linker");
				netMan.GetComponent<Linker>().Send();

			} else {
				//print ("time remaining=" + (time - DateTime.Now.Second + startTime));
			}

		}
	}
}
