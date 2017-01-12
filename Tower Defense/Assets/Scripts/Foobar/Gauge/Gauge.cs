using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gauge : MonoBehaviour {
	/* Fields */
	/* The value ranges between 0 to 1, and will always be float. */
	private float fillAmount;

	/* This field is to determine how fast the value will increase or decrease. */
	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Text valueText;

	[SerializeField]
	private Image content;

	[SerializeField]
	private bool lerpColor; 

	/* These two fields determine which color indicates when a bar is at low, high, or max value. */
	[SerializeField]
	private Color 	fullColor,
					lowColor;
	
	/* Properties */
	/* The fillAmount needs to be float so it can reflects the visual display
	 to the real gauge. */
	public float FillAmount{
		get
		{
			/* Lines below are to clamp value of the fillAmount. */
			if(fillAmount >= 1f)
				fillAmount = 1f;
			
			if(fillAmount <= 0f)
				fillAmount = 0f;
			
			return fillAmount;
		}
		
		set{
			fillAmount = value;
		}
	}

	/* Maximum value of anyone's health.
	 This later can be implemented with separate class. */
	public float MaxValue {
		get;

		set;
	}
	 
	/* Normal value of anyone's health. */
	public float Value {
		set{
			valueText.text = value + "/" + MaxValue;
			fillAmount = Map(value, 0, MaxValue, 0, 1);
		}
	}

	// Use this for initialization
	void Start () {
		if (lerpColor)
			content.color = fullColor;
	}
	
	// Update is called once per frame
	/* This function will upgrade gauge and its inner value over time. */
	void Update () {
		HandleGauge ();
	}
	
	/* This function handles the content of gauge constantly. */
	private void HandleGauge(){
		/* If the content of fillAmount is different, then update. */
		if (fillAmount != content.fillAmount) {
			/* Within the script, the FillAmount will be manipulated. */
			/* Using Lerp, the content will decrease gradually as if there is an animation. */
			content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
		}

		if(lerpColor)
			content.color = Color.Lerp (lowColor, fullColor, fillAmount);
	}

	/* This function will return a value between zero and one,
	 depending on how much points or health you have.*/
	private float Map(float value, float inMin, float inMax, float outMin, float outMax){
		return ((value - inMin) * (outMax - outMin)) / (inMax - inMin) + outMin;
	}

	/* Param note:
	 - float value = the actual value
	 - float inMin = the minimum value of a gauge can take
	 - float inMax - the maximum value of a gauge can take

	Based on inMin and inMax, the return value will be a percentage which value varies between zero to one,
	or zero percent to one hundred percent.
	- outMin should be 0
	- outMax should be 1
	 */
}






