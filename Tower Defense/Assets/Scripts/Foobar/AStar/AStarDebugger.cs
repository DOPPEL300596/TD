using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This class is used to debug the pathfinding algorithm. */
public class AStarDebugger : MonoBehaviour {
	/* Fields */
	private TileScript 	start, 
						goal;

	[SerializeField]
	private Sprite 		blankTile;

	[SerializeField]
	private GameObject	arrowPrefab;

	[SerializeField]
	private GameObject 	debugTilePrefab;
	/* Use this tile separately when debugging. */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	/* If [SPACE] is pressed, then generate path. */
	/*
	void Update () {
		ClickTile ();

		if (Input.GetKeyDown (KeyCode.Space))
			AStar.GetPath (start.GridPosition, goal.GridPosition);

	}
	*/
	private void ClickTile(){
		/*
		 * Parameter of MouseButton:
		 * 
		 * 0 = left click
		 * 1 = right click 
		 */

		if (Input.GetMouseButtonDown (1)) {
			/* Raycast is an invisible ray from any point of a game; out in the game world, etc.
			 The function is similiar to invisible detector when the ray hits something in the game.
			 */
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			/* If we hit something, or the collider is not null, then... */
			if(hit.collider != null){
				/* then we need to make a reference to any object we hit. */
				TileScript tmp = hit.collider.GetComponent<TileScript>();

				/* If there is an object that is hit, then... */
				if(tmp != null){
					/* then we need to decide something we need to do. */
					if(start == null){
						start = tmp;
						CreateDebugTile(start.WorldPosition, new Color32(255, 135, 0, 255));
					}

					/**
					 * The first time we click a tile in a geme, then it's equal to the tile we just clicked.
					 */
					else if(goal == null){
						goal = tmp;
						CreateDebugTile(goal.WorldPosition, new Color32(255, 0, 0, 255));
					}
				}
			}
		}
	}
	/* The HashSet requires importing from System.Collections.Generic */
	public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> finalPath){
		/* Open list */
		foreach (Node node in openList) {
			/* The marked color indicates only the neighbor, but leaves the parent to its original color. */
			if(node.TileRef != start && node.TileRef != goal)
				CreateDebugTile(node.TileRef.WorldPosition, Color.cyan, node);
			
			/* Call function PointToParent() */
			PointToParent(node, node.TileRef.WorldPosition);
		}

		/* Closed list */
		foreach (Node node in closedList) {
			/* The marked color indicates only the neighbor, but leaves the parent to its original color. */
			if(node.TileRef != start && node.TileRef != goal && !finalPath.Contains(node))
				CreateDebugTile(node.TileRef.WorldPosition, Color.blue, node);

			/* Call function PointToParent() */
			PointToParent(node, node.TileRef.WorldPosition);
		}

		/* Final path */
		foreach (Node node in finalPath) {
			if(node.TileRef != start && node.TileRef != goal)
				CreateDebugTile(node.TileRef.WorldPosition, Color.green, node);
		}
	}


	/*!! THE INTERESTING PART STARTS HERE. !!*/
	private void PointToParent(Node node, Vector2 position){
		/* This function will use a bunch of if statements. */

		if (node.Parent != null) {
			GameObject arrow = (GameObject)Instantiate (arrowPrefab, position, Quaternion.identity);
			/* Cast it as a GameObject. */

			/* Set the layer position of arrow (it has to be the top. */
			arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;

			/* Here comes the fun! 
			 The direction is counter clockwise. */

			/* Right */
			if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 0);

			/* Top Right */
			else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 45);

			/* Top */
			else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 90);

			/* Top Left */
			else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 135);

			/* Left */
			else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 180);

			/* Bottom Left */
			else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 225);

			/* Bottom */
			else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 270);

			/* Bottom Right */
			else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
				arrow.transform.eulerAngles = new Vector3 (0, 0, 315);
		}
		/**
		 * Note:
		 * A = current node
		 * B = parent node
		 * 
		 * A.X = B.X 	=> no horizontal movement (left or right)
		 * A.Y = B.Y	=> no vertical movement (up or down)
		 * 
		 * A.X > B.X	=> to left
		 * A.X < B.X 	=> to right
		 * 
		 * A.Y > B.Y	=> to top
		 * A.Y < B.Y	=> to bottom
		 */
	}
	/* END OF INTERESTING PART */

	/* Placing Debug Tile */
	/* (node) is optional parameter, which can be added value or not.
	 If there is no value added, then default value will be used. */
	private void CreateDebugTile(Vector3 worldPosition, Color32 color, Node node = null){
		GameObject debugTile = (GameObject)Instantiate (debugTilePrefab, worldPosition, Quaternion.identity);

		if (node != null) {
			/* Use tmp to shorten the code line. */
			DebugTile tmp = debugTile.GetComponent<DebugTile>();

			tmp.GText.text += node.GCost;
			tmp.HText.text += node.HCost;
			tmp.FText.text += node.FCost;
		}

		/* Change the color of debug tile. */
		debugTile.GetComponent<SpriteRenderer> ().color = color;


	}
}





