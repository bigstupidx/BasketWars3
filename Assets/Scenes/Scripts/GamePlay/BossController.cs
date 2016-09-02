using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	public int health = 200;
	private Vector2[] row;
	private Rigidbody2D rb;
	private Animator anim;
	public bool stun = false;
	private bool attack_mode = false;
	private float attack_timer = 0;
	private bool at_endpoint = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		StartCoroutine (Boss_Movement ());
	}

	public void set_row(Vector2 one, Vector2 two, Vector2 three) {
		row = new Vector2[3];
		row [0] = one;
		row [1] = two;
		row [2] = three;
		transform.position = row [0];
	}

	private IEnumerator Boss_Movement() {
		while (true) {
			attack_timer += Time.deltaTime;
			if (!attack_mode) {
				if (!stun) {
					float temp = Mathf.Sin (Time.time) * 0.005f;
					rb.MovePosition (new Vector2 (rb.position.x + temp, rb.position.y + temp));
				}

				if (attack_timer >= 8f) {
					if (stun) {
						attack_timer = 0;
						anim.speed = 1;
						stun = false;
					} else {
						attack_mode = true;
						rb.velocity = new Vector2 (-2f, 0);
					}
				}
			}
			yield return null;
		}
	}

	public void stun_boss() {
		stun = true;
		anim.speed = 0;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (!at_endpoint && c.gameObject.tag == "Finish" && c.transform.position.y - transform.position.y < 0.09) {
			at_endpoint = true;
			StartCoroutine (Attack_And_Go_Back ());
		}
	}

	private IEnumerator Attack_And_Go_Back() {
		//GameManager.s_Inst.RemoveLife(35);
		//Possible attack annimation here?
		rb.velocity = new Vector2 (2f, 0);
		float i = 0;
		while(i < 3f) {
			i += Time.deltaTime;
			yield return null;
		}
		attack_timer = 0;
		attack_mode = false;
		at_endpoint = false;
	}
}