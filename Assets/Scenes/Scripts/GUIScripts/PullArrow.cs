using UnityEngine;
using System.Collections;

//RequireComponent(typeof(tk2dAnimatedSprite))]
public class PullArrow : MonoBehaviour {
	public static bool isReady;
	public static bool doShoot;
	public static bool bounceBack;
	
//	private tk2dAnimatedSprite anim;           // Link to the animated sprite
	//private tk2dAnimatedSprite tapPointAnim;   // Link to tapPoint animated sprite
	//private tk2dAnimatedSprite soldierAnim;        // Link to soldier animated sprite
	
	private GameObject tapPoint;
	private GameObject basketball;
	
	//private float dampTime = 0.1f;
	//private Vector3 bouncePosition = new Vector3 (0, 0, 0);
	//private Vector3 velocity = new Vector3 (0, 1, 0);
	private Vector3 ballPosition;
	
	private static PullArrow _inst;
	public static PullArrow Inst
	{	
		get{
			return _inst;
		}		
	}
	
	// Use this for initialization
	void Start () {
		_inst = this;
		/*anim = GetComponent<tk2dAnimatedSprite>(); // This script must be attached to the sprite to work.
		
		tapPoint = GameObject.FindGameObjectWithTag("TapPoint");
		tapPointAnim = tapPoint.GetComponent<tk2dAnimatedSprite>();
		
		GameObject soldier = GameObject.FindGameObjectWithTag("Soldier");
		soldierAnim = soldier.GetComponent<tk2dAnimatedSprite>();
		
		doShoot = false;
		isReady = false;
		bounceBack = false;*/
	}
	
	// Update is called once per frame
	void Update () {		
		
		/*if (GlobalControl.isPaused) {
			resetArrow ();
			return;
		}
		
		if (doShoot) {
			isReady = false;
			bounceBack = true;
			doShoot = false;
			bouncePosition = new Vector3(tapPoint.transform.position.x, tapPoint.transform.position.y, this.gameObject.transform.position.z);
			velocity = 0.1f * (bouncePosition - this.gameObject.transform.position);
			return;
		}
		
		if (bounceBack) {
			this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, bouncePosition, ref velocity, dampTime);
		}
		
		if (!isReady && !bounceBack)
			return;
		
		float diff = (this.gameObject.transform.position - tapPoint.transform.position).sqrMagnitude;
		//Debug.Log("diff: " + diff);
		
		if (diff > 0.110f) {
			anim.Play("Arrow5");
			DrawTrajectory.showPathCount = 10;
			
			if (!bounceBack) {
				Soldier.Inst.stopBlink ();
				soldierAnim.Play("Throw03");
				basketball.transform.eulerAngles = new Vector3(0, 0, 17);
				basketball.transform.position = new Vector3(ballPosition.x-0.14f, ballPosition.y, ballPosition.z);				
			}
		}
		else if (diff > 0.075f) {
			anim.Play("Arrow4");
			DrawTrajectory.showPathCount = 10;
			
			if (!bounceBack) {
				Soldier.Inst.stopBlink ();
				soldierAnim.Play("Throw03");
				basketball.transform.eulerAngles = new Vector3(0, 0, 17);
				basketball.transform.position = new Vector3(ballPosition.x-0.14f, ballPosition.y, ballPosition.z);				
			}
		}
		else if (diff > 0.049f) {
			anim.Play("Arrow3");
			DrawTrajectory.showPathCount = 9;
			
			if (!bounceBack) {
				Soldier.Inst.stopBlink ();
				soldierAnim.Play("Throw02");
				basketball.transform.eulerAngles = new Vector3(0, 0, 16.5f);
				basketball.transform.position = new Vector3(ballPosition.x-0.08f, ballPosition.y, ballPosition.z);				
			}
		}
		else if (diff > 0.029f) {
			anim.Play("Arrow2");
			DrawTrajectory.showPathCount = 8;
			
			if (!bounceBack) {
				Soldier.Inst.stopBlink ();
				soldierAnim.Play("Throw02");
				basketball.transform.eulerAngles = new Vector3(0, 0, 16.5f);
				basketball.transform.position = new Vector3(ballPosition.x-0.08f, ballPosition.y, ballPosition.z);				
			}
		}
		else if (diff < 0.012f) {
			if (bounceBack) {
				bounceBack = false;
				isReady = true;
				tapPointAnim.Play("Shoot");
				tapPointAnim.animationCompleteDelegate = AimCompleteDelegate;
			}
			
			if (basketball == null && !GlobalControl.m_ball_has_been_shot) {
				basketball = GameObject.FindGameObjectWithTag("Basketball");
				ballPosition = basketball.transform.position;
			}
			
			anim.Play("Idle");
			DrawTrajectory.showPathCount = 4;
		}		
		else {
			anim.Play("Arrow1");
			DrawTrajectory.showPathCount = 6;
			
			if (!bounceBack) {
				Soldier.Inst.stopBlink ();
				soldierAnim.Play("Throw01");
				basketball.transform.eulerAngles = new Vector3(0, 0, 8f);
				basketball.transform.position = new Vector3(ballPosition.x-0.04f, ballPosition.y, ballPosition.z);				
			}
		}*/
	}
	
	void AimCompleteDelegate(Sprite sprite, int clipId) {
		/*isReady = false;
		tapPointAnim.Play("Arrow0");
		tapPoint.transform.position = new Vector3(-20, tapPoint.transform.position.y, 0f);
		GlobalControl.m_ball_has_been_shot = true;
		BallControl.Inst.shootTheBall ();
		
		DrawTrajectory.Inst.drawPreviousPath ();
		
		//GlobalControl.currentTimescale = Time.timeScale;
		//GlobalControl.currentDeltaTime = Time.fixedDeltaTime;
		
		Invoke ("slowTime", 0.15f);
		/*
		if (BallControl.Inst.weaponType == BallControl.WeaponType.MachineGun && !GlobalControl.basketActive) {
			Invoke ("slowTime", 0.15f);
		}*/
    }
	
	public void resetArrow () {
		/*this.gameObject.transform.position = new Vector3(tapPoint.transform.position.x, tapPoint.transform.position.y, this.gameObject.transform.position.z);
		tapPointAnim.Play("Arrow0");
		tapPoint.transform.position = new Vector3(-20, tapPoint.transform.position.y, 0f);*/
	}
	
	void slowTime () {		
		Time.timeScale = 0.5f;
		Time.fixedDeltaTime = 0.5f * 0.02f;
		
		//GlobalControl.currentTimescale = Time.timeScale;
		//GlobalControl.currentDeltaTime = Time.fixedDeltaTime;
	}
}
