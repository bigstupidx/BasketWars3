using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public float zombie_speed;
    public bool at_endpoint = false;
    public short health;
	private short damage;

	private IEnumerator co_routine_base;

	private int pivot_row;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-0.1f*zombie_speed, 0);
    }

    public void DestroyZombie()
    {
		Destroy (gameObject);
	}

	public void prep_DestoryZombie()
	{
		GetComponent<PolygonCollider2D>().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		if (co_routine_base != null)
			StopCoroutine (co_routine_base);
		GameManager.s_Inst.removeZombie(gameObject);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!at_endpoint && c.gameObject.tag == "Finish" && c.transform.position.y - transform.position.y < 0.09)
        {
            at_endpoint = true;
			co_routine_base = OnZombieReachBase ();
			StartCoroutine(co_routine_base);
        }
    }

	public void zombie_move_again()
	{
		if (at_endpoint) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-0.1f * zombie_speed, 0);
			at_endpoint = false;
			StopCoroutine (co_routine_base);
		}
	}

    public IEnumerator OnZombieReachBase()
    {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        while (GameManager.s_Inst.m_lives > 0)
        {
            yield return new WaitForSeconds(1f);
			GameManager.s_Inst.RemoveLife(damage);
        }
    }

	public void set_pivot_row(int x)
	{
		pivot_row = x;
	}

	public void set_zombie_dmg(short x)
	{
		damage = x;
	}

	public int get_pivot_row()
	{
		return pivot_row;
	}

	public void zombie_shot_back() {
		transform.position = new Vector2 (transform.position.x + 2, transform.position.y);
		zombie_move_again ();
	}
}
