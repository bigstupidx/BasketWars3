using UnityEngine;
using System.Collections;

public class PowerUpButton : MonoBehaviour 
{
    public GameObject m_ace_powerup;
    public int current_x_position;

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
        for (var f = 1.0; f >= 0; f -= 0.1)
        {
            Instantiate(m_ace_powerup, new Vector3(current_x_position, 16), Quaternion.identity);
            current_x_position++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
