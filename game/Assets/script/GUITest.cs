using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	//style that can be edited from Inspector
	[SerializeField]
	private GUIStyle customStyle;
	GUISizer.GUIParams button1 = new GUISizer.GUIParams(GUISizer.PositionDef.middle, "Middle");
	// Use this for initialization
	GUISizer.GUIParams label1 = new GUISizer.GUIParams(GUISizer.WIDTH/2 - GUISizer.MEDIUM_BUTTON_WIDTH/2, GUISizer.HEIGHT/2 - GUISizer.MEDIUM_BUTTON_HEIGHT, GUISizer.MEDIUM_BUTTON_WIDTH, GUISizer.MEDIUM_BUTTON_HEIGHT, "Pressed: none");
	
	private string cachedText = "none";
	void AppendLabel(string text)
	{
		//remove the previously appended text
		//if (label1.text.EndsWith(cachedText))
		//{
		//	label1.text = label1.text.Substring(0, label1.text.LastIndexOf(cachedText));
		//}
		
		//label1.text += text;
		
		//cachedText = text;
		
		//cube.rigidbody.AddForce(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f));
	}


	void Start () {
		
	}

/*	void OnGUI(){
		GUISizer.BeginGUI(GUISizer.PositionDef.middle);
		
		if (GUISizer.ButtonPressed(button1))
		{
			Camera.main.backgroundColor = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			//AppendLabel(button1.text);
		}
		
		GUISizer.MakeLabel(label1, 20, customStyle);
		
		GUISizer.EndGUI();
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
