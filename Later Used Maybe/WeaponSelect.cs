using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSelect : MonoBehaviour 
{
	int m_selected_weapon = 0;
	public List<GameObject> m_weapons;
	public GameObject m_equip;
	public GameObject m_equiped;
	public GameObject m_buy_weapon;
	void Start(){
		Invoke("UpdateLabels",0.1f);
	}

	public void UpdateLabels()
	{
		bool owned = GameManager.s_Inst.IsWeaponOwned(m_selected_weapon);
		if(owned && m_selected_weapon != GameManager.s_Inst.m_current_weapon){
			m_equip.SetActive(true);
			m_equiped.SetActive(false);
			m_buy_weapon.SetActive(false);
		}
		if(owned && m_selected_weapon == GameManager.s_Inst.m_current_weapon){
			m_equip.SetActive(false);
			m_equiped.SetActive(true);
			m_buy_weapon.SetActive(false);
		}
		if(!owned){
			m_equip.SetActive(false);
			m_equiped.SetActive(false);
			m_buy_weapon.SetActive(true);
		}
		//GameManager.s_Inst.gameObject.GetComponent<KiiBucketManager>().SaveWeaponObject();
	}
	public void Left(){
		m_selected_weapon++;
		if(m_selected_weapon >= m_weapons.Count)
		{
			m_selected_weapon = 0;
			foreach(GameObject g in m_weapons){
				g.transform.localPosition += new Vector3(731.4f,0,0);
			}
		}
		else{
			foreach(GameObject g in m_weapons){
				g.transform.localPosition -= new Vector3(243.8f,0,0);
			}
		}
		UpdateLabels();
		
	}

	public void Right(){
		m_selected_weapon--;
		if(m_selected_weapon < 0)
		{
			m_selected_weapon = m_weapons.Count -1;
			foreach(GameObject g in m_weapons){
				g.transform.localPosition -= new Vector3(731.4f,0,0);
			}
		}
		else{
			foreach(GameObject g in m_weapons){
				g.transform.localPosition += new Vector3(243.8f,0,0);
			}
		}
		UpdateLabels();
	}

	public void Equip()
	{
		GameManager.s_Inst.m_current_weapon = m_selected_weapon;
		UpdateLabels();
	}

	public void Purchase()
	{
		if(SaveLoadManager.m_save_info.m_coins >= 1000)
		{
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().WithdrawCoins();
			GameManager.s_Inst.RemoveCoins(1000);
			GameManager.s_Inst.UnlockWeapon(m_selected_weapon);
		}
		UpdateLabels();
	}
}
