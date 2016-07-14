using UnityEngine;
using System.Collections;
using System;

public class ZombieManager : MonoBehaviour {

    public GameObject small_zombie;
    public GameObject large_zombie;

    private GameManager game_manager;

    private int wave_number;

    private float timer;
    private float in_wave_timer;

    public float max_zombies;

    private int current_zombies;
    public GameObject wave_text;

    private GameObject[] pivot_points;
    private int previous_pivot;

    public float zombie_timer;


    // Use this for initialization
    void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Physics2D.IgnoreLayerCollision(14, 14);
        Physics2D.IgnoreLayerCollision(0, 14);

        wave_number = 1;
        timer = 0f;
        in_wave_timer = 0f;
        current_zombies = 0;
        previous_pivot = 0;

        //Wave Text
        wave_text.SetActive(true);
        wave_text.GetComponent<UILabel>().text = "Wave 1";

        pivot_points = GameObject.FindGameObjectsWithTag("Pivot");
        Array.Sort(pivot_points, CompareNames);
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        in_wave_timer += Time.deltaTime;
        if (wave_text.activeSelf && timer > 2.5f)
        {
            wave_text.SetActive(false);
        }
	    if (timer >= 15f || (current_zombies == max_zombies && game_manager.noZombie()))
        {
            if (wave_number == 5 && game_manager.noZombie())
            {
                GameManager.s_Inst.FinishedLevel();
            } else if (wave_number != 5)
            {
                wave_number++;
                timer = 0f;
                in_wave_timer = 0f;
                wave_text.SetActive(true);
                wave_text.GetComponent<UILabel>().text = "Wave " + wave_number;
                current_zombies = 0;
                max_zombies = max_zombies * 1.2f;
            }
        }
        if (current_zombies < max_zombies)
        {
            wave_management();
        }
	}

    private void wave_management()
    {
        if (in_wave_timer > zombie_timer)
        {
            GameObject go;
            current_zombies++;
            in_wave_timer = 0;
            if (current_zombies % 10 == 0)
                go = Create_zombie(large_zombie);
            else
                go = Create_zombie(small_zombie);
            speed_zombies(go);
            sorting_order_zombie(go);
            //scale_zombies(go);
            game_manager.addZombie(go);
        }
    }

    private GameObject Create_zombie(GameObject type )
    {
        int temp = UnityEngine.Random.Range(0, pivot_points.Length);
        if (pivot_points.Length > 1)
        {
            while (previous_pivot == temp)
            {
                temp = UnityEngine.Random.Range(0, pivot_points.Length);
            }
        }
        previous_pivot = temp;
        return (GameObject)Instantiate(type, transform.position + new Vector3(0,previous_pivot), Quaternion.identity);
    }

    private void sorting_order_zombie(GameObject Zombie)
    {
        if (previous_pivot == 0)
            Zombie.GetComponent<SpriteRenderer>().sortingLayerName = "Play Area";
        Zombie.GetComponent<SpriteRenderer>().sortingOrder = pivot_points.Length - previous_pivot;
    }

    private void scale_zombies(GameObject Zombie)
    { 
        Zombie.GetComponent<Transform>().localScale -= new Vector3( .0014f*(previous_pivot), .0014f*(previous_pivot));
    }
    
    private void speed_zombies(GameObject Zombie)
    {
        if (UnityEngine.Random.Range(0, 10) > 8)
            Zombie.GetComponent<ZombieController>().zombie_speed = 8;
        else 
            Zombie.GetComponent<ZombieController>().zombie_speed = 3;
    }

    private int CompareNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }
}
