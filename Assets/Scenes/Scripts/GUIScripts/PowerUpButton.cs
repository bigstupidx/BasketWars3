using UnityEngine;
using System.Collections;

public class PowerUpButton : MonoBehaviour 
{
	public GameObject reset_bar;

    public GameObject m_ace_powerup;
	public GameObject m_Armor_powerup_gui;
	public GameObject m_nuke_prefab;
	public UILabel powerup_number;
	public GameObject[] button_parts;

	private int value = 10;
	private float reset_time = 0;

	// Use this for initialization
	void Start () {
		if(GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.None)
			gameObject.SetActive(false);
		if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Ace)
			powerup_number.text = SaveLoadManager.m_save_info.m_ace_powerup.ToString();
		else if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Armor)
			powerup_number.text = SaveLoadManager.m_save_info.m_armor_powerup.ToString();
		else if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Nuke)
			powerup_number.text = SaveLoadManager.m_save_info.m_nuke_powerup.ToString();
	}

	public void OnClick(){
		if (reset_time > 0)
			return;
		
		if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Ace) {
			StartCoroutine (AcePowerUp ());
			UpdatePowerUpLabel(--SaveLoadManager.m_save_info.m_armor_powerup);
		} else if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Nuke) {

			//Position and create nuke
			GameObject go = (GameObject)Instantiate(m_nuke_prefab);
			go.transform.parent = GameObject.Find ("Anchor - BC").transform;
			go.transform.localScale = new Vector3(1.7f,1.7f,1);
			go.transform.localPosition = new Vector3(120,0,0);

			UpdatePowerUpLabel(--SaveLoadManager.m_save_info.m_nuke_powerup);

				
		} else if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.Armor) {
			//Add Armor
			m_Armor_powerup_gui.SetActive (true);
			GameManager.s_Inst.m_armor += 10;
			GameManager.s_Inst.UpdateArmorLabel ();

			UpdatePowerUpLabel(--SaveLoadManager.m_save_info.m_armor_powerup);
		}

		DisableButton ();

		SaveLoadManager.s_inst.SaveFile ();

		reset_time = 10;
		StartCoroutine (Reset_Time ());
    }

	private void DisableButton() {
		GetComponent<UIButton> ().Disable ();
		foreach (GameObject child in button_parts)
			child.SetActive (false);
	}

	private void EnableButton() {
		GetComponent<UIButton> ().Enable ();
		foreach (GameObject child in button_parts)
			child.SetActive (true);
	}

	private IEnumerator Reset_Time()
	{
		if (value == 0) {
			reset_bar.SetActive (false);
			yield break;
		}
		reset_bar.SetActive (true);

		while (--reset_time > 0) {
			reset_bar.GetComponent<UIProgressBar> ().value = (10 - reset_time) / 10;
			yield return new WaitForSeconds (1);
		}
		EnableButton ();
	}

    private IEnumerator AcePowerUp()
    {
        float m_ace_x = 7;
        GetComponent<AudioSource>().Play();
        for (var f = 2.0; f >= 0; f -= 0.1)
        {
            GameObject go = (GameObject) Instantiate(m_ace_powerup, new Vector3(m_ace_x, 17.4f), Quaternion.identity);
            go.GetComponent<PistolBullet>().Fire(new Vector3(1,-4,0));
            m_ace_x += 0.4f;
            yield return new WaitForSeconds(0.1f);
        }
    }

	private void UpdatePowerUpLabel(int x) {
		value = x;
		powerup_number.text = x.ToString();
	}
}