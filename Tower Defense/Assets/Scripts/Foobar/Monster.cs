using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
	/* Fields */
	/* This field is to determine how fast the monster will walk. */
	[SerializeField]
	private float speed;

	/* Speed will later on be a property, because some tower
	 or items may affect the movement speed of each monster. */

	/* Each monster have their own path. */
	private Stack<Node> path; 

	/* Destination of a monster. */
	private Vector3 destination;

	/* Properties */
	/* This will be used for A* algorithm. */
	public Point GridPosition {
		get;

		set;
	}

	/* IsActive is going to decide whether a monster is moving or not. */
	public bool IsActive {
		get;

		set;
	}


	/* OVERIDDEN METHOD. */
	private void Start(){
		
	}

	private void Update(){
		Move ();
	}

	private void OnMouseDown(){
		Debug.Log ("Clicked.");
	}

	/* END OF OVERRIDDEN METHOD. */

	/* This function spawns a monster. 
	 This function will also need an exact location of spawning. */
	public void Spawn(){
		transform.position = LevelManager.Instance.InitPortal.transform.position;
		
		StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1), false));
		
		/* This is where SetPath() is called. */
		SetPath (LevelManager.Instance.FinalPath);
	}
	
	/* This function will create an effect when a monster is entering or exiting the stage. 
	 Both parameter can be either larger or smaller than the other, as this function complements both
	 effects of entering and exiting. */
	public IEnumerator Scale(Vector3 scaleFrom, Vector3 scaleTo, bool remove){
		/* Initially, the monster is inactive. */
		//IsActive = false; 
		
		float progress = 0;
		/* As long as the progress is not larger than 1, then scaling process is starting. 
		 If the progress reaches one, the stop the scaling process. */
		
		/* Lerp = linear interpolation 
		 Lerp is used to change value OVER TIME! 

		 The implementation not only for time trial or scaling, but also status effects such as poison, bleeding, etc.*/
		while (progress <= 1) {
			transform.localScale = Vector3.Lerp (scaleFrom, scaleTo, progress);
			
			/* Adding time all the time. */
			progress += Time.deltaTime;
			
			yield return null;
		}
		
		/* Progress = 0, then start scaling
		 * Progress = 1, then stop scaling*/
		
		transform.localScale = scaleTo;
		
		/* When done scaling, set the monster status to active. */
		IsActive = true;
		
		/* When the monster reaches the final point, destroy it. */
		if (remove)
			Release ();
	}

	/* This function will move the monster. */
	private void Move(){
		/* If the monster is active, then... */
		if (IsActive) {
			/* The movement will be from tile to tile. */
			transform.position = Vector2.MoveTowards (transform.position, destination, speed * Time.deltaTime);

			/* Vector2.MoveTowards() can be used to move towards a certain target. */

			if (transform.position == destination) {
				/* If path count is larger than zero, the movement will continue. */
				if (path != null && path.Count > 0) {
					/* Peek doesn't remove, but still retrieve the final element of a stack. */
					GridPosition = path.Peek ().GridPosition;

					/* Here comes the important part... */
					destination = path.Pop ().WorldPosition;
			
				}
			}
		}
	}

	/* Set movement path of a monster. 
	 This function will be executed as soon as the monster is spawned. */
	private void SetPath(Stack<Node> newPath){
		if (newPath != null) {
			this.path = newPath;

			GridPosition 	= path.Peek ().GridPosition;
			destination		= path.Pop	().WorldPosition;
		}
	}

	/* This function will be used to control animation of a monster. 
	 Depending on how much sprite you have, this will be relatively easy to hard. */
	private void Animate(Point currentPosition, Point newPosition){
		/* Moving down */
		if (currentPosition.Y > newPosition.Y) {
			
		}
		/* Moving up */
		else if (currentPosition.Y < newPosition.Y) {
			
		}

		/* This indicates that the monster is not moving anywhere vertical,
		 which now is horizontal. */
		if (currentPosition.Y == newPosition.Y) {
			/* Moving left */
			if (currentPosition.X > newPosition.X) {
				
			}

			/* Moving right */
			else if (currentPosition.X < newPosition.X) {
				
			}
		}
	}

	/* Note:
	 * A = current position
	 * B = new position
	 * 
	 * 1. A.Y > B.Y => moving down
	 * 2. A.Y < B.Y => moving up
	 * 3. A.X > B.X => moving left
	 * 4. A.X < B.X => moving right
	 */

	private void OnTriggerEnter2D(Collider2D other){
		/* If monster hits the final point, then something specific must be done. */
		if (other.tag == "FinalPoint")
			//Debug.Log ("Entering another dimensions...");
			StartCoroutine (Scale (new Vector3 (1, 1), new Vector3 (0.1f, 0.1f), true)); 
	}

	/* Create a new function to re-use the monster. */
	/* Call this method whenever a monster is hitting the final point. 
	 This will also set the normal condition of a monster, including health bar. */
	private void Release(){
		IsActive = false;
		GridPosition = LevelManager.Instance.InitPoint;
		GameManager.Instance.Pool.ReleaseObject (gameObject);
	}


}




