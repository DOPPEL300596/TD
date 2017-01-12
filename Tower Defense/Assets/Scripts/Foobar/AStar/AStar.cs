using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

/* Static keyword identifies an ease of access to another class. 
 Not only the class needs to be static, but methods and procedures as well. */
public static class AStar
{
	/* 1. Create a Dictionary of Nodes. 
	 Dictionary is faster than a List. */
	private static Dictionary<Point, Node> nodes;
	/* The key is a point, whilst node represents the value. */


	/* This function is used to create nodes.
	 This function will create nodes for each tile in the game. */
	/* This function is the CORE of generating paths... */
	private static void CreateNodes(){
		nodes = new Dictionary<Point, Node> ();

		foreach(TileScript tile in LevelManager.Instance.Tiles.Values){
			nodes.Add (tile.GridPosition, new Node(tile));
		}
	}

	/* This function returns a final path, which is a stack of nodes. */
	public static Stack<Node> GetPath(Point start, Point goal){
		/* If the nodes don't exist, then it has to be created first. */
		if (nodes == null)
			CreateNodes ();

		/* Create an open list. */
		HashSet<Node> openList = new HashSet<Node>();

		/* Create an closed list. */
		HashSet<Node> closedList = new HashSet<Node>();

		Stack<Node> finalPath = new Stack<Node> ();

		/* Add the starting point to open list. */
		Node currentNode = nodes[start];
		openList.Add (currentNode);

		/* 10. Loop-ception */
		while (openList.Count > 0){
			/* Create a for-loop to iterate all over the neighbors. */
			for(int i = -1; i <= 1; i++){		/* X coordinate */
				for(int j = -1; j <= 1; j++){	/* Y coordinate */
					Point neighborPosition = new Point(currentNode.GridPosition.X - i, currentNode.GridPosition.Y - j);
					
					
					if(LevelManager.Instance.IsInBounds(neighborPosition) && 
					   LevelManager.Instance.Tiles[neighborPosition].IsPassable && 
					   neighborPosition != currentNode.GridPosition){
						/* Initialize G cost */
						int gCost = 0; 
						
						/* Abs = returns absolute value (no negatives) */
						/* The if statement is used to determine values of neighboring tiles,
					 	diagonally, vertically, or horizontally. */
						if(Math.Abs(i - j) == 1)
							gCost = 10;
						else{
							if(!IsDiagonal(currentNode, nodes[neighborPosition]))
							   continue; 
							   /* Continue = proceed to the next loop and cut any process next to it.*/
							gCost = 14;
						}
						Node neighbor = nodes[neighborPosition];
						//Debug.Log(neighborPosition.X + ", " + neighborPosition.Y);
						
						/* !! THIS IS ONLY FOR DEBUGGING, NEEDS TO BE REMOVED LATER!  !!*/
						//neighbor.TileRef.SpriteRenderer.color = Color.black;
						/* END OF DEBUGGING */
						
						
						if(openList.Contains(neighbor)){
							/* Do something... */
							/* 9.4. Calculate neighbors. */
							if(currentNode.GCost + gCost < neighbor.GCost)
								neighbor.Calculate (currentNode, nodes[goal], gCost);
						}
						/* 9.1. Examine all neighbors. */
						else if (!closedList.Contains(neighbor)){
							/* 9.2. Add undiscovered neighbor. */
							openList.Add (neighbor);
							/* 9.3. calculate values. */
							neighbor.Calculate(currentNode, nodes[goal], gCost);
						}

					}
				}
			}
			
			openList.Remove (currentNode);
			closedList.Add (currentNode);
			
			/* 7. Comparing the F value */
			/* LINQ can be found on System.Linq*/
			if (openList.Count > 0) {
				currentNode = openList.OrderBy(n => n.FCost).First();
				/* This takes the whole list then orders it by the F value. 
			 Finally, take the first element of the list (which has the lowest score of FCost.*/
			}

			if(currentNode == nodes[goal]){
				while(currentNode.GridPosition != start){
					finalPath.Push(currentNode);
					currentNode = currentNode.Parent;

					/* Final path is a stack of nodes. */
				}
				break;
			}
		}
		/* End of Loop-ception */


		/* !! THIS IS ONLY FOR DEBUGGING, NEEDS TO BE REMOVED LATER!  !!*/
		//GameObject.Find ("Debugger").GetComponent<AStarDebugger> ().DebugPath (openList, closedList, finalPath);
		/* END OF DEBUGGING */
		return finalPath;
	}

	/* This piece of code is optional, as each enemies has its own passing technique. */
	private static bool IsDiagonal(Node currentNode, Node neighbor){
		Point direction = neighbor.GridPosition - currentNode.GridPosition;

		Point 	first = new Point (currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y),
				second = new Point (currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

		if (LevelManager.Instance.IsInBounds (first) && !LevelManager.Instance.Tiles [first].IsPassable)
			return false;

		if (LevelManager.Instance.IsInBounds (second) && !LevelManager.Instance.Tiles [second].IsPassable)
			return false;

		return true;
	}
}

/*
 * Movements:
 * 
 * - Center			: X +0, Y +0
 * - Right			: X +1, Y +0
 * - Top Right		: X +1, Y +1
 * - Bottom Right	: X +1, Y -1
 * - Down			: X +0, Y -1
 * - Left			: X -1, Y +0
 * - Bottom Left	: X -1, Y -1
 * - Top Left		: X +1, Y -1
 * - Up				: X +0, Y +1
 * 
 */
