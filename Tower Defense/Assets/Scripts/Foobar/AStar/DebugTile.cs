using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugTile : MonoBehaviour 
{
	/* Fields */
	[SerializeField]
	private Text 	fText,
					gText,
					hText;

	/* Properties */
	public Text FText{
		get{
			fText.gameObject.SetActive (true);
			return fText;
		}

		set{
			fText = value;
		}
	}

	public Text GText{
		get{
			gText.gameObject.SetActive (true);
			return gText;
		}

		set{
			gText = value;
		}
	}

	public Text HText{
		get{
			hText.gameObject.SetActive (true);
			return hText;
		}

		set{
			hText = value;
		}
	}
}
