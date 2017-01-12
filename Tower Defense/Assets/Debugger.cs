using UnityEngine;
using System.Collections;

public class Debugger : MonoBehaviour {
	private int counter = 1;

	// Use this for initialization
	void Start () {
		CreateLog ();
	}
	
	// Update is called once per frame
	void Update () {
		int second;

		counter++;
		if (counter % 100 == 0) {
			CreateLog ();
		}

		second = (int)Time.time;
		if (second % 10 == 0)
			Debug.Log ("Timelapse : " + second);
	}

	private void CreateLog(){
		Debug.Log ("counter = " + counter);
	}
}
