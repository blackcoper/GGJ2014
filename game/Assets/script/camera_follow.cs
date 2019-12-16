using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour 
{
	public Transform FocusTarget;
	public float limitMinX = -178.25f;
	public float limitMinY;
	public float limitMaxX = 163.15f;
	public float limitMaxY;
	public GameObject parallax01;
	public GameObject parallax02;
	private bool limited = false;
	void Update () 
	{
		if(FocusTarget == null)return;
		Vector3 newPos = FocusTarget.position;
		limited = false;
		if(newPos.x<=limitMinX){
			limited = true;
			newPos.x = limitMinX;
		}else if(newPos.x>=limitMaxX){
			limited = true;
			newPos.x = limitMaxX;
		}
		newPos.z = transform.position.z;
		//newPos.y = newPos.y;
		Vector3 oldPos = transform.position;
		float delta = 0.1f;
		transform.position = (1 - delta) * oldPos + delta * newPos;
		if (!limited) {
			/*Debug.Log(oldPos.x+", "+newPos.x);
			if (Mathf.Floor(oldPos.x) > Mathf.Floor(newPos.x)) {
						parallax01.renderer.material.mainTextureOffset = new Vector2 (Time.deltaTime * newPos.x - oldPos.x * 0.005f, 0);
						parallax02.renderer.material.mainTextureOffset = new Vector2 (Time.deltaTime * newPos.x - oldPos.x * 0.010f, 0);
			} else if (Mathf.Floor(oldPos.x) < Mathf.Floor(newPos.x)) {
						parallax01.renderer.material.mainTextureOffset = new Vector2 (Time.deltaTime * newPos.x - oldPos.x * 0.005f, 0);
						parallax02.renderer.material.mainTextureOffset = new Vector2 (Time.deltaTime * newPos.x - oldPos.x * 0.010f, 0); 
			}*/
		}
	}
}
