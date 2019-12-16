using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Debug.Log("dapat darah");
			other.gameObject.GetComponent<player_status>().receiveHealth(20);
			Destroy(gameObject,0.1f);
		}
	}
}
