using UnityEngine;
using System.Collections;

public class WeaponEquipper : MonoBehaviour {
	public int m_weapon_number;
	public GameObject stamp;

	public void OnClick() {
		switch (m_weapon_number) {
		case (1):
			GameManager.s_Inst.Solider_weapon = SoldierController.WeaponType.Pistol;
			break;
		case (2):
			GameManager.s_Inst.Solider_weapon = SoldierController.WeaponType.Shotgun;
			break;
		case (3):
			GameManager.s_Inst.Solider_weapon = SoldierController.WeaponType.Rocket;
			break;
		}
		stamp.transform.localPosition = new Vector3 ((m_weapon_number -1)*340, 200, 1);
	}
}
