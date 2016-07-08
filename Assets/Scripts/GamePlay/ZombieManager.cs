using UnityEngine;
using System.Collections;
using System;

public class ZombieManager : MonoBehaviour {

    public GameObject small_zombie;
    public GameObject large_zombie;

    private int wave_number;

    private float timer;
    private float in_wave_timer;

    public float max_zombies;

    private int current_zombies;
    public GameObject wave_text;

    private GameObject[] pivot_points;
    private int previous_pivot;

    // Use this for initialization
    void Start()
    {
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
	    if ((current_zombies >= max_zombies && GameObject.FindGameObjectsWithTag("Zombie").Length == 0) || timer > 15f)
        {
            if (wave_number == 5 && GameObject.FindGameObjectsWithTag("Zombie").Length == 0)
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
        GameObject go;
        if (in_wave_timer > 1.05 && UnityEngine.Random.Range(0,10) > 8)
        {
            current_zombies++;
            in_wave_timer = 0;
            go = Create_zombie();
            speed_zombies(go);
            sorting_order_zombie(go);
            scale_zombies(go);
        }
    }

    private GameObject Create_zombie()
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
        if (UnityEngine.Random.Range(0, 6) == wave_number)
            return (GameObject)Instantiate(large_zombie, transform.position + new Vector3(0, previous_pivot + 0.4f), Quaternion.identity);

        else
            return (GameObject)Instantiate(small_zombie, transform.position + new Vector3(0,previous_pivot), Quaternion.identity);
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
            Zombie.GetComponent<ZombieController>().zombie_speed = 3 + 0.01f * (UnityEngine.Random.Range(0, 100));
    }

    private int CompareNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }
}
