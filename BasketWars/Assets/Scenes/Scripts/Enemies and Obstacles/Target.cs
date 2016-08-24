using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
	public enum TargetType{
		Conveyor,
		FoldedRim,
		Windmill,
		TrapDoor,
		BonusBlimp,
		BonusBlimpMini,
		None
	};
	public TargetType m_target_type;
	private Animator anim;
	public bool m_is_hit = false;
	AudioSource m_audio_source;
	Vector3 m_start_pos;
	//Conveyor
	public GameObject basket;
	ConveyorBelt m_conveyor;
	//Folded Rim
	public TimedBasketControl m_basket_control;	
	public float m_folded_time;
	float m_folded_time_left;

	//Windmill
	public RotateObject m_windmill;
	public float m_pause_time;
	float m_pause_time_left;

	//TrapDoor
	public GameObject m_trapdoor;
	public float m_open_time;
	float m_open_time_left;

	//Bonus Blimp
	public GameObject m_Explosion;
	public int m_hits_left;

	//Bonus Blimp Mini
	public Sprite m_time_target_sprite;

	void Start () 
	{
		m_start_pos = transform.position;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		m_audio_source = GetComponent<AudioSource>();	
		if(m_target_type == TargetType.Conveyor){
			if(basket != null)
				m_conveyor = basket.GetComponent<ConveyorBelt>();
		}
		if(m_target_type == TargetType.BonusBlimp){
			m_hits_left = Random.Range(0,4);
			ChangeColor();
		}
		else if(m_target_type == TargetType.BonusBlimpMini){
			
		}
	}

	void Update(){
		if(m_target_type == TargetType.Conveyor){
			if(m_is_hit && basket != null){
				if(m_conveyor.m_pause_timer <= 0){
					Reset();
				}
			}
		}
		else if(m_target_type == TargetType.FoldedRim){
			if(m_is_hit){
				m_folded_time_left -= Time.deltaTime;
				if(m_folded_time_left <= 0){
					Reset();
				}
			}
		}
		else if(m_target_type == TargetType.Windmill){
			if(m_is_hit){
				m_pause_time_left -= Time.deltaTime;
				if(m_pause_time_left <= 0){
					StartCoroutine(m_windmill.LerpToFullSpeed(1));
					Reset();
				}
			}
		}
		else if(m_target_type == TargetType.TrapDoor){
			if(m_is_hit){
				m_open_time_left -= Time.deltaTime;
				if(m_open_time_left <= 0){
					m_trapdoor.GetComponent<TweenPosition>().PlayReverse();
					Reset();
				}
			}
		}
	}

	/// <summary>
	/// Check to see if the target gets hit by a bullet.
	/// </summary>
	void OnTriggerEnter2D (Collider2D c) 
	{		
		if(c.gameObject.tag == "Weapon") 
		{
			/*if(GameManager.s_Inst.m_current_game_state != GameManager.GameState.Tutorial){
				AchievementManager.Ding();
				if(m_is_hit == false){
					GameManager.s_Inst.m_total_targets_hit++;
					AchievementManager.YourePrettyGoodAtThis(GameManager.s_Inst.m_total_targets_hit);
				}
			}*/
			m_audio_source.Play();
			if(m_target_type != TargetType.BonusBlimp){
				anim.Play("BullseyeHit");
				m_is_hit = true;
				c.gameObject.GetComponent<Weapon>().DestroyBullet();
			}
			GameManager.s_Inst.AddScore(10);
			if(m_target_type == TargetType.Conveyor){
				if(basket != null){
					m_conveyor.PauseHoop();
				}
			}
			else if(m_target_type == TargetType.FoldedRim){
				m_basket_control.RaiseNet();
			}
			else if(m_target_type == TargetType.Windmill){
				m_pause_time_left = m_pause_time;
				StartCoroutine(m_windmill.LerpToStop(1));
			}
			else if(m_target_type == TargetType.TrapDoor){
				m_open_time_left = m_open_time;
				m_trapdoor.GetComponent<TweenPosition>().PlayForward();
			}
			else if(m_target_type == TargetType.BonusBlimp){
				m_hits_left--;
				ChangeColor();
				c.gameObject.GetComponent<Weapon>().DestroyBullet();
				if(m_hits_left < 0){
					gameObject.GetComponent<Collider2D>().enabled = false;
					Instantiate(m_Explosion,gameObject.transform.position,Quaternion.identity);
					Destroy(this.gameObject);
				}
			}
			else if(m_target_type == TargetType.BonusBlimpMini){
				gameObject.GetComponent<Collider2D>().enabled = false;
				Instantiate(m_Explosion,gameObject.transform.position,Quaternion.identity);
				GameManager.s_Inst.m_game_timer += 10;
				Destroy(this.gameObject);
			}
		}
	}

	void ChangeColor(){
		SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
		if(m_hits_left == 3){
		 	s.color = Color.blue;
		}else if(m_hits_left == 2){
			s.color = Color.green;
		}else if(m_hits_left == 1){
			s.color = Color.yellow;
		}else if(m_hits_left == 0){
			s.color = Color.white;
		}

	}

	/// <summary>
	/// Reset the target to default state.
	/// </summary>
	public void Reset()
	{
		transform.position = m_start_pos;
		anim.Play("BullseyeIdle");
		m_is_hit = false;
	}
}
