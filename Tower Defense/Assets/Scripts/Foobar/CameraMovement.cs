//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

/* Add the script to Main Camera */
public class CameraMovement : MonoBehaviour
{
	/* Field(s) */
	[SerializeField]
	/* Default camera speed */
	private float cameraSpeed = 0;

	/* Maximum row and column */
	private float 	row,	/* Maximum Y*/
					col;	/* Maximum X*/

	void Start(){
	}

	private void Update(){
		/* Execute GetInput() */
		GetInput ();
	}

	/* This function will take players' input to actually move camera around. */
	private void GetInput(){
		/* Standard input:
			- W : up
			- A : left
			- S : down
			- D : right
		 */

		/* If certain keycode is pressed, then take action. */
		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.up * cameraSpeed * Time.deltaTime);
		/* 	transform.Translate will make camera to move on the screen,
			based on whatever we do.

			Parameter(s):
			- Vector3.up 		= tell the camera to move upward
			- cameraSpeed 		= how fast is the movement?
			- Time.deltaTime	= the amount of time that has passed since the last time Update() was called.

			The camera the will be moving in the same speed and size.
			The rest is simply co-pas.
		 */

		/* Left */
		if (Input.GetKey (KeyCode.A))
			transform.Translate (Vector3.left * cameraSpeed * Time.deltaTime);

		/* Down */
		if (Input.GetKey (KeyCode.S))
			transform.Translate (Vector3.down * cameraSpeed * Time.deltaTime);

		/* Right */
		if (Input.GetKey (KeyCode.D))
			transform.Translate (Vector3.right * cameraSpeed * Time.deltaTime);

		/* 	The whole function is simply to move camera wherever direction it might go to.*/
		/* 	But our job isn't finished yet, we still need to restrict the camera movement. 
			Make sure the camera doesn't reach the 'blue edges'.	
		 */

		/* In order to do so, let's "clamp" to prevent any exceeded value.*/
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, col), Mathf.Clamp(transform.position.y, row, 0), -20);

		/*
			Clamp:
			A value shouldn't exceed (n) or below (m)

			Where (n) is a minimum value, and
			(m) is maximum value.
		 */
	}

	public void SetCameraBounds(Vector3 maxTile){
		/* Conversion from viewport to world point */
		Vector3 worldPoint = Camera.main.ViewportToWorldPoint (new Vector3(1, 0));
		
		row = maxTile.y - worldPoint.y;
		col = maxTile.x - worldPoint.x;
	}

}


