﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour{
	/* Fields */
	private Color32 fullColor = new Color32(255, 118, 118, 255),
					emptyColor = new Color32(96, 255, 90, 255);

	private SpriteRenderer spriteRenderer;

	/* This color will be shown on tile if something is already placed on a tile.
	For example, tower or traps, this is to prevent overlapping.*/

	/* Properties */
	/* For now, just add a grid position to a tile. */
	public Point GridPosition{ get; private set;}
	
	/* 'private set' is to prevent any accidental changing to grid position. */

	public Vector2 WorldPosition{
		get
		{
			return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x/2),
			                   transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
		}
	}

	/* Temporary property */
	public bool IsDebugging {
		get;

		set;
	}

	public bool IsPassable {
		get;

		set;
	}

	/* This property will be used in pathfinding to determine which tile is passable. 
	 This also indicates whether it is valid to put a tower on certain tile or not. (Occupied or not)*/
	public bool IsEmpty {
		get;

		private set;
	}

	public void Setup(Point gridPosition, Vector3 worldPosition, Transform parent){
		/* Each tile is initially unoccupied. */
		IsPassable = true;
		IsEmpty = true;

		GridPosition = gridPosition;
		transform.position = worldPosition;

		/* 
		 	Now comes the thing where we need to use our Singleton.
			Because 'Tiles' is not accessible from TileScript because it's inside the LevelManager.cs

			Somehow, we need to access the LevelManager from TileScript.

			If we use a hardcode without any Singleton, it would be like this:

			LevelManager lm = GameObject.FindObjectOfType<LevelManager>();
			lm.Tiles.Add(gridPosition, this);

			We actually create a reference and use FindObjectOfType to find the specific object.
			Then we're using that reference to access 'Tiles'. Although it somehow works, it's still tiring.
			Because every single time we need to access the LevelManager, we have to write the code manually.

			We can actually use a Singleton pattern, so the LevelManager's public functionality can be accessed
			in a very easy way.

			
		 */
		transform.SetParent (parent);
		LevelManager.Instance.Tiles.Add (gridPosition, this);
	}

	/* This is script will always be automatically executed whenever the mouse is over the specific tile, tower, or other object. */
	/* Overridden */
	private void OnMouseOver(){
		/* To determine which position of the tile based on X and Y coordinate. 
		 	This script complements the collider (you have to create one from prefab folder.)*/
		// Debug.Log (GridPosition.X + ", " + GridPosition.Y);

		/* The piece of code below will only be executed if the mouse is not over the game object (on the UI). */ 
		if (!EventSystem.current.IsPointerOverGameObject () && GameManager.Instance.ClickedButton != null) {
			/* If the tile is unoccupied AND is not debugging, then tower placement is allowed. */
			if(IsEmpty && !IsDebugging)
				ColorTile(emptyColor);

			if(!IsEmpty && !IsDebugging)
				ColorTile (fullColor);
			/* If the mouse is clicked, then place tower. 
		 	This is only executed once. */
			else if(Input.GetMouseButton (0))
				PlaceTower();
		}


	}

	/* This function is used to prevent color leakage. */
	/* Overridden */
	private void OnMouseExit(){
		if(!IsDebugging)
			ColorTile (Color.white);
	}

	/* Place a tower to a specific tile */
	private void PlaceTower(){
		//Debug.Log ("Placing tower...");

		/* Place a tower to specific position, but do not rotate it (Quartenion.identity = no rotation is allowed )*/
		/* The casting needs to be done so the layer position can be adjusted. */
		GameObject tower = (GameObject) Instantiate (GameManager.Instance.ClickedButton.TowerPrefab, transform.position, Quaternion.identity);

		/* The tower sorting order now equals to grid position Y. */
		tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
		
		/* This sets each tile as a parent of overlapping tower. */
		tower.transform.SetParent (transform);

		/* This tile is occupied! */
		IsEmpty = false;

		/* Revert the color. */
		ColorTile (Color.white);

		GameManager.Instance.BuyTower();
		/* Else, the errors might get after this line, and we don't want that. */

		/* If there is already a tower, then IsPassable value is false. */
		IsPassable = false;
	}

	private void ColorTile(Color newColor){
		spriteRenderer.color = newColor;
	}

	void Start(){
		/* Initiate the spriteRenderer value. */
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	void Update(){
		
	}
}

/**
 * 	Traits:
 * 	
 * 	Constructible and Walkable			: normal tile
 *  Constructible and Not Walkable		: tower-exclusive tile
 * 	Not Constructible and Walkable		: unit-exclusive tile
 *  Not Constructible and Not Walkable	: terrain tile
 * 
 * If the variable is private, it cannot be added to prefab.
 * 
 * 
 **/


