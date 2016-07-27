using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BasketTrigger : MonoBehaviour {

	public int triggerID;
	bool m_is_level_passed;
	GameManager m_game_manager;
	// Use this for initialization
	void Start () {
		if(GameManager.s_Inst != null)
			m_game_manager = GameManager.s_Inst.GetComponent<GameManager>();
	}
	
	void OnTriggerEnter2D (Collider2D c) 
	{
		if(c.gameObject.tag == "Ball") 
		{		
			BallController ball = c.GetComponent<BallController>();
			if(triggerID == 1 && !ball.m_is_past_trigger_1) 
			{
				ball.m_is_past_trigger_1 = true;
				c.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.95f;
                ball.ZombieCollision(true);
			}
			else if (triggerID == 2 && ball.m_is_past_trigger_1) 
			{						
				GetComponent<AudioSource>().Play();
				transform.parent.FindChild("NetFront").GetComponent<Animator>().Play("NetFrontSwish");
				transform.parent.FindChild("NetBack").GetComponent<Animator>().Play("NetBackSwish");
				ball.StartSmile();
				ball.m_is_past_trigger_1 = false;
				m_game_manager.MadeBasket();
				ball.ResetBall();
			}
		}
		if(c.gameObject.tag == "Obstacle"){
			GameManager.s_Inst.ResetMultiplyer();
		}
	}
}
