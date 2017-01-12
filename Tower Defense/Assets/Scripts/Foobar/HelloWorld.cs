using UnityEngine;
using System.Collections;

/* 
	This class is used to create random comments, or to test script(s).
 */

public class HelloWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* Initial procedure */
	void Initialize(){
		InitLog ();
	}

	/* Display initial commments */
	void InitLog(){
		Debug.Log ("Hello World!");
		Debug.Log ("I love Unity!");
	}

	
	/*
		Note:

		Right click to add folder or script in Assets.
		To see the script's taking effect, it must be added to an object.

		And Voila!
	 */

}
