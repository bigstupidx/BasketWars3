using UnityEngine;
using System.Collections;

public class PowerUpButton : MonoBehaviour 
{
    public GameObject m_ace_powerup;
    private float m_ace_x;

	// Use this for initialization
	void Start () {
		if(GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.None)
			gameObject.SetActive(false);
	}

	public void OnClick(){
        if (GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.AcePowerup)
        {
            StartCoroutine(AcePowerUp());
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
        m_ace_x = 7;
        GetComponent<AudioSource>().Play();
        for (var f = 2.0; f >= 0; f -= 0.1)
        {
            GameObject go = (GameObject) Instantiate(m_ace_powerup, new Vector3(m_ace_x, 17.4f), Quaternion.identity);
            go.GetComponent<PistolBullet>().Fire(new Vector3(1,-4,0));
            m_ace_x += 0.4f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
