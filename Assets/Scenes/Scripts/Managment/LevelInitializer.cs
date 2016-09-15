using UnityEngine;
using System.Collections;

public class LevelInitializer : MonoBehaviour 
{
	public Vector2 m_16_9_camera_position;
	public float m_16_9_camera_size;
	public Vector2 m_4_3_camera_position;
	public float m_4_3_camera_size;

	public int m_star_baskets_made;
	public int m_3_star_coins;
	public int m_2_star_coins;
	public int m_1_star_coins;
	public int m_3_star_xp;
	public int m_2_star_xp;
	public int m_1_star_xp;

	public short waves; 
	public short[] zombies_spawn_wave;
	public float[] zombie_speeds;
	public int[] zombie_speed_rates;

	public short small_zombie_health;
	public short small_zombie_dmg;

	public int m_throw_line_dots = 11;

	float m_4_by_3 = 0.666f;
	float m_16_by_9 = 0.5621986f;

	void Start (){
		float ratio = (float)Screen.height / (float)Screen.width;
		if(ratio <= m_16_by_9 + 0.05f && ratio >= m_16_by_9 - 0.05f){
            Debug.Log("16/9 Ratio screen");
			GameObject.Find("RenderCamera").transform.position = new Vector3(m_16_9_camera_position.x, m_16_9_camera_position.y,-10);
			GameObject.Find("RenderCamera").GetComponent<Camera>().orthographicSize = m_16_9_camera_size;
		}
		else if(ratio <= m_4_by_3 + 0.05f && ratio >= m_4_by_3 - 0.05f){
            Debug.Log("4/3 Ratio screen");
			GameObject.Find("RenderCamera").transform.position = new Vector3(m_4_3_camera_position.x, m_4_3_camera_position.y,-10);
			GameObject.Find("RenderCamera").GetComponent<Camera>().orthographicSize = m_4_3_camera_size;
		}

		GameManager.s_Inst.set_BasketLabel (m_star_baskets_made);

		GameManager.s_Inst.m_bullets = 0;
        GameManager.s_Inst.max_m_bullets = 10;
        GameManager.s_Inst.UpdateAmmoLabel();

        GameManager.s_Inst.m_lives = 100;
        GameManager.s_Inst.m_max_lives = 100;
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
		m_star_baskets_made = 5;
		m_3_star_coins = 0;
		m_2_star_coins = 0;
		m_1_star_coins = 0;
	}
}
