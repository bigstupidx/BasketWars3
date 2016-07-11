using UnityEngine;
using System.Collections;

public class LevelInitializer : MonoBehaviour 
{
	public Vector2 m_16_9_camera_position;
	public float m_16_9_camera_size;
	public Vector2 m_4_3_camera_position;
	public float m_4_3_camera_size;
	public Vector2 m_16_9_throw_force;
	public Vector2 m_4_3_throw_force;
	public Vector2 m_16_9_grenade_dir;
	public Vector2 m_4_3_grenade_dir;
	public Vector2 m_16_9_mortar_dir;
	public Vector2 m_4_3_mortar_dir;

	public int m_lives_given;
	public int m_3_star_score;
	public int m_2_star_score;
	public int m_1_star_score;
	public int m_ammo_given;
    public int m_max_ammo;
	public int m_3_star_coins;
	public int m_2_star_coins;
	public int m_1_star_coins;
	public int m_3_star_xp;
	public int m_2_star_xp;
	public int m_1_star_xp;
	public int m_throw_line_dots = 11;

	float m_4_by_3 = 0.666f;
	float m_16_by_9 = 0.5621986f;

	void Start (){
		float ratio = (float)Screen.height / (float)Screen.width;
		if(ratio <= m_16_by_9 + 0.05f && ratio >= m_16_by_9 - 0.05f){
            Debug.Log("16/9 Ratio screen");
			GameObject.Find("RenderCamera").transform.position = new Vector3(m_16_9_camera_position.x, m_16_9_camera_position.y,-10);
			GameObject.Find("RenderCamera").GetComponent<Camera>().orthographicSize = m_16_9_camera_size;
			GameManager.s_Inst.m_max_force_y = m_16_9_throw_force.y;
			GameManager.s_Inst.m_max_force_x = m_16_9_throw_force.x;
			GameManager.s_Inst.GrenadeThrowDirection = new Vector3(m_16_9_grenade_dir.x,m_16_9_grenade_dir.y,0);
			GameManager.s_Inst.MortarShootDirection = new Vector3(m_16_9_mortar_dir.x,m_16_9_mortar_dir.y,0);
		}
		else if(ratio <= m_4_by_3 + 0.05f && ratio >= m_4_by_3 - 0.05f){
            Debug.Log("4/3 Ratio screen");
			GameObject.Find("RenderCamera").transform.position = new Vector3(m_4_3_camera_position.x, m_4_3_camera_position.y,-10);
			GameObject.Find("RenderCamera").GetComponent<Camera>().orthographicSize = m_4_3_camera_size;			
			GameManager.s_Inst.m_max_force_y = m_4_3_throw_force.y;
			GameManager.s_Inst.m_max_force_x = m_4_3_throw_force.x;
			GameManager.s_Inst.GrenadeThrowDirection = new Vector3(m_4_3_grenade_dir.x,m_4_3_grenade_dir.y,0);
			GameManager.s_Inst.MortarShootDirection = new Vector3(m_4_3_mortar_dir.x,m_4_3_mortar_dir.y,0);
		}
		GameManager.s_Inst.m_3_star_score = m_3_star_score;
		GameManager.s_Inst.m_2_star_score = m_2_star_score;
		GameManager.s_Inst.m_1_star_score = m_1_star_score;
		if(GameObject.Find("StarLivesNumber") != null)
			GameObject.Find("StarLivesNumber").GetComponent<UILabel>().text = m_2_star_score.ToString();
		GameManager.s_Inst.m_bullets = m_ammo_given;
        GameManager.s_Inst.max_m_bullets = m_max_ammo;
        GameManager.s_Inst.UpdateAmmoLabel();
        GameManager.s_Inst.m_lives = m_lives_given;
        GameManager.s_Inst.m_max_lives = m_lives_given;
        GameManager.s_Inst.UpdateLivesLabel();
		GameManager.s_Inst.m_3_star_coins = m_3_star_coins;
		GameManager.s_Inst.m_2_star_coins = m_2_star_coins;
		GameManager.s_Inst.m_1_star_coins = m_1_star_coins;
		GameManager.s_Inst.m_3_star_xp = m_3_star_xp;
		GameManager.s_Inst.m_2_star_xp = m_2_star_xp;
		GameManager.s_Inst.m_1_star_xp = m_1_star_xp;
		GameManager.s_Inst.SetCameraRect();
		DrawTrajectory.showPathCount = m_throw_line_dots;
	}
	

	[ContextMenu("Set Camera 16:9")]
	public void SetCamera16_9(){ 
		Transform cam = GameObject.Find("RenderCamera").transform;
		m_16_9_camera_position = new Vector2(cam.position.x, cam.position.y);
		m_16_9_camera_size = cam.GetComponent<Camera>().orthographicSize;
	}

	[ContextMenu("Set Camera 4:3")]
	public void SetCamera4_3(){ 
		Transform cam = GameObject.Find("RenderCamera").transform;
		m_4_3_camera_position = new Vector2(cam.position.x, cam.position.y);
		m_4_3_camera_size = cam.GetComponent<Camera>().orthographicSize;
	}

	[ContextMenu("Make Camera 16:9")]
	public void MakeCamera16_9(){ 
		Transform cam = GameObject.Find("RenderCamera").transform;
		cam.position = new Vector3(m_16_9_camera_position.x,m_16_9_camera_position.y,-1);
		cam.GetComponent<Camera>().orthographicSize = m_16_9_camera_size;
	}
	
	[ContextMenu("Make Camera 4:3")]
	public void MakeCamera4_3(){ 
		Transform cam = GameObject.Find("RenderCamera").transform;
		cam.position = new Vector3(m_4_3_camera_position.x,m_4_3_camera_position.y,-1);
		cam.GetComponent<Camera>().orthographicSize = m_4_3_camera_size;
	}

	[ContextMenu("Revert to Default Values")]
	public void RevertToDefaultValues(){
		m_lives_given = 5;
		m_3_star_score = 5;
		m_2_star_score = 4;
		m_ammo_given = 5;
		m_3_star_coins = 0;
		m_2_star_coins = 0;
		m_1_star_coins = 0;
		m_4_3_throw_force = new Vector2(1600,1475);
		m_16_9_throw_force = new Vector2(1600,1475);
	}
}
