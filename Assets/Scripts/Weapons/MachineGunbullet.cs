using UnityEngine;
using System.Collections;

public class MachineGunbullet : Weapon {
	protected override void Awake()
	{
		m_bullet_cost = 0;
		base.Awake();
	}
}
