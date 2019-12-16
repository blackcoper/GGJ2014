using UnityEngine;
using System.Collections;

public class close_credit : MonoBehaviour {
	public string jumpTo;
	// Use this for initialization
	void Start () {

	}

	void OnMouseDown(){
		AutoFade.LoadLevel(jumpTo ,2,1,Color.black);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
