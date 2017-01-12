using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/* This class is not part of MonoBehaviour. */
/* The implementation can be HP, Mana, Exp, JobExp, and so on... */
/* [Serializable] is used to serialize field for non-MonoBehaviour classes. */
[Serializable]
public class Stat
{	/* Fields */
	/* The stat needs to have gauge, because every single stat should be bound to a gauge.
	 So the health can tell the bar its monitoring that it needs to be updated. */
	[SerializeField]
	private Gauge gauge;

	[SerializeField]
	private float maxValue;

	//[SerializeField]
	//private Text valueText;

	/* This is the current value of a stat. 
	 It's changeable to health, exp, mana, and so on. It's reusable. */
	[SerializeField]
	private float currentValue;

	/* Properties */
	public float CurrentValue{
		get{
			return currentValue;
		}

		set{
			//valueText.text = value + "/" + maxValue;
			/* Use clamp to restrict value from going below zero and over maxValue. */
			currentValue = Mathf.Clamp (value, 0, maxValue);
			/* Update the displayed current value, based on encapsulated value. */
			gauge.Value = currentValue;
		}
	}

	public float MaxValue{
		get{
			return maxValue;
		}

		set{
			//valueText.text = maxValue + "/" + value;
			maxValue = value;
			/* Update the displayed max value, based on encapsulated value. */
			gauge.MaxValue = maxValue;
		}
	}

	/* This makes the properties read all the serialized field's values. */
	public void Initialize(){
		this.MaxValue 		= maxValue;
		this.CurrentValue 	= currentValue;
	}


}







