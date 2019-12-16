using UnityEngine;
using System.Collections;

public class ai_movement : MonoBehaviour {
	public int state = 0;
	public int laststate = 0;
	public Vector2 area;
	public Vector2 position;
	public float maxDelay = 2;
	public float delay = 0;

	public Animator animator;
	public bool is_walk = false;
	public bool is_idle = true;
	
	//public Transform heart;		
	private GameObject player;
	private enemy_status unit_status;

	private float nextFire = 0.0F;
	public GameObject hit_enemy;
	public Transform hit_pos;

	public string idle_anim="monster1_idle";
	public string walk_anim="monster1_walk";
	public string dead_anim="monster1_dead";
	public string attack_anim="monster1_attack";
	public string hurt_anim="monster1_hurt";
	public GameObject loot;
	public AudioClip punch;
	public bool is_boss = false;
	public Transform boss_spawn;
	public GameObject boss;
	void Start() {
		area = new Vector2 (transform.position.x, transform.position.y);
		player = transform.gameObject;
		unit_status = player.GetComponent<enemy_status>();
		//unit_status.setPlayerName("Shadow 1");
	}
	public void receiveDamage(float percent){
		unit_status.Health -= percent;
		gameObject.transform.FindChild("hud").active = true;
		unit_status.currentHP.localScale = new Vector3(unit_status.maxHP.localScale.x * unit_status.Health/100,1,0.04f);
		if (unit_status.Health <= 0) {
			unit_status.currentHP.localScale = new Vector3 (0, 1, 0.04f);
			if (!unit_status.die) {
				//audio.Play();
				gameObject.transform.FindChild("hud").active = false;
				Instantiate (loot, transform.position, transform.rotation);
				animator.Play (dead_anim);
				unit_status.Health = 0;
				unit_status.die = true;
				StartCoroutine (unit_status.WaitSecond ());
				if(!is_boss){
					if(GameObject.FindGameObjectsWithTag("Enemy").Length<=1){ 
						GameObject a = GameObject.FindGameObjectWithTag("Player").gameObject;
						player_controller b = a.GetComponent<player_controller>();
						b.playBoss();
						Instantiate (boss, boss_spawn.position, boss_spawn.rotation);
					}
				}else{
					GameObject.FindGameObjectWithTag("MainCamera").transform.FindChild("win").gameObject.SetActive(true);
				}
			}
		} else {
			animator.Play (hurt_anim);
		}
	}
	void Update () {
		if(unit_status.die)return;
		if(state==0){
			position.x = Random.Range(area.x-1.0F, area.x-5.0F);
			state = 3;
		}else if(state==1){
			position.x = area.x;
			state = 4;
		}else if(state==2){
			position.x = Random.Range(area.x+1.0F, area.x+5.0F);
			state = 5;
		}else{
			if(state == 3){
				if(position.x<=transform.localPosition.x){
					if(!is_walk){
						is_idle = false;
						is_walk = true;
						animator.Play(walk_anim);
						transform.localScale = new Vector3(1.2f,1.2f,1.2f);
					}
					transform.localPosition = new Vector3(transform.localPosition.x-0.1f,transform.localPosition.y,0);
				}else{
					if(!is_idle){
						is_idle = true;
						is_walk = false;
						maxDelay = Random.Range(1,3);
						animator.Play(idle_anim);
						transform.localScale = new Vector3(1.2f,1.2f,1.2f);
					}
					delay+=Time.deltaTime; 
					if(delay>maxDelay){
						state = 1;
						delay = 0;
						laststate = 0;
					}
				}
			}else if(state == 4){
				if (laststate == 0){
					if(position.x>=transform.localPosition.x){
						if(!is_walk){
							is_idle = false;
							is_walk = true;
							animator.Play(walk_anim);
							transform.localScale = new Vector3(-1.2f,1.2f,1.2f);  
						}
						transform.localPosition = new Vector3(transform.localPosition.x+0.1f,transform.localPosition.y,0);
					}else {
						if(!is_idle){
							is_idle = true;
							is_walk = false;
							maxDelay = Random.Range(1,3);
							animator.Play(idle_anim);
							transform.localScale = new Vector3(1.2f,1.2f,1.2f);
						}
						delay+=Time.deltaTime; 
						if(delay>maxDelay){
							state = 2;
							delay = 0;
						}
					}
				}else{
					if(position.x<=transform.localPosition.x){
						if(!is_walk){
							is_idle = false;
							is_walk = true;
							animator.Play(walk_anim);
							transform.localScale = new Vector3(1.2f,1.2f,1.2f);
						}
						transform.localPosition = new Vector3(transform.localPosition.x-0.1f,transform.localPosition.y,0);
					}else{
						if(!is_idle){
							is_idle = true;
							is_walk = false;
							maxDelay = Random.Range(1,3);
							animator.Play(idle_anim);
							transform.localScale = new Vector3(1.2f,1.2f,1.2f);
						}
						delay+=Time.deltaTime; 
						if(delay>maxDelay){
							state = 0;
							delay = 0;
						}
					}
				}
			}else{
				if(position.x>=transform.localPosition.x){
					if(!is_walk){
						is_idle = false;
						is_walk = true;
						animator.Play(walk_anim);
						transform.localScale = new Vector3(-1.2f,1.2f,1.2f);
					}
					transform.localPosition = new Vector3(transform.localPosition.x+0.1f,transform.localPosition.y,0);
				}else {
					if(!is_idle){
						is_idle = true;
						is_walk = false;
						maxDelay = Random.Range(1,3);
						animator.Play(idle_anim);
						transform.localScale = new Vector3(1.2f,1.2f,1.2f);
					}
					delay+=Time.deltaTime; 
					if(delay>maxDelay){
						state = 1;
						delay = 0;
						laststate = 2;
					}
				}
			}
			if(area.y!=transform.localPosition.y){
				if(area.y<transform.localPosition.y){
					transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y-0.01f,0);
				}else{
					transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y+0.01f,0);
				}
			}
			
			if(transform.eulerAngles.z>0&&transform.eulerAngles.z<180){
				float decrement = 100*transform.eulerAngles.z/180;
				transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z-decrement);
			}
			else if(transform.eulerAngles.z>180&&transform.eulerAngles.z<360){
				float a = 360-transform.eulerAngles.z;
				float decrement = 100*a/180;
				transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z+decrement);
			}

			if(rigidbody2D.velocity.y!=0){
				if(rigidbody2D.velocity.y<0){
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y +1f);
				}else{
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y -1f);
				}
			}
			if(rigidbody2D.velocity.x!=0){
				if(rigidbody2D.velocity.x<0){
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x+1f, rigidbody2D.velocity.y);
				}else{
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x-1f, rigidbody2D.velocity.y);
				}
			}
		}
		if(Time.time > nextFire){
			nextFire = Time.time + unit_status.fireRate;
			if(hit_pos.transform){
				animator.Play(attack_anim);

				GameObject hitObj = (GameObject)Instantiate(hit_enemy, hit_pos.transform.position, hit_pos.transform.rotation);
				hitObj.GetComponent<enemy_hit>().punch = punch;
				Destroy(hitObj, 0.2f);
			}
		}
		//heart.position = new Vector3(transform.position.x,transform.position.y, transform.position.z+0.4f);
	}
}