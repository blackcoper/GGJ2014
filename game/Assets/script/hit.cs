using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour{
	public Transform explosion;
	private Transform point;
	private Quaternion explosionRotation;
	public float power = 10f;
	public float radius = 3f;
	private bool is_hit = true;
	private Collider[] objectsInRange;
	void Start(){
		//audio.PlayDelayed(1f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			point = other.transform;
			Explode ();
			//player_status targetStatus = hit.gameObject.transform.parent.GetComponent<player_status>();
			/*if(hit.gameObject.networkView!= null){
					if(hit.gameObject.networkView.isMine)hit.gameObject.networkView.RPC("receiveDamage", RPCMode.All, power, hit.gameObject.networkView.viewID);
				}else{*/
			other.GetComponent<ai_movement>().receiveDamage(power);
			//}
			//targetStatus.receiveDemage(power);
		}
	}
	void Explode()
	{
		if (point) {
			Instantiate (explosion, new Vector3(point.position.x, point.position.y, -0.5f), point.rotation);
			Destroy (gameObject, 1f);
		}
	}
}

