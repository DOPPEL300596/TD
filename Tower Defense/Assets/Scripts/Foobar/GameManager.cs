using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This class is used to handle game mechanics and timing. 
 * It handles nearly everthing; currency, wave control, enemy spawning, and so on.
 */

public class GameManager : Singleton<GameManager> {
	/* Fields */
	private int currency;	/* This represents a coin or cost to build each tower. */

	[SerializeField]
	private Text currencyText;		/* This requires UnityEngine.UI */



	/* Properties */
	public TowerButton ClickedButton {
		get;

		set;
	}

	public int Currency{
		get
		{
			return currency;
		}
		set
		{
			currency = value;
			currencyText.text = value.ToString () + " <color=lime>OXYGEN</color>";
		}
	}

	/* Make reference to ObjectPool */
	public ObjectPool Pool {
		get;

		set;
	}

	// Use this for initialization
	void Start () {
		Currency = 2000;
	}
	
	// Update is called once per frame
	void Update () {
		HandleEscape ();
	}

	public void PickTower(TowerButton towerButton){
		if (currency >= towerButton.Price) {
			this.ClickedButton = towerButton;
			Hover.Instance.Activate (towerButton.Sprite);
		}
	}

	public void BuyTower(){
		if (Currency >= ClickedButton.Price) {
			/* If the buying is successful, then decrease the value. */
			Currency -= ClickedButton.Price;

			/* This will deactivate the sprite once the tower has been spawned. */
			Hover.Instance.Deactivate ();
		}
	}

	/* This is piece of code is used to drop a tower. */
	private void HandleEscape(){
		/* If [ESC] is pressed, the cancel place tower. */
		if (Input.GetKeyDown (KeyCode.Escape))
			Hover.Instance.Deactivate ();
	}

	/* This function will start a new wave. */
	public void StartWave(){
		StartCoroutine (SpawnWave ());
	}

	/* This function is for co-routine. */
	private IEnumerator SpawnWave(){
		LevelManager.Instance.GeneratePath ();

		/* Create a random monster. */
		int monsterIndex = Random.Range (0, 2);

		string type = string.Empty;

		switch (monsterIndex) {
			case 0:	type = "reversed_cocaine"; break;
			case 1: type = "reversed_cocaine"; break;
			default: type = "reversed_cocaine"; break;
		}

		/* Create a reference to the spawned monster. */
		Monster monster = Pool.GetObject (type).GetComponent<Monster>();
		monster.Spawn ();

		/* Each time the mob enters the stage, it will have a delay for about 2.5 seconds. */
		yield return new WaitForSeconds(2.5f);
	}

	private void Awake(){
		Pool = GetComponent<ObjectPool> ();
	}
}
