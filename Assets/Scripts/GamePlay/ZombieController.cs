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
        walk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (walk)
            transform.position += new Vector3(-0.001f * zombie_speed, 0, 0);
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Finish")
        {
            GameManager.s_Inst.FailedLevel();
        }
    }
}
