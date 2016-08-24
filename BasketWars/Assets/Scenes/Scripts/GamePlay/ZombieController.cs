using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public float zombie_speed;
    public bool walk;
    public int health;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-0.1f*zombie_speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Finish")
        {
            if (c.gameObject.name == "ZombieNearFinish")
            {
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Zombie Wave";
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                }
            else
                GameManager.s_Inst.OnZombieReachBase(gameObject);
        }
    }
}
