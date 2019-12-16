using UnityEngine;
using System.Collections;

public class clickMenu : MonoBehaviour {

	public string jumpTo = "MainPage1";

	void OnMouseDown(){
		//StartCoroutine ("DisplayScene");
		if(jumpTo.Equals("quit")){
			Application.Quit();
		}else{
			AutoFade.LoadLevel(jumpTo ,2,1,Color.black);
		}
	}
}
