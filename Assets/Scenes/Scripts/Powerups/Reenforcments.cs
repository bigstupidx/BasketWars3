using UnityEngine;
using System.Collections;

public class Reenforcments : MonoBehaviour 
{
	bool is_enabled = false;
	public GameObject m_shell;
	public GameObject m_shell_spawn;
	public GameObject m_bullet;
	public GameObject m_gun_pivot;
	public GameObject m_wheel_front;
	public GameObject m_wheel_back;
	public Transform m_bullet_spawn;
	public float m_wheel_rot_speed;
	public Animator m_muzzle_flash;
	public Transform m_right_position;
	public float m_idle_time;
	public float m_jeep_speed = 5;
	float m_fire_speed = 0.15f;
	float m_fire_timer = 0.15f;
	float m_max_angle = 45;
	float idle_time = 0.5f;
	float idle_timer = 0.5f;
	public bool m_is_on_building = false;

	public enum JeepState{
		Idle,
		MoveIn,
		FireUp,
		FireDown
	}

	public JeepState m_current_state = JeepState.Idle;
	JeepState m_next_state = JeepState.Idle;

	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update (){
		if(is_enabled){
			if(m_current_state == JeepState.Idle){
				if(idle_timer > 0)
					idle_timer -= Time.deltaTime;
				else{
					m_current_state = m_next_state;
					idle_timer = idle_time;
				}
			}
			if(m_current_state == JeepState.FireUp){
				m_gun_pivot.transform.Rotate(Vector3.forward,Time.deltaTime * m_max_angle);
				if(m_gun_pivot.transform.rotation.eulerAngles.z > 45){
					Quaternion temp = new Quaternion();
					temp.eulerAngles = new Vector3(0,0,45);
					m_gun_pivot.transform.rotation = temp;
					m_current_state = JeepState.Idle;
					m_next_state = JeepState.FireDown;
					m_fire_timer = m_fire_speed;
				}
				if(m_fire_timer > 0)
					m_fire_timer -= Time.deltaTime;
				else{
					m_fire_timer = m_fire_speed;
					m_muzzle_flash.Play("JeepFire");
					Instantiate(m_shell,m_shell_spawn.transform.position,Quaternion.identity);
					GameObject go = (GameObject)Instantiate(m_bullet,m_bullet_spawn.position, m_gun_pivot.transform.rotation);
					go.transform.localScale = Vector3.one * 0.75f;
				}
			}
			if(m_current_state == JeepState.FireDown){
				m_gun_pivot.transform.Rotate(Vector3.forward,Time.deltaTime * -m_max_angle);
				if(m_gun_pivot.transform.rotation.eulerAngles.z  < 360 && m_gun_pivot.transform.rotation.eulerAngles.z > 300 ){
					Quaternion temp = new Quaternion();
					temp.eulerAngles = Vector3.zero;
					m_gun_pivot.transform.rotation = temp;
					m_current_state = JeepState.Idle;
					m_next_state = JeepState.FireUp;
					m_fire_timer = m_fire_speed;
				}
				if(m_fire_timer > 0)
					m_fire_timer -= Time.deltaTime;
				else{
					m_fire_timer = m_fire_speed;
					m_muzzle_flash.Play("JeepFire");
					Instantiate(m_shell,m_shell_spawn.transform.position,Quaternion.identity);
					GameObject go = (GameObject)Instantiate(m_bullet,m_bullet_spawn.position, m_gun_pivot.transform.rotation);
					go.transform.localScale = Vector3.one * 0.75f;
				}
			}
		}
		if(m_current_state == JeepState.MoveIn){
			m_wheel_back.transform.Rotate(Vector3.forward,m_wheel_rot_speed * Time.deltaTime);
			m_wheel_front.transform.Rotate(Vector3.forward,m_wheel_rot_speed * Time.deltaTime);
			transform.position += Vector3.right * m_jeep_speed * Time.deltaTime;
			m_jeep_speed -= Time.deltaTime;
			m_wheel_rot_speed += Time.deltaTime * 60;
			if(transform.position.x > m_right_position.position.x){
				transform.position = new Vector3(m_right_position.position.x,transform.position.y,transform.position.z);
				if(m_is_on_building)
					ParentToBuilding();
				m_current_state = JeepState.Idle;
				m_next_state = JeepState.FireUp;
				is_enabled = true;
			}
		}
	
	}

	public void ParentToBuilding(){
		transform.parent = GameObject.Find("ST_Gold_Building_0001").transform;
	}

	public void EnableJeep(){
		m_current_state = JeepState.MoveIn;
	}
}
