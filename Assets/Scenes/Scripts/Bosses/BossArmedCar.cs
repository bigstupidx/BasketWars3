using UnityEngine;
using System.Collections;

public class BossArmedCar : MonoBehaviour {
	public Transform bullet;
	public Transform gun;
	public Transform backWheel;
	public Transform frontWheel;
	public Transform spawnPoint;
	
	public UISlider bossBlood;
	public int bossLife;
	
	public Animator explosion;
	public Animator spark;
	public Animator smoke1;
	public Animator smoke2;
	public Animator smoke3;
	
	private bool isMoving;
	private float dampTime;
	private Vector3 moveVelocity;
	private Vector3 toPosition;
	
	private float rotateVelocity;
	private float toWheelAngle;
	private float currentWheelAngle;
	private float distance;
	
	private float currentGunAngle;
	private float toGunAngle;
	private int totalStep;
	private int rotateStep;
	private bool rotateUp;
	private float deltaDegree;
	private float upAngle = -60;
	private float downAngle = 0;
	
	private Transform[] bullets;
	private int bulletsIndex;
	
	private int pauseTimes;
	private int currentPause;
	private int bulletCount;
	private Animator anim;
	private bool didExploded;
	int explosion_count = 20;
	public AudioSource m_boss_hum;
	public BossHealthBarHandler m_boss_health_bar;

	// Use this for initialization
	void Start () {
		//GlobalControl.bossLife = bossLife;
		didExploded = false;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		
		isMoving = false;
		moveVelocity = Vector3.zero;
		rotateVelocity = 0;
		dampTime = 1f;	
		
		totalStep = 20;
		rotateStep = 20;
		pauseTimes = 3;
		currentPause = 0;
		bulletCount = 3;
		
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
		if (GameManager.m_is_paused)
			return;
		
		if (GameManager.m_level_complete && !didExploded) {
			didExploded = true;
			explosion.Play("BunkerExplosion");
			//explosion.animationCompleteDelegate = ExplosionCompleteDelegate;
			
			if (GameManager.m_boss_life < 3) {
				anim.Play("ST_ArmedCar_Damaged");
				smoke1.transform.GetComponent<Renderer>().enabled = true;
				smoke2.transform.GetComponent<Renderer>().enabled = true;
				smoke3.transform.GetComponent<Renderer>().enabled = true;
				
				smoke1.Play ("Smoke");
				smoke2.Play ("Smoke");
				smoke3.Play ("Smoke");
			}
			
			CancelInvoke ("movingCar");
			CancelInvoke ("doRotate");
		}
		
		if (didExploded) 
			return;
		
		// car moving
		if (isMoving) {
			currentWheelAngle = Mathf.SmoothDamp (currentWheelAngle, toWheelAngle, ref rotateVelocity, dampTime);
			backWheel.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			frontWheel.eulerAngles = new Vector3 (0, 0, currentWheelAngle);
			transform.position = Vector3.SmoothDamp (transform.position, toPosition, ref moveVelocity, dampTime);
			if((transform.position - toPosition).magnitude <= 0.1f)
				GetComponent<AudioSource>().Stop();
		}
		
		// Rotate the gun and fire
		if (rotateStep < totalStep) {
			float diff = currentGunAngle - toGunAngle;
			if (diff > 0f) {
				currentGunAngle -= deltaDegree;
				gun.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			else if (diff < 0f) {
				currentGunAngle += deltaDegree;
				gun.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			
			if (rotateStep == 20) 
			{
				currentPause++;
				StartCoroutine( shootBulletRoutine() );
			}
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
		
		for (int i = 0 ; i < bulletCount ; i++) {
			shootBullet ();
			yield return new WaitForSeconds(0.3f);			
		}
		
		yield return new WaitForSeconds(0.2f);
		
		if (currentPause < pauseTimes) {
			rotateStep = 0;			
		}
	}
	
	void shootBullet () {
		if (GameManager.m_level_complete)
			return;
		
		float speed = -8f;
		
		bullets[bulletsIndex].eulerAngles = new Vector3(0, 0, currentGunAngle);
		bullets[bulletsIndex].GetComponent<AudioSource>().Play();
		spark.Play("Spark");
		bullets[bulletsIndex].GetComponent<Rigidbody2D>().velocity = new Vector3(speed*Mathf.Cos(Mathf.PI * currentGunAngle/180.0f), speed*Mathf.Sin(Mathf.PI * currentGunAngle/180.0f), 0);
		bullets[bulletsIndex].position = spawnPoint.position;
		
		bulletsIndex++;
		if (bulletsIndex > 11) bulletsIndex = 0;		
	}
	
	void pause () {
		isMoving = false;		
		distance = - distance;
	}
	
	void movingCar () {
		isMoving = true;
		toPosition = new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z);
		toWheelAngle = -2f*(distance/0.025f);		
		Invoke ("pause", 2.5f);
		GetComponent<AudioSource>().Play ();
	}
	
	void ExplosionCompleteDelegate(){
		if (GameManager.m_boss_life <= 0) {
			Destroy (this.gameObject);
		}
		else {			
			Invoke ("reset", 1.5f);
		}
    }
	
	void reset () {
		bulletCount++;
		didExploded = false;
		GameManager.m_level_complete = false;
		InvokeRepeating ("movingCar", 1f, 5f);		
		InvokeRepeating ("doRotate", 1.5f, 6.5f);
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
	
	void ExplosionTime()
	{
		explosion_count--;
		if(explosion_count <= 0){
			CancelInvoke("ExplosionTime");
			return;
		}
		Vector2 temp = Random.insideUnitCircle * Random.Range(1f,5f);
		Instantiate(explosion, transform.position + new Vector3(temp.x,temp.y), Quaternion.identity);
	}

}
