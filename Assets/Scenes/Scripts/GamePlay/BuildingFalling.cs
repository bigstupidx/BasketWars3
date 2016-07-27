using UnityEngine;
using System.Collections;

public class BuildingFalling : MonoBehaviour {
	
	private float ShakeDecay;
	private float ShakeIntensity;
	
	public static bool isFalling;
	public static bool Shaking;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;
	private Vector3 OriginalBallPos;
	private Quaternion OriginalBallRot;
	
	private GameObject basketball;
	private Vector3 toPosition;
	private Vector3 toBallPosition;
	//private Vector3 moveVelocity;
	private int fallingTimes;
	private float deltaRange;
	
	private Animator anim;   // Link to the animated sprite
	
	// Use this for initialization
	void Start () {
		isFalling = false;
		Shaking = false;
//		moveVelocity = Vector3.zero;
		ShakeIntensity = 0;
		fallingTimes = 4;
		basketball = null;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		
		InvokeRepeating ("DoShake", 8f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.m_is_paused || GameManager.m_ball_has_been_thrown || GameManager.s_Inst.m_did_drag)
			return;
		
		if(ShakeIntensity > 0)
		{
			Vector3 shakePosition = Random.insideUnitSphere * ShakeIntensity;
			float shakeRot = Random.Range(-ShakeIntensity, ShakeIntensity)*.2f;
			
			transform.position = OriginalPos + shakePosition;
			transform.rotation = new Quaternion(OriginalRot.x, OriginalRot.y, OriginalRot.z + shakeRot, OriginalRot.w);
			
			if (basketball != null) {
				basketball.transform.position = OriginalBallPos + shakePosition;
				basketball.transform.rotation = new Quaternion(OriginalBallRot.x, OriginalBallRot.y, OriginalBallRot.z + shakeRot, OriginalBallRot.w);				
			}
			ShakeIntensity -= ShakeDecay;
			//Debug.Log(ShakeIntensity);
		}
		else {
			if (GameManager.s_Inst.m_building_shake) {
				GameManager.s_Inst.m_building_shake = false;
				ShakeIntensity = 0;
				transform.position = OriginalPos;
				transform.rotation = OriginalRot;
				
				if (basketball != null) {
					basketball.transform.position = OriginalBallPos;
					basketball.transform.rotation = OriginalBallRot;
				}
			}			
		}
		
		if (isFalling) {
			//transform.position = Vector3.SmoothDamp (transform.position, toPosition, ref moveVelocity, 0.2f);
			//basketball.transform.position = Vector3.SmoothDamp (basketball.transform.position, toBallPosition, ref moveVelocity, 0.2f);	
			if (transform.position.y >= toPosition.y) {
				transform.position -= new Vector3(0, deltaRange, 0);
			}
			
			if (basketball != null) {
				if (basketball.transform.position.y >= toBallPosition.y) {
					basketball.transform.position -= new Vector3(0, deltaRange, 0);
				}
				//Debug.Log(transform.position + " " + toPosition + " : " + basketball.transform.position + " " + toBallPosition);
				
				if (transform.position.y <= toPosition.y && basketball.transform.position.y <= toBallPosition.y) {
					reset ();
				}
			}
			else {
				if (transform.position == toPosition) 
					reset ();
			}
		}
	}
	
	void DoShake()
	{
		if (GameManager.m_ball_has_been_thrown || PullArrow.isReady || PullArrow.bounceBack || GameManager.m_nuke_explosion || GameManager.s_Inst.m_did_drag)
			return;
		basketball = GameObject.FindGameObjectWithTag("Ball");
		OriginalBallPos = basketball.transform.position;
		OriginalBallRot = basketball.transform.rotation;							
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		ShakeIntensity = 0.045f;
		ShakeDecay = 0.0005f;
		GameManager.s_Inst.m_building_shake = true;
		anim.Play ("BuildingShake");
		//ShakeCompleteDelegate();
	}
	
	public void ShakeCompleteDelegate() {
		isFalling = true;
		float range = 0;
		
		fallingTimes--;
		if (fallingTimes > 0) {
			range = 0.75f;			
			deltaRange = 0.05f;
		}
		else {
			range = 1.8f;
			deltaRange = 0.05f;
		}		
		
		toPosition = new Vector3(OriginalPos.x, OriginalPos.y - range, OriginalPos.z);
		
		if (basketball != null) {
			toBallPosition = new Vector3(OriginalBallPos.x, OriginalBallPos.y - range, OriginalBallPos.z);
			basketball.GetComponent<BallController>().m_start_pos = toBallPosition;
		}
	}
	
	void reset () {
		isFalling = false;
		basketball = null;
		//GlobalControl.Inst.throwHeightLimit += 20f;			
		if (fallingTimes <= 0) {
			GameManager.s_Inst.FailedLevel();
		}
	}
}
