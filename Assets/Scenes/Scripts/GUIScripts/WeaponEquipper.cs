using UnityEngine;
using System.Collections;

public class WeaponEquipper : MonoBehaviour {
	public int m_weapon_number;
	public GameObject stamp;

	public void OnClick() {
		stamp.transform.position.Set ((m_weapon_number - 1) * 340, 200, 0);
	}
}
