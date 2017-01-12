using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : Singleton<LevelManager>
{
	/*
	 * In general, in OOP, it's a very bad practice to make your fields 'public'.
	 * 
	 * If something is public, everything and everywhere can reach and manipulate
	 * especially when two or more people code at the same time,
	 * 
	 * If the other scripts or classes don't need to have access to another class/script,
	 * then make it private.
	 * Sometimes people tend to change the value by mistake.
	 * 
	 */
	[SerializeField]

	/* Create an array of tiles */
	private GameObject[] tilePrefabs;

	[SerializeField]

	/* Add new component (object) of CameraMovement */
	private CameraMovement cameraMovement;

	[SerializeField]
	/* Map is a parent of tiles. */
	private Transform map;

	/* Decide where the initial access point will be placed. */
	private Point 	initPoint,
					finalPoint;
	/* These two are needed for pathfinding. */

	[SerializeField]
	private GameObject 	initPortalPrefab,
						finalPortalPrefab;
	/* Both are prefabs */

	public Portal InitPortal {
		get;

		set;
	}

	/* Map Size*/
	private Point mapSize;

	private Stack<Node> finalPath;

	/* Dictionary for tiles */
	/* Declared, but not instantiated yet. */
	public Dictionary<Point, TileScript> Tiles { get; set;}


	/*
		In order to make each tile placed one after another,
		use width and height;

		Steps:
		1. Get component of tile
		2. Get size of sprite

		By doing this, the value is automatically generated.
	 */

	/* Don't use int! Use float to make a precise location of each tile! */
	public float TileSize
	{
		/* The first tile is a reference for size */
		get {return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;}
	}

	public Stack<Node> FinalPath{
		get
		{
			if(finalPath == null)
				GeneratePath();

			/* This line creates a new stack of nodes for each monster,
			 which monster can use to walk through the game world.*/
			return new Stack<Node>(new Stack<Node>(finalPath));
		}
	}

	public Point InitPoint{
		get
		{
			return initPoint;	
		}

		set
		{
			initPoint = value;
		}
	}

	public Point FinalPoint{
		get
		{
			return finalPoint;
		}
		
		set
		{
			finalPoint = value;
		}
	}
	// Use this for initialization
	void Start () {
		CreateLevel ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* This function will be used to create levels in-game. */
	private void CreateLevel(){
		/* Before anything else... instantiate the Dictionary */
		Tiles = new Dictionary<Point, TileScript> ();


		/*
			To auto-generate tiles, we need to know the X and Y axis.
			Use nested for loops to do so.
		 */

		string[] mapData = ReadLevelText ();

		/* Determine Map Size */
		mapSize = new Point (mapData [0].ToCharArray ().Length, mapData.Length);

		int row = mapData.Length,					/* Y axis */
			col = mapData[0].ToCharArray().Length;	/* X axis */

		/* Initialize local variable so it's not null */
		Vector3 maxTile = Vector3.zero;

		/* Initial position */
		Vector3 start = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		/* Nested loops */
		for (int i = 0; i < row; i++) {
			char[] newTiles = mapData[i].ToCharArray();

			for(int j = 0; j < col; j++) {
				/* This way, we can actually know the maximum tile of each level. */
				PlaceTile (newTiles[j].ToString(), i, j, start);
			}
		}

		/* Since we're using dictionary, we don't need to use return value anymore.*/
		maxTile = Tiles[new Point(col - 1, row - 1)].transform.position;

		/* I literally said 'Oh, it actually worked.' 
		 	Nevertheless, a success is a success.  */
		cameraMovement.SetCameraBounds (new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

		/* Spawn Portal */
		SpawnPoints ();
	}

	/* Instantiate a piece of tile, then re-place it.*/
	private void PlaceTile(string tileType, int i, int j, Vector3 start){
		/* Read which tile will be placed */
		int tileIndex = int.Parse (tileType);

		/* Make reference so tile can be placed accordingly.
		 * Create a new tile based on tileIndex
		 */
		TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		/*
			Do not confused between GetComponent() and GetComponents()!
		 */

		/* To automatically place the tile, use 'transform'.
			
					Transformable(s):
					- position
					- rotation
					- scale
					- etc. 
				 */
		
		/* Vector 3 parameter(s): width (row) , height (col) , depth (Z axis)*/
		/* It finally worked! */
		newTile.Setup (new Point (j, i), new Vector3(start.x + (TileSize * j), start.y - (TileSize * i),  0), map); 


	}

	/* Read map data from text */
	private string[] ReadLevelText(){
		/*
		 * Steps:
		 * 
		 * 1. Load data from text using TextAsset class.
		 */

		/* Keyword 'as' is used for casting. */
		TextAsset bindData = Resources.Load ("Level") as TextAsset;

		/* Replace each newlines with nothing, as in removing it.*/
		string tmp = bindData.text.Replace ("\n", String.Empty);

		/* Return the value while 'splitting' each part when '-' shows up. */
		return tmp.Split ('-');
	}

	/* This function spawns both initial and final portal of the game. */
	private void SpawnPoints(){
		/* The coordinate of initial point. */
		initPoint = new Point (0, 1);

		/* In order to instantiate both initial and final point, we need to create a prefab. */
		GameObject initPortal = (GameObject) Instantiate (initPortalPrefab, Tiles [initPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
		/* Quartenion.identity = the object cannot be rotated. */
		InitPortal = initPortal.GetComponent<Portal>();
		InitPortal.name = "Initial Portal";

		/* The coordinate of final point. */
		finalPoint = new Point (11, 6);
		
		/* In order to instantiate both initial and final point, we need to create a prefab. */
		Instantiate (finalPortalPrefab, Tiles [finalPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
		/* Quartenion.identity = the object cannot be rotated. */

	}

	/* This function will test if the tile is bound. */
	public bool IsInBounds(Point position){
		return 	(position.X >= 0 && position.Y >= 0) && 
				(position.X < mapSize.X && position.Y < mapSize.Y);
	}

	public void GeneratePath(){
		/* Create reference of final path. */
		finalPath = AStar.GetPath (initPoint, finalPoint);
	}
}

/**
 * Rule:
 * 
 * 1. The tower can be built while enemies are spawning, or
 * 2. The tower can only be built when cooldown time occurs.
 * 
 */