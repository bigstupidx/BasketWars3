using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BossBunker : MonoBehaviour {
	public Transform bullet;
	public Transform spawnPoint;
	
	public GameObject Fire;
	
	public float upAngle = -30f;
	public float downAngle = 13f;
	public float startTime;
	public float interval;
	public GameObject explosion;
	
	private Transform [] bullets;
	private Animator anim;   // Link to the animated sprite
	public  Animator main_bunker;   // Link to the animated sprite
	
	private bool rotateUp;
	private float currentAngle;
	private float toAngle;
	private float deltaDegree;
	
	private int totalStep = 20;
	private int rotateStep = 20;
	
	public static Dictionary<Transform,bool> bulletPool = new Dictionary<Transform, bool>();
	
	public TweenPosition m_health_bar_1;
	public TweenPosition m_health_bar_2;
	public TweenPosition m_health_bar_3;

	private static BossBunker _inst;
	public static BossBunker Inst
	{	
		get{
			return _inst;
		}		
	}
	
	// Use this for initialization
	void Start () {
		_inst = this;
		currentAngle = downAngle;
		anim = Fire.GetComponent<Animator>();
		rotateUp = true;
		deltaDegree = Mathf.Abs(upAngle - downAngle) / totalStep;
		
		bulletPool.Clear();
		//bullets = new Transform[4];
		for (int i = 0 ; i < 13 ; i++) {
			//bullets[i] = (Transform) Instantiate(bullet, new Vector3 (-1f, -0.9f, 0), Quaternion.identity);
			Transform theBullet = (Transform) Instantiate(bullet, new Vector3 (-1f, -0.9f, 0), Quaternion.identity);
			bulletPool.Add(theBullet,true);
		}
		
		InvokeRepeating ("doRotate", startTime, interval);
	}
	
	// Update is called once per frame
	void Update () {		
		if (rotateStep < totalStep) {
			float diff = currentAngle - toAngle;
			if (diff > 0f) {
				currentAngle -= deltaDegree;
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
				rotateStep++;
			}
			else if (diff < 0f) {
				currentAngle += deltaDegree;
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
				rotateStep++;
			}
			
			if (rotateStep % 5 == 0) 
			{
				shootBullet ();
				//bulletID++;
			}
		}
	}
	
	void doRotate () {
		if (GameManager.m_level_complete)
			return;
		
		if (rotateUp) {
			toAngle = upAngle;
			rotateUp = false;
			rotateStep = 0;
		}
		else {
			toAngle = downAngle;
			rotateUp = true;
			rotateStep = 0;
		}		
	}
	
	void shootBullet () {
		if (GameManager.m_level_complete)
			return;
		
		float speed = -30f;
		
		Transform result = null;
		foreach(var obj in bulletPool)
		{
			if (obj.Value)
			{
				Transform t = obj.Key;
				result = t;
				bulletPool[t] = false;
				break;
			}
		}
		
		if (result == null) {
			Transform theBullet = (Transform)Instantiate(bullet);
			bulletPool.Add(theBullet,false);
			result = theBullet;
		}
		
		result.eulerAngles = new Vector3(0, 0, currentAngle);
		GetComponent<AudioSource>().Play();
		anim.Play("Fire");
		result.GetComponent<Rigidbody2D>().velocity = new Vector2(speed*Mathf.Cos(Mathf.PI * currentAngle/180.0f), speed*Mathf.Sin(Mathf.PI * currentAngle/180.0f));
		result.position = spawnPoint.position;
	}

	public void RemoveHealth(int num){
		if(num == 3){
			main_bunker.Play("Bunker_Damaged");
			GameObject.Find("Health 1").GetComponent<AudioSource>().Play();
			m_health_bar_1.PlayForward();
			m_health_bar_1.gameObject.GetComponent<TweenAlpha>().PlayForward();
			Instantiate(explosion, transform.position, Quaternion.identity);
		}
		if(num == 2){
			GameObject.Find("Health 2").GetComponent<AudioSource>().Play();
			m_health_bar_2.PlayForward();
			m_health_bar_2.gameObject.GetComponent<TweenAlpha>().PlayForward();
			Instantiate(explosion, transform.position, Quaternion.identity);
		}
		if(num == 1){
			GameObject.Find("Health 3").GetComponent<AudioSource>().Play();
			m_health_bar_3.PlayForward();
			m_health_bar_3.gameObject.GetComponent<TweenAlpha>().PlayForward();
			Instantiate(explosion, transform.position, Quaternion.identity);
			InvokeRepeating("ExplosionTime",0.1f,0.1f);
		}
	}
	
	public void powerUp () {
		// reset Bunker cannon
		CancelInvoke ("doRotate");
		currentAngle = toAngle;
		this.gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
		
		interval -= 1.2f;
		totalStep += 5;		
		deltaDegree = Mathf.Abs(upAngle - downAngle) / totalStep;
		rotateStep = totalStep;
		
		InvokeRepeating ("doRotate", startTime, interval);
	}
}
