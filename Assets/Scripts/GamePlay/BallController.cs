using UnityEngine;
using System.Collections;
#pragma warning disable 0414
[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour 
{
	Animator m_animator;	
	Animator m_eyelid_animator;	
	bool m_is_blinking = true;
	float m_blink_timer;
	public Vector3 m_start_pos;
	bool m_is_in_flight = false;
	float m_throw_time = 0;
	public float m_min_velocity;
	bool m_level_finished = false;
	Vector3 m_center;
	Vector3 m_goal_pos;
	float m_translation_time = 0;
	GameManager m_game_manager;
	float m_start_scale = 0.6520672f;
	int m_current_character;
	string m_character_name;
	int m_ground_bounce = 0;
	Transform m_ball_spawn_point;
	float resetTimer = 0;
	public bool m_is_in_air = false;
	public bool m_is_past_trigger_1;

	// Use this for initialization
	void Start () 
	{
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), (Collider2D) GameObject.Find("Zombie Endpoint").GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), (Collider2D)GameObject.Find("ZombieNearFinish").GetComponent<BoxCollider2D>());

        m_animator = gameObject.GetComponent<Animator>();
		m_current_character = GameManager.m_character_chosen;
		if(m_current_character == 0)
			m_character_name = "Ace";
		else if(m_current_character == 1)
			m_character_name = "Svet";
		else if(m_current_character == 2)
			m_character_name = "Diesel";
		else if(m_current_character == 3)
			m_character_name = "Major";
		string temp = m_character_name + "Idle";
		m_animator.Play(temp);
		m_eyelid_animator = transform.GetChild(0).GetComponent<Animator>();
		m_start_pos = transform.position;
		m_ball_spawn_point = GameObject.Find("BallSpawnPoint").transform;
		m_center = new Vector3(12,12,0);
		m_is_in_flight = false;
		if(GameObject.FindWithTag("GameManager") != null)
		{
			m_game_manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		}
		else
		{
			Debug.LogWarning("Warning: There is no Game Manager in the scene. This could be caused by hitting play in a scene in the ediotor without loading the main menu first.\nThe game is not playable without the game manager");
		}
	}

	// Update is called once per frame
	void Update () 
	{
		DoBlink();
		if(m_throw_time > 0)
		{
			m_throw_time += Time.deltaTime;
		}
		if(transform.position != m_start_pos)
		{
			if( (GetComponent<Rigidbody2D>().velocity.magnitude < m_min_velocity && transform.position.y < m_start_pos.y - 1) || m_throw_time > 10)
				ResetBall();
		}
		if(m_level_finished){
			MoveBall();
		}
	}

	public void StartSmile()
	{
		m_animator.Play(m_character_name + "ShotMade");
	}
	
	void DoBlink()
	{
		if(m_is_blinking && !m_is_in_flight)
		{
			m_blink_timer -= Time.deltaTime;
			if(m_blink_timer <= 0)
			{
				if(m_character_name == "Ace")
				{
					m_eyelid_animator.Play(m_character_name + "Blink");
					m_blink_timer = Random.Range(1.5f, 5.0f);
				}
			}
		}
	}

	public void StartBlinking()
	{
		m_is_blinking = true;
	}

	public void StopBlinking()
	{
		m_is_blinking = false;
	}

	public void Throw(Vector2 velocity)
	{
		if(!m_is_in_flight)
		{
			GetComponent<Rigidbody2D>().velocity = velocity; 
			GetComponent<Rigidbody2D>().gravityScale = 2;
			m_is_in_flight = true;
			m_is_in_air = true;
			m_throw_time += Time.deltaTime;
		}
	}

	public void ResetBall()
	{
		if(!GameManager.m_level_complete && !GameManager.s_Inst.m_failed_level){
			m_is_past_trigger_1 = false;
            ZombieCollision(false);
			m_ground_bounce = 0;
			if(!m_level_finished)
			{
				m_is_in_air = false;
				GameManager.m_ball_has_been_thrown = false;
				transform.position = m_ball_spawn_point.position;
				transform.rotation = Quaternion.identity;
				GetComponent<Rigidbody2D>().gravityScale = 0;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				GetComponent<Rigidbody2D>().angularVelocity = 0;
				m_is_in_flight = false;
				m_animator.Play(m_character_name + "Idle");
				GameObject.FindWithTag("Player").GetComponent<SoldierController>().ResetStance();
				m_throw_time = 0;
			}
		}
		else
			Invoke("MoveBall",2.0f);
	}

	public void MoveBall()
	{
		transform.rotation = Quaternion.identity;
		GetComponent<Rigidbody2D>().gravityScale = 0;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0;
		GetComponent<Collider2D>().enabled = false;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
        if (m_is_in_flight)
		{
			m_is_in_flight = false;
			m_animator.Play(m_character_name + "Idle");
		}
		if(c.gameObject.tag == "Wall"){
			ResetBall();
		}
        if (c.gameObject.tag == "Ground" || c.gameObject.tag == "Zombie")
        {
            m_ground_bounce++;
            m_is_past_trigger_1 = false;
        }
		if(c.gameObject.tag == "Obstacle"){
			m_is_past_trigger_1 = false;
		}
		if(m_ground_bounce >= 3)
			ResetBall();
		else
			gameObject.GetComponent<AudioSource>().Play();
	}

	void OnCollisionStay2D(Collision2D c)
	{
		resetTimer += Time.deltaTime;
		if(resetTimer >= 2){
			ResetBall();
			resetTimer = 0;
		}
	}

	void OnCollisionExit2D(Collision2D c)
	{
		resetTimer = 0;
	}

	public void KillBall()
	{
		m_is_in_flight = false;
		GetComponent<Rigidbody2D>().gravityScale = 0;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0;
		m_animator.Play(m_character_name + "Charred");
	}
    
    public void ZombieCollision(bool enabled)
    {
        Physics2D.IgnoreLayerCollision(10, 14, enabled);
    }
}
