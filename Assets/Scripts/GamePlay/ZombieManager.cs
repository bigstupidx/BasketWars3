using UnityEngine;
using System.Collections;
using System;

public class ZombieManager : MonoBehaviour {

    public GameObject small_zombie;
    public GameObject large_zombie;

    private GameManager game_manager;

    private int wave_number;

    private float timer;

    public float max_zombies;

    private int current_zombies;
    public GameObject wave_text;

    public int pivot_points;
    public float zombie_timer;


    // Use this for initialization
    void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Physics2D.IgnoreLayerCollision(14, 14);
        Physics2D.IgnoreLayerCollision(0, 14);

        StartCoroutine(waves());
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;  
        if (wave_text.activeSelf && timer > 2.5f)
        {
            wave_text.SetActive(false);
        }
	}

    private void Create_zombie(GameObject type )
    {
        for (int i = 0; i < pivot_points; i++)
        {
            GameObject go;
            if (type == large_zombie)
                go = (GameObject)Instantiate(type, transform.position + new Vector3(0, i + 0.24f), Quaternion.identity);
            else
                go = (GameObject)Instantiate(type, transform.position + new Vector3(0,i), Quaternion.identity);
            game_manager.addZombie(go);
            sorting_order_zombie(go, i);
            speed_zombies(go);
        }
    }

    private void sorting_order_zombie(GameObject Zombie, int pivot_position)
    {
        if (pivot_position == 0)
            Zombie.GetComponent<SpriteRenderer>().sortingLayerName = "Play Area";
        Zombie.GetComponent<SpriteRenderer>().sortingOrder = pivot_points - pivot_position;
    }

    
    private void speed_zombies(GameObject Zombie)
    {
        if (UnityEngine.Random.Range(0, 10) > 8)
            Zombie.GetComponent<ZombieController>().zombie_speed = 8;
        else 
            Zombie.GetComponent<ZombieController>().zombie_speed = 3 + wave_number*0.02f;
    }

    private int CompareNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }

    public void shoot_zombies()
    {
        game_manager.SoldierFire();
    }

    private IEnumerator waves()
    {
        for (wave_number = 1; wave_number < 6; wave_number++)
        {
            timer = 0;
            wave_text.SetActive(true);
            wave_text.GetComponent<UILabel>().text = "Wave " + wave_number;
            for (current_zombies = 0; current_zombies < max_zombies; current_zombies++)
            {
                if ((current_zombies + 1) % 4 == 0)
                    Create_zombie(large_zombie);
                else
                    Create_zombie(small_zombie);
                yield return new WaitForSeconds(zombie_timer);
            }
            yield return new WaitForSeconds(5f);
            current_zombies = 0;
            max_zombies = max_zombies * 1.2f;
        }
        while (!game_manager.noZombie())
            yield return new WaitForSeconds(0.4f);
        game_manager.FinishedLevel();
    }
}
