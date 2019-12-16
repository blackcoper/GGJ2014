using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour 
{
	public Transform weapon;
	public ParticleEmitter smokeEmiter;
	public Transform hit_pos;
	public GameObject hit;
	public string face = "right";
	public float moveForce = 365f;	
	public float maxSpeed = 5f;
	public float jumpForce = 500f;
	//public AudioClip shootAudio;
	//private float velocity;	
	//private float x = 0.0f;
	private Joystick leftTouchPad;
	private Joystick rightTouchPad;
	private Joystick fireTouchPad;
	private Joystick jumpTouchPad;
	private float decrement = 0;
	private float nextFire = 0.0F;
	private GameObject player;
	private player_status unit_status;
	private Collider[] objectsInRange;
	private Transform groundCheck;
	private bool grounded = false;	
	public Animator animator;
	private bool is_attacking = false;
	private float delay = 0;
	private float maxdelay=0;
	public AudioClip bgm; 
	public AudioClip bgm_boss; 
	public AudioClip punch;
	public AudioClip jump;
	void Start(){
		audio.clip = bgm;
		audio.Play();
		transform.localScale = new Vector3(-1f,1f,1f);
		player = transform.gameObject;
		groundCheck = transform.Find("ground_check");
		unit_status = player.GetComponent<player_status>();
		unit_status.setPlayerName("Player 1");
		//gameObject.transform.rigidbody.centerOfMass = new Vector3(1,0,0);
		if(GameObject.Find("FireButton")!=null)fireTouchPad = GameObject.Find("FireButton").GetComponent<Joystick>();
		if(GameObject.Find("JumpButton")!=null)jumpTouchPad = GameObject.Find("JumpButton").GetComponent<Joystick>();
		if(GameObject.Find("LeftButton")!=null)leftTouchPad = GameObject.Find("LeftButton").GetComponent<Joystick>();
		if(GameObject.Find("RightButton")!=null)rightTouchPad = GameObject.Find("RightButton").GetComponent<Joystick>();
		/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if(GameObject.Find("Touchpad").gameObject!=null)GameObject.Find("Touchpad").gameObject.SetActive(false);
		#endif*/
		//gameObject.AddComponent("AudioListener");
	}
	public void playBoss()
	{
		audio.Stop();
		audio.clip = bgm_boss;
		audio.Play();
	}
	void Update(){
		if(Input.GetKeyDown("escape")){
			Application.LoadLevel("MainPage");
		}
		/*if(rigidbody2D.velocity.y!=0){
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
		}*/
		if(unit_status.die)return;
		if(transform.eulerAngles.z!=0){

			if(transform.eulerAngles.z>0&&transform.eulerAngles.z<180){
				decrement = 100*transform.eulerAngles.z/180;
				transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z-decrement);
			}
			else if(transform.eulerAngles.z>180&&transform.eulerAngles.z<360){
				float a = 360-transform.eulerAngles.z;
				decrement = 100*a/180;
				transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z+decrement);
			}
		}
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
	}
	void FixedUpdate ()
	{
		if(unit_status.die)return;
			float h = 0;
			//float v = 0;
			h = Input.GetAxis("Horizontal");
			//v = Input.GetAxis("Vertical");
			if(rightTouchPad.isFingerDown)h = 1;
			if(leftTouchPad.isFingerDown)h = -1;
			delay += Time.deltaTime;
			if (delay > maxdelay) {
				is_attacking = false;
				delay = 0;
				if(grounded){animator.Play("anais_idle");}
				else{
					delay = 0;
					maxdelay = 0.3f;
				}
			}
			if(h!=0){
				if(h<0){
					if(h * rigidbody2D.velocity.x < maxSpeed)
						rigidbody2D.AddForce(Vector2.right * h * moveForce);
					if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
						rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
					if(grounded && !is_attacking)animator.Play("anais_walk");
					if(face.Equals("left")){
						face = "right";
						transform.localScale = new Vector3(1f,1f,1f);
					}
				}else{
					if(h * rigidbody2D.velocity.x < maxSpeed)
						rigidbody2D.AddForce(Vector2.right * h * moveForce);
					if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
						rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
					if(grounded && !is_attacking)animator.Play("anais_walk");	
					if(face.Equals("right")){
						face = "left";
						transform.localScale = new Vector3(-1f,1f,1f);
					}
				}
				if(grounded){smokeEmiter.emit = true;}else{
					smokeEmiter.emit = false;
				}
			}else{
				smokeEmiter.emit = false;
			}
			/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
			// Using Mouse
			Vector3 mouse_pos = Input.mousePosition;
			mouse_pos.z = 5.23f; 
			Vector3 object_pos = Camera.main.WorldToScreenPoint(weapon.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;
			float _angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
			weapon.rotation = Quaternion.Euler(0, 0, _angle);
			
			//shoot
			if (Input.GetButtonUp("Fire1") && Time.time > nextFire)
			{
				nextFire = Time.time + unit_status.fireRate;
				//AudioSource.PlayClipAtPoint(shootAudio, fire_pos.transform.position,1);
				//GameObject bulletObj = (GameObject)Instantiate(bullet, fire_pos.transform.position, fire_pos.transform.rotation);
				//bulletObj.GetComponent<Explosion>().power = unit_status.fireDamage;
				//bulletObj.rigidbody.velocity = fire_pos.transform.TransformDirection(Vector3.right * speed);
				//Destroy(bulletObj, 15f);
			}
			
			// flip the car
			if ( Input.GetButton("Jump"))
			{
				float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, 0);
				if (Mathf.Abs(deltaAngle) > 10)
				{
					rigidbody.AddTorque(0, 0, deltaAngle * deltaAngle * deltaAngle, ForceMode.VelocityChange);
				}
			}	
			#else*/
			//shoot
		//(Input.GetButton("Fire1")&& Time.time > nextFire) ||
		if ( (fireTouchPad.isFingerDown && Time.time > nextFire))
			{
				is_attacking = true;
				delay = 0;
				maxdelay = 0.7f;
				nextFire = Time.time + unit_status.fireRate;
				animator.Play("anais_attack");
				AudioSource.PlayClipAtPoint(punch, transform.position,1);
				if(hit_pos.transform){
					GameObject hitObj = (GameObject)Instantiate(hit, hit_pos.transform.position, hit_pos.transform.rotation);
					//hitObj.GetComponent<Explosion>().power = unit_status.fireDamage;
					Destroy(hitObj, 0.2f);
				}
			}

		if ((Input.GetButton("Jump") && grounded) || (jumpTouchPad.isFingerDown && grounded))
			{
				delay = 0;
				maxdelay = 0.3f;
				animator.Play ("anais_jump");
				AudioSource.PlayClipAtPoint(jump, transform.position,1);
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			}
			//#endif
		
	}
	bool isGrounded(GameObject obj) {
		return Physics.Raycast(obj.transform.position, -Vector3.up, obj.GetComponent<SphereCollider>().bounds.extents.y + 1f);
	}
	float ClampAngle (float angle, float min, float max ) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
	public void receiveDamage(float percent){
		unit_status.Health -= percent;
		gameObject.transform.FindChild("hud").active = true;
		unit_status.currentHP.localScale = new Vector3(unit_status.maxHP.localScale.x * unit_status.Health/100,1,0.04f);
		if (unit_status.Health <= 0) {

						unit_status.currentHP.localScale = new Vector3 (0, 1, 0.04f);
						if (!unit_status.die) {
								animator.Play ("anais_dead");
								//audio.Play();
								unit_status.Health = 0;
								unit_status.die = true;
								StartCoroutine (unit_status.WaitSecond ());
						}
						
				} else {
					animator.Play("anais_hurt");
				}
	}
} 
