using UnityEngine;
using System.Collections;

/* 
 	This will be a parent class for every single class that is going to be turned into Singleton.
	Meaning, every single class will be inherited from Singleton class.

	This class will be superclass and never be existed on its own.
	So, this class will be abstract.

	Abstract class is general term of a class (more like specialization).

	Example:
	The abstract class (parent) is Animal.
	Then the child class is Lion, Zebra, Snake, Hippo, Cat, etc.
	
 */
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

	/* Create a static instance so we can access another class. */
	/* This is field. */
	private static T instance;

	/* This is property. */
	public static T Instance{
		get
		{
			if(instance == null)
				instance = FindObjectOfType<T>();

			return instance;
		}
	}

	/* Please bear in mind that fields begin in lowercase, while properties in uppercase. */

	/*
		When something is 'static', it is accessible on the class level.
		Which means, we can access from the class name.
	 */
}
