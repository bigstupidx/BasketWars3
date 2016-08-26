using UnityEngine;
using System.Collections;

public class BossTank : MonoBehaviour {
	public Transform bullet;
	public Transform cannon;
	public Transform wheel1;
	public Transform wheel2;
	public Transform wheel3;
	public Transform wheel4;
	public Transform wheel5;
	public Transform wheel6;
	public Transform wheel7;
	public Transform spawnPoint;

	public Animator tread;
	public Animator backing;
	public Animator explosion;
	public Animator spark;
	public Animator smoke1;
	public Animator smoke2;
	public Animator smoke3;
	public Animator smoke4;
	
	private bool isMoving;
	private float dampTime;
	private Vector3 moveVelocity;
	private Vector3 toPosition;
	private float distance;
	
	private float rotateVelocity;
	private float toWheelAngle;
	private float currentWheelAngle;
	
	private float currentGunAngle;
	private float toGunAngle;
	private int totalStep;
	private int rotateStep;
	private bool rotateUp;
	private float deltaDegree;
	private float upAngle = -45;
	private float downAngle = -9;
	
	private Transform[] bullets;
	private int bulletsIndex;
	
	private float pauseInterval;
	private int pauseTimes;
	private int currentPause;
	private int bulletCount;
	private Animator anim;
	private bool didExploded;
	public AudioSource m_boss_hum;
	public BossHealthBarHandler m_boss_health_bar;
	

	bool m_damaged = false;

	// Use this for initialization
	void Start () {
		didExploded = false;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		
		isMoving = false;
		moveVelocity = Vector3.zero;
		rotateVelocity = 0;
		dampTime = 1f;	
		
		totalStep = 12;
		rotateStep = 12;
		pauseTimes = 3;
		currentPause = 0;
		bulletCount = 3;
		pauseInterval = 0.9f;
		
		currentGunAngle = downAngle;
		rotateUp = true;
		deltaDegree = Mathf.Abs(upAngle - downAngle) / (totalStep*pauseTimes);
		
		currentWheelAngle = 0f;
		
		bulletsIndex = 0;
		bullets = new Transform[12];
		for (int i = 0 ; i < 12 ; i++) {
			bullets[i] = (Transform) Instantiate(bullet, new Vector3 (0f, -9f, 0), Quaternion.identity);
		}
		
		distance = -2f;
		InvokeRepeating ("movingCar", 4f, 5f);
		
		InvokeRepeating ("doRotate", 2f, 6f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
		
		if (GameManager.m_level_complete && !didExploded) {
			didExploded = true;
			explosion.Play("BunkerExplosion");
			//explosion.animationCompleteDelegate = ExplosionCompleteDelegate;
			
			CancelInvoke ("movingCar");
			CancelInvoke ("doRotate");
		}

		if (GameManager.m_boss_life < 3 && !m_damaged) {
			m_damaged = true;
			anim.Play("KU_TankBase_Damaged");
			backing.Play("KU_TankBack_Damaged");
			smoke1.transform.GetComponent<Renderer>().enabled = true;
			smoke2.transform.GetComponent<Renderer>().enabled = true;
			smoke3.transform.GetComponent<Renderer>().enabled = true;
			smoke4.transform.GetComponent<Renderer>().enabled = true;
			
			smoke1.Play ("Smoke");
			smoke2.Play ("Smoke");
			smoke3.Play ("Smoke");
			smoke4.Play ("Smoke");
		}

		// Rotate the gun and fire
		if (rotateStep < totalStep) {
			float diff = currentGunAngle - toGunAngle;
			if (diff > 0f) {
				currentGunAngle -= deltaDegree;
				cannon.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			else if (diff < 0f) {
				currentGunAngle += deltaDegree;
				cannon.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			
			if (rotateStep == 12) 
			{
				currentPause++;
				StartCoroutine( shootBulletRoutine() );
			}
		}
		
		if (didExploded) 
			return;
		
		// car moving
		if (isMoving) {
			currentWheelAngle = Mathf.SmoothDamp (currentWheelAngle, toWheelAngle, ref rotateVelocity, dampTime);
			wheel1.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel2.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel3.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel4.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel5.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel6.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			wheel7.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			transform.position = Vector3.SmoothDamp (transform.position, toPosition, ref moveVelocity, dampTime);
		}
	}
	
	void doRotate () {
		if (GameManager.m_level_complete)
			return;
		
		if (rotateUp) {
			toGunAngle = upAngle;
			rotateUp = false;
			rotateStep = 0;
			currentPause = 0;
		}
		else {
			toGunAngle = downAngle;
			rotateUp = true;
			rotateStep = 0;
			currentPause = 0;
		}		
	}
	
	IEnumerator shootBulletRoutine () {
		
		//audio.Play();
		spark.Play("KU_tank_spark");
		
		float angle = currentGunAngle - 10*(bulletCount/2);
		for (int i = 0 ; i < bulletCount ; i++) {
			shootBullet (angle);
			angle += 10;
		}
		
		yield return new WaitForSeconds(pauseInterval);
		
		if (currentPause < pauseTimes) {
			rotateStep = 0;			
		}
	}
	
	void shootBullet (float angle) {
		if (GameManager.m_level_complete)
			return;
		
		float speed = -30f;
		
		bullets[bulletsIndex].eulerAngles = new Vector3(0, 0, angle);
		bullets[bulletsIndex].GetComponent<Rigidbody2D>().velocity = new Vector2(speed*Mathf.Cos(Mathf.PI * angle/180.0f), speed*Mathf.Sin(Mathf.PI * angle/180.0f));
		bullets[bulletsIndex].position = spawnPoint.position;
		bullets[bulletsIndex].localScale = new Vector3(-1,-1,-1);
		
		bulletsIndex++;
		if (bulletsIndex > 11) bulletsIndex = 0;		
	}
	
	void pause () {
		isMoving = false;		
		distance = - distance;
		
		tread.speed = 0;
	}
	
	void movingCar () {
		isMoving = true;
		toPosition = new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z);
		toWheelAngle = -2f*(distance/0.0025f);
		
		tread.speed = 0;
		
		if (distance < 0) 
			tread.Play("MoveTread");
		else
			tread.Play("MoveTread"); //TODO: Change this back.
		
		Invoke ("pause", 2f);
	}
	
	void ExplosionCompleteDelegate() {
		if (GameManager.shotCountForBoss == GameManager.m_boss_life) {
			Destroy (this.gameObject);
			
			GameObject basket = GameObject.FindGameObjectWithTag("Basket");
			Destroy (basket);
		}
		else {			
			Invoke ("reset", 2f);
		}
    }

	public void RemoveHealth(){
		GameManager.m_boss_life--;
		m_boss_health_bar.TookDamage();
		if(GameManager.m_boss_life > 0){
			Instantiate(explosion, transform.position, Quaternion.identity);
		}else{
			InvokeRepeating("ExplosionTime",0.1f,0.1f);
			m_boss_hum.Stop();
		}
	}
	
	void reset () {
		bulletCount++;
		pauseInterval -= 0.2f;
		didExploded = false;
		GameManager.m_level_complete = false;
		InvokeRepeating ("movingCar", 1f, 5f);		
		InvokeRepeating ("doRotate", 1.5f, 4.5f);
	}
}
