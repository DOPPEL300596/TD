using UnityEngine;
using System;
using System.Collections;

public class Node
{
	/* Fields */
	/* Properties */
	/* 1. Create a GridPosition. */
	public Point GridPosition {
		get;

		private set;
	}

	/* Create a WorldPosition */
	public Vector2 WorldPosition {
		get;

		private set;
	}

	/* 2. Create a TileScript. 
	 This needs to be done because each node is placed on top of a tile,
	 for convenience each one of the node needs to have reference to each tile. */
	public TileScript TileRef {
		get;

		private set;
	}

	/* 4. Create a node inside a node, as the algorithm is basically backtracking. */
	public Node Parent {
		get;

		private set;
	}
	/* We only intend to set from only inside the script, hence the private set. */

	/* Create a score of F(X) */
	public int FCost {
		get;
		
		set;
	}

	/* Create a score of G(X) */
	public int GCost {
		get;

		set;
	}

	/* Create a score of H(X) */
	public int HCost {
		get;

		set;
	}

	/* Heuristic values = F and H */

	/* 3. Create a constructor */
	public Node(TileScript tileRef){
		/* Everytime a node is created, it has reference to each tile. */
		this.TileRef 		= tileRef;
		this.GridPosition 	= tileRef.GridPosition;
		this.WorldPosition 	= tileRef.WorldPosition;
	}

	/* 5. Calculate values of F, G, and H at some point... */
	public void Calculate(Node parent, Node goal, int gCost){
		this.Parent = parent;
		this.GCost = parent.GCost + gCost;

		/* H value */
		this.HCost = (Math.Abs(GridPosition.X - goal.GridPosition.X) + Math.Abs(goal.GridPosition.Y - GridPosition.Y))  * 10;
		this.FCost = GCost + HCost;
	}
}

/**
 * Coordinates:
 * 
 * Top-Left corner 		= 0,0
 * Next to it			= 1,0
 * .
 * .
 * .
 * and so on, until 
 * Bottom-Right corner 	= n,n
 * 
 */