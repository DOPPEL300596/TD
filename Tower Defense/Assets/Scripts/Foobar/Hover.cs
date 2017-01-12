using UnityEngine;
using System.Collections;

/**
 * This script will be attached to the object which will be carried around the cursor,
 * so when a tower is picked, the object will follow it.
 * 
 * To do so, create a 2D Sprite and name it 'Hover'.
 * Hover will be the icon for each corresponding object.
 */

public class Hover : Singleton<Hover> {
	/* Fields */
	private SpriteRenderer spriteRenderer;	/* Used as a reference to Hover's SpriteRenderer. */


	// Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		FollowMouse ();
	}

	/* This piece of code will make sure that the object or icon will follow
	 the mouse on screen. */
	private void FollowMouse(){
		/* This piece of code will ONLY be executed when the icon/sprite is activated. */
		if (spriteRenderer.enabled) {
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		}
		/* Since it's 2 dimensional, the z value will be set to 0. */

		/* Although the hover is not visible, the coordinate changes over time. */
	}

	/* This piece of code is used to deactivate/activate the hover icon.
	 The hover icon is none as default, and will change as you click some button. */
	public void Activate(Sprite sprite){
		this.spriteRenderer.sprite = sprite;
		spriteRenderer.enabled = true;
	}

	public void Deactivate(){
		spriteRenderer.enabled = false;
		/* This is used to prevent the tower from placing, as the icon is disabled as well. */
		GameManager.Instance.ClickedButton = null;
	}
}
