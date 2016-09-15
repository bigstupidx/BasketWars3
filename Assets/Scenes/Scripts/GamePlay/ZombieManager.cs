using UnityEngine;
using System.Collections;
using System;

public class ZombieManager : MonoBehaviour {

    public GameObject small_zombie;
    public GameObject large_zombie;

    private GameManager game_manager;
	public LevelInitializer level_init;

    private short wave_number;
	private short total_waves;

	private short small_zombie_health;
	private short small_zombie_dmg;

    private float timer;

    private int current_zombies;
    public GameObject wave_text;

    public int pivot_points;
    public float zombie_timer;

	private int current_pivot_position = 0;


    // Use this for initialization
    void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		total_waves = level_init.waves;
		small_zombie_dmg = level_init.small_zombie_dmg;
		small_zombie_health = level_init.small_zombie_health;
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
		current_pivot_position = (current_pivot_position + 1) % pivot_points;
    	GameObject go;
		go = (GameObject)Instantiate(type, transform.position + new Vector3(current_pivot_position*0.4f,current_pivot_position), Quaternion.identity);
      	game_manager.addZombie(go);
		sorting_order_zombie(go, current_pivot_position);
		set_zombie(go);
    }

    private void sorting_order_zombie(GameObject Zombie, int pivot_position)
    {
        if (pivot_position == 0)
            Zombie.GetComponent<SpriteRenderer>().sortingLayerName = "Play Area";
		Zombie.GetComponent<ZombieController> ().set_pivot_row (pivot_position);
        Zombie.GetComponent<SpriteRenderer>().sortingOrder = pivot_points - pivot_position;
    }

    
    private void set_zombie(GameObject Zombie)
    {
		ZombieController temp = Zombie.GetComponent<ZombieController> ();
		if (Zombie.tag == "Zombie") {
			temp.set_zombie_dmg (small_zombie_dmg);
			temp.health = small_zombie_health;
		}

		if (level_init.zombie_speeds.Length == 1)
			temp.zombie_speed = level_init.zombie_speeds [0];
		else {
			int randomSpeedNum = UnityEngine.Random.Range(0,100);
			for (int i = 0; i < level_init.zombie_speeds.Length; ++i) {
				if (randomSpeedNum < level_init.zombie_speed_rates [i]) {
					temp.zombie_speed = level_init.zombie_speeds [i];
					return;
				}
			}
		}
    }

    private int CompareNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }

    private IEnumerator waves()
    {
		for (wave_number = 1; wave_number <= total_waves; wave_number++)
        {
            timer = 0;
            wave_text.SetActive(true);
            wave_text.GetComponent<UILabel>().text = "Wave " + wave_number;
			for (current_zombies = 0; current_zombies < level_init.zombies_spawn_wave[wave_number-1]; current_zombies++)
            {
                Create_zombie(small_zombie);
				yield return new WaitForSeconds(zombie_timer + UnityEngine.Random.Range(0,10)*.08f);
            }
			while (!game_manager.noZombie ())
				yield return new WaitForFixedUpdate ();
			if (wave_number != total_waves)
				yield return new WaitForSeconds(3.4f);
            current_zombies = 0;
        }
        game_manager.FinishedLevel();
    }
}
