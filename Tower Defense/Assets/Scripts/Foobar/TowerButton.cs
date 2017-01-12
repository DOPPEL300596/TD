using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* This class will be embedded to the tower button, so it contains an actual prefab of a tower. */

public class TowerButton : MonoBehaviour {
	/* Fields */
	[SerializeField]
	private GameObject towerPrefab;

	/* Each button will have sprite. */
	[SerializeField]
	private Sprite sprite;

	/* Each button will decrease the currency valud, which makes sense to every TD game. */
	[SerializeField]
	private int price;

	[SerializeField]
	private Text priceText;

	/* Properties */
	public GameObject TowerPrefab{
		get
		{
			return towerPrefab;
		}
	}

	public Sprite Sprite{
		get
		{
			return sprite;
		}
	}

	public int Price{
		get
		{
			return price;
		}
	}

	private void Start(){
		priceText.text = price + " OXYGEN";
	}
}
