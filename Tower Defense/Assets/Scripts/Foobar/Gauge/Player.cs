using UnityEngine;
using System.Collections;

/* The player needs to be able to use stat, so
 this is where the class will be implemented. */
public class Player : MonoBehaviour {
	/* Fields */
	[SerializeField]
	private Stat health;

	// Use this for initialization
	void Start () {
		//health.MaxValue = 200;
		//health.CurrentValue = 120;
	}
	
	// Update is called once per frame
	void Update () {
		/* Every time [Q] is pressed, then reduce health's current value. */
		if(Input.GetKeyDown (KeyCode.Q))
		   health.CurrentValue -= 10;

		/* Every time [W] is pressed, then increase health's current value. */
		if(Input.GetKeyDown (KeyCode.W))
			health.CurrentValue += 7;

	}

	private void Awake(){
		health.Initialize();	
	}
}
