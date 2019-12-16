using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	public float timer = 2f;
	public string levelToload;
	private GUITexture myGUITexture; 
	private Transform  trans_form;

	void Awake(){
		trans_form = new GameObject().transform;
	}

	void Start () {
		StartCoroutine ("DisplayScene");
	}

	
	/*IEnumerator DisplayScene2(){ //splash screen 2
		yield return new WaitForSeconds(timer);
		string tempJump = levelToload;
		levelToload = variable.pageJump;
		//Application.LoadLevel (levelToload);
		AutoFade.LoadLevel(tempJump ,3,1,Color.black);

	}*/

	IEnumerator DisplayScene(){ //splash screen 1
		yield return new WaitForSeconds(timer);
		AutoFade.LoadLevel (levelToload, 1, 1, Color.black);
	}
	
}
