using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {

	// Use this for initialization
	public GameObject slide1;
	public GameObject slide2;
	public GameObject slide3;

	void Start () {
		slide1.SetActive (true);
		slide2.SetActive (true);
		slide3.SetActive (true);
		StartCoroutine ("changeIntro1");
	}
	IEnumerator changeIntro1(){
		yield return new WaitForSeconds (5f);
		StartCoroutine ("changeIntro2");
		slide1.SetActive (false);
	}
	IEnumerator changeIntro2(){
		yield return new WaitForSeconds (5f);
		StartCoroutine ("changeIntro3");
		slide2.SetActive (false);
	}
	IEnumerator changeIntro3(){
		yield return new WaitForSeconds (5f);
		Application.LoadLevel("game");
	}
}
