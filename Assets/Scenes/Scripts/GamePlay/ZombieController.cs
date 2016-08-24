using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public float zombie_speed;
    public bool at_endpoint = false;
    public int health;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-0.1f*zombie_speed, 0);
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!at_endpoint && c.gameObject.tag == "Finish" && c.transform.position.y - transform.position.y < 0.09)
        {
            at_endpoint = true;
            StartCoroutine(OnZombieReachBase());
        }
    }

    public IEnumerator OnZombieReachBase()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        while (GameManager.s_Inst.m_lives > 0)
        {
            GameManager.s_Inst.RemoveLife(health);
            yield return new WaitForSeconds(1f);
        }
    }
}
