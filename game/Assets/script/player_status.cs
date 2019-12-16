using UnityEngine;
using System.Collections;

public class player_status : MonoBehaviour {
	public float Health = 100;
	public bool die = false;
	public GameObject frefabPlayer;
	private GameObject playerHud;
	public Transform maxHP;
	public Transform currentHP;
	public float fireRate = 5F; 
	public float fireDamage = 25f;
	public bool is_enemy = false;
	private Transform playerModel;
	void Start()
	{
		playerModel = gameObject.transform.FindChild("model");
		playerHud = gameObject.transform.FindChild("hud").gameObject;
		maxHP = playerHud.transform.FindChild("MaxHP");
		currentHP = playerHud.transform.FindChild("CurrentHP");
	}
	public void setPlayerName(string name){
		TextMesh textMesh = transform.FindChild("hud").transform.FindChild("Name").GetComponent<TextMesh>();
		textMesh.text = name;
	}
	
	public IEnumerator WaitSecond() { 
	    yield return new WaitForSeconds(5.0f);
		if(playerModel.networkView!= null){
			MultiplayerManager.spawnPos.position = transform.position+(new Vector3(0,10,0));
			if(!is_enemy && playerModel.networkView.isMine)MultiplayerManager.is_destroy = true;
		}else{
			if(!is_enemy)LevelManager.is_destroy = true;
		}
		Destroy(gameObject);
	}
	public void receiveHealth(float percent){
		Health += percent;
		if(Health>100)Health=100;
		currentHP.localScale = new Vector3(maxHP.localScale.x * Health/100,1,0.04f);
		//currentHP.localScale = new Vector3(currentHP.localScale.x - maxHP.localScale.x * percent/100,1,0.04f);
	}
}
