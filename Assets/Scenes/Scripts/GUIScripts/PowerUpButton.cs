using UnityEngine;
using System.Collections;

public class PowerUpButton : MonoBehaviour 
{
    public GameObject m_ace_powerup;
    private float m_ace_y_angle;
    private float m_ace_x_angle;

	// Use this for initialization
	void Start () {
		if(GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.None)
			gameObject.SetActive(false);
	}

	public void OnClick(){
        if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.AcePowerup)
        {
            StartCoroutine(AcePowerUp());
            //gameObject.SetActive(false);
        }
    }

	public void TurnOffFocusMode(){
		GameManager.s_Inst.m_is_in_focus_mode = false;
		GameManager.m_nuke_explosion = false;
		GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach(GameObject g in obs){
			if(g.GetComponent<TweenPosition>() != null){
				g.GetComponent<TweenPosition>().duration /= 2;
			}
		}
	}

    private IEnumerator AcePowerUp()
    {
        m_ace_y_angle = 4;
        m_ace_x_angle = 1;
        GetComponent<AudioSource>().Play();
        for (var f = 2.0; f >= 0; f -= 0.1)
        {
            GameObject go = (GameObject) Instantiate(m_ace_powerup, new Vector3(7, 16.4f), Quaternion.identity);
            go.GetComponent<PistolBullet>().Fire(new Vector3(m_ace_x_angle,-m_ace_y_angle,0));
            m_ace_y_angle -= 0.16f;
            m_ace_x_angle += 0.04f;
            yield return new WaitForSeconds(0.1f);
        }
        m_ace_y_angle = 4;
    }
}
