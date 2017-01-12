using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	/* This array will contain all the prefabs. */
	[SerializeField]
	private GameObject[] objectPrefabs;

	/* Create a collection */
	private List<GameObject> pooledObjects = new List<GameObject> ();

	/* From the objectPrefabs array, create a monster based on its type. */
	public GameObject GetObject(string type){
		foreach (GameObject go in pooledObjects) {
			if(go.name == type && !go.activeInHierarchy){
				go.SetActive(true);
				return go;
			}
		}

		/* Use this loop only when there is no other object to be reused! */
		for (int i = 0; i < objectPrefabs.Length; i++) {
			if(objectPrefabs[i].name == type){
				GameObject newObject = Instantiate(objectPrefabs[i]);
				pooledObjects.Add(newObject);
				newObject.name = type;
				return newObject;
			}
		}

		/* If there is no selected monster available, then return null. */
		return null;
	}

	public void ReleaseObject(GameObject gameObject){
		gameObject.SetActive (false);
	}
}
