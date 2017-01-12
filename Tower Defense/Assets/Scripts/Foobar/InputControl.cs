using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ShowKeyDialog ();
	}

	void ShowKeyDialog(){
		/* W - A - S - D */

		if (Input.GetKey (KeyCode.A))
			Debug.Log ("Left");

		if (Input.GetKey (KeyCode.D))
			Debug.Log ("Right");

		if (Input.GetKey (KeyCode.W))
			Debug.Log ("Up");

		if (Input.GetKey (KeyCode.S))
			Debug.Log ("Down");

	}
}
