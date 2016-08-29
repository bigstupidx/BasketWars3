using UnityEngine;
using System.Collections;

public class SoldierController: MonoBehaviour 
{
	public Animator m_animator;
	public GameObject m_head_for_shooting;
	public GameObject m_gun_for_shooting;
	public GameObject m_shotgun_for_shooting;
	public GameObject m_rocket_for_shooting;
	public Animator m_gun_spark;
	public GameObject m_pistol_bullet;
	public GameObject m_shotgun_bullet;
	public GameObject m_rocket;
	public Transform m_bullet_spawn_point;
	public enum WeaponType{
		Pistol,
		Shotgun,
		Rocket
	}
	public enum SoldierState{
		Throwing,
		Shooting
	}

	public SoldierState m_current_state;
	public WeaponType m_current_weapon;
    public Sprite final_weapon_state;

	void Start (){
		m_animator.Play("Idle");
		DisableHeadAndArm();

    }
	
	// Update is called once per frame
	void Update (){ 
    }

	public void ShootGun(){
        //Check Bullets
        if (GameManager.s_Inst.m_bullets <= 0)
            return;
        --GameManager.s_Inst.m_bullets;
        GameManager.s_Inst.UpdateAmmoLabel();

        //Check Sprite
        if (GetComponent<SpriteRenderer>().sprite != final_weapon_state)
        {
            EnableHeadAndArm();
            m_animator.Play("Animation Shooting Cancel");
        }

		GameObject go = null;

        //Actual Shooting
		if (m_current_weapon == WeaponType.Pistol) {
			go = (GameObject)Instantiate (m_pistol_bullet, m_bullet_spawn_point.position, Quaternion.identity);
		} else if (m_current_weapon == WeaponType.Shotgun) {
			go = (GameObject)Instantiate (m_shotgun_bullet, m_bullet_spawn_point.position, Quaternion.identity);
		} 


		if (go != null) {
			Weapon temp = go.GetComponent<Weapon> ();
			temp.Fire (m_bullet_spawn_point.right);
			temp.set_pivot_row (GameManager.s_Inst.soldier_position);
			m_gun_spark.Play ("SoldierGunSpark");
		}
	}

	public void StartThrow(){
		DisableHeadAndArm();
		m_animator.Play("Throwing");
	}

	public void FinishThrow(){
		DisableHeadAndArm();
		m_animator.Play("Throwing_finish");
	}

	public void ResetStance(){
		DisableHeadAndArm();
        m_current_state = SoldierState.Throwing;
		m_animator.Play("Idle");
    }

	public void EnableHeadAndArm(){
		if(m_head_for_shooting != null){
			m_head_for_shooting.SetActive(true);
			if(m_current_weapon == WeaponType.Pistol)
				m_gun_for_shooting.SetActive(true);
            else if(m_current_weapon == WeaponType.Rocket)
				m_rocket_for_shooting.SetActive(true);
			else if(m_current_weapon == WeaponType.Shotgun)
				m_shotgun_for_shooting.SetActive(true);
        }
    }

	public void DisableHeadAndArm(){
		if(m_head_for_shooting != null){
			m_head_for_shooting.SetActive(false);
			m_gun_for_shooting.SetActive(false);
			m_rocket_for_shooting.SetActive(false);
			m_shotgun_for_shooting.SetActive(false);
		}
		
	}

	public void ToggleState(){
		if(m_current_state == SoldierState.Throwing){
			m_current_state = SoldierState.Shooting;
			SwitchToGun();
		}
		else
			m_current_state = SoldierState.Throwing;
	}
		
	public void SwitchToGun(){
        if (m_current_weapon == WeaponType.Pistol)
			m_animator.Play("DrawWeapon");
		else if(m_current_weapon == WeaponType.Rocket)
			m_animator.Play("DrawRocket");
		else if(m_current_weapon == WeaponType.Shotgun)
			m_animator.Play("DrawShotGun");
	}

    public bool isShooting()
    {
        return m_current_state == SoldierState.Shooting;
    }
}
