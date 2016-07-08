using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour 
{
	public Transform bullet;
	public Transform spawnPoint;
	public Transform gunLeg;
	
	public GameObject gunShell;
	
	public float upAngle = 325f;
	public float downAngle = 335f;
	public float startTime;
	
	private Transform [] bullets;
	
	private Animator anim;   // Link to the animated sprite
	private Animator shell;
	
	private bool didExplode;
	private bool shouldShoot;
	private bool rotateUp;
	private float currentAngle;
	private float toAngle;
	public GameObject m_ground_explosion;
	public GameObject m_machine_gun_barrel;
	public float m_bullet_speed = 5;
	// Use this for initialization
	void Start () {
		shell = gunShell.GetComponent<Animator>();
		bullets = new Transform[3];
		for (int i = 0 ; i < 3 ; i++) {
			bullets[i] = (Transform) Instantiate(bullet, new Vector3 (-1f, -3.9f, 0), Quaternion.identity);	
			if(transform.localScale.x == -1)
				bullets[i].transform.localScale = new Vector4(-1,1,1);
		}
		
		currentAngle = downAngle;
		toAngle = downAngle;
		didExplode = false;
		shouldShoot = false;
		rotateUp = true;
		InvokeRepeating ("doRotate", startTime, 3.5f);
		
		anim = m_machine_gun_barrel.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float diff = Mathf.Abs(currentAngle - toAngle);
		
		if (currentAngle > toAngle) {
			currentAngle -= 0.5f;
			this.gameObject.transform.eulerAngles = new Vector3(0, this.gameObject.transform.eulerAngles.y, currentAngle);	
		}
		else if (currentAngle < toAngle) {
			currentAngle += 0.5f;
			this.gameObject.transform.eulerAngles = new Vector3(0, this.gameObject.transform.eulerAngles.y, currentAngle);
		}
		
		if (diff < 0.5f) {
			if (shouldShoot) {
				shouldShoot = false;
				
				if (!didExplode && !GameManager.m_nuke_explosion)	{
					StartCoroutine(shootBulletRoutine());
				}
			}
		}
		
		if (GameManager.m_level_complete && !didExplode) {
			didExplode = true;
			Instantiate(m_ground_explosion,transform.position,Quaternion.identity);
			//anim.Play("Explosion");
			gunLeg.GetComponent<Renderer>().enabled = false;
			m_machine_gun_barrel.GetComponent<Renderer>().enabled = false;
		}
	}
	
	void doRotate () {
		if (didExplode || GameManager.m_nuke_explosion)
			return;
		
		if (rotateUp) {
			toAngle = upAngle;
			rotateUp = false;
			shouldShoot = true;
			
		}
		else {
			toAngle = downAngle;
			rotateUp = true;
			shouldShoot = true;
		}		
	}
	
	IEnumerator shootBulletRoutine () {		
		float speed;
		if(transform.localScale.x == -1)
			speed = m_bullet_speed;
		else
			speed = -m_bullet_speed;
		
		yield return new WaitForSeconds(0.1f);
		
		for (int i = 0 ; i < 3 ; i++) {
			bullets[i].eulerAngles = new Vector3(0, this.gameObject.transform.eulerAngles.y, toAngle);
			bullets[i].GetComponent<BossGunBullet>().ResetBullet();
			if (!didExplode) {
				GetComponent<AudioSource>().volume = 0.4f;
				GetComponent<AudioSource>().Play();
				anim.Play("MachineGunFire");
				shell.Play("MachineGunShell");

				bullets[i].GetComponent<Rigidbody2D>().velocity = bullets[i].transform.right * speed;
				bullets[i].position = spawnPoint.position;
				
				if (i == 2) {
					Donefiring();			
				}
				yield return new WaitForSeconds(0.4f);
			}
		}
		
				
	}
	
	void Donefiring() 
	{
		anim.Play("MachineGunIdle");
    }
}
