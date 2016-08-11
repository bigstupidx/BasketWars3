using UnityEngine;
using System.Collections;

public class KUFarTank : MonoBehaviour {
	
	public Animator spark;   // Link to the animated sprite
	
	public float StartTime;
	public float Interval;
	
	public float startMovingTime;
	public float movingInterval;
	
	private bool isMoving;
	private float dampTime;
	private Vector3 moveVelocity;
	private Vector3 toPosition;
	
	public float distance;
	
	// Use this for initialization
	void Start () {
		isMoving = false;
		
		moveVelocity = Vector3.zero;
		dampTime = 0.8f;
		
		InvokeRepeating("doShoot", StartTime, Interval);
		InvokeRepeating("movingCar", startMovingTime, movingInterval);
	}
	
	// Update is called once per frame
	void Update () {
		// car moving
		if (isMoving) {
			transform.position = Vector3.SmoothDamp (transform.position, toPosition, ref moveVelocity, dampTime);
		}
	}
	
	void doShoot () 
	{
		spark.Play("BGTankFire");	
		//gameObject.GetComponent<AudioSource>().Play();
	}
	
	void movingCar () {
		isMoving = true;
		toPosition = new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z);
		
		Invoke ("pause", 2.5f);
	}
	
	void pause () {
		isMoving = false;		
		distance = - distance;
	}
}
