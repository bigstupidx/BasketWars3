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

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Finish")
        {
            GameManager.s_Inst.OnZombieReachBase(gameObject);
        }
    }
}
