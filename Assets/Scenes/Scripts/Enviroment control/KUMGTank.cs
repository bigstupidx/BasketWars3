using UnityEngine;
using System.Collections;

public class KUMGTank : MonoBehaviour {
	
	public Animator spark;   // Link to the animated sprite
	public Animator explosion1;   // Link to the animated sprite
	public Animator explosion2;   // Link to the animated sprite
	public Animator explosion3;   // Link to the animated sprite
	
	public float StartTime;
	public float Interval;
	
	private float currentGunAngle;
	private float toGunAngle;
	private int totalStep;
	private int rotateStep;
	private bool rotateRight;
	private float deltaDegree;
	private float rightAngle = -60;
	private float leftAngle = 0;
	private int pauseTimes;
	private int currentPause;
	
	// Use this for initialization
	void Start () {
		totalStep = 20;
		rotateStep = 20;
		pauseTimes = 2;
		currentPause = 0;
		
		currentGunAngle = leftAngle;
		rotateRight = true;
		deltaDegree = Mathf.Abs(rightAngle - leftAngle) / (totalStep*pauseTimes);
		
		InvokeRepeating("doRotate", StartTime, Interval);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.m_is_paused)
			return;
		
		// Rotate the gun and fire
		if (rotateStep < totalStep) {
			float diff = currentGunAngle - toGunAngle;
			if (diff > 0f) {
				currentGunAngle -= deltaDegree;
				transform.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			else if (diff < 0f) {
				currentGunAngle += deltaDegree;
				transform.eulerAngles = new Vector3(0, 0, currentGunAngle);
				rotateStep++;
			}
			
			if (rotateStep == 20) 
			{
				currentPause++;
				StartCoroutine( shootRoutine() );
			}
		}
	}
	
	void doRotate () {
		if (rotateRight) {
			toGunAngle = rightAngle;
			rotateRight = false;
			currentPause = 0;
			StartCoroutine( shootRoutine() );		
		}
		else {
			toGunAngle = leftAngle;
			rotateRight = true;
			currentPause = 0;
			StartCoroutine( shootRoutine() );
		}			
	}
	
	IEnumerator shootRoutine () {		
		spark.Play("MGTankSpark");
		yield return new WaitForSeconds(0.5f);
		
		if (currentPause == 0) {
			if (rotateRight) {
				explosion3.Play("GroundExplosion");
			}
			else {
				explosion1.Play("GroundExplosion");
			}
		}
		else if (currentPause == 1) {
			explosion2.Play("GroundExplosion");			
		}
		else if (currentPause == 2) {
			if (rotateRight) {
				explosion1.Play("GroundExplosion");
			}
			else {
				explosion3.Play("GroundExplosion");
			}
		}
		
		yield return new WaitForSeconds(1.5f);
		
		if (currentPause < pauseTimes) {
			rotateStep = 0;			
		}
	}
	
}
