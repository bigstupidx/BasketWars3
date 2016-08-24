using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BuildingSparkController : MonoBehaviour 
{
	int m_spark_count;
	float m_play_timer;
	Animator m_anim;
	public enum SparkType
	{
		Building,
		Building1,
		Gun,
		Gun2,
	}
	[SerializeField]
	public SparkType m_spark_type;
	// Use this for initialization
	void Start () 
	{
		m_anim = GetComponent<Animator>();
	 	m_spark_count = Random.Range(3,5);
		m_play_timer = Random.Range(3.0f,7.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_spark_count <= 0)
		{
			m_spark_count = Random.Range(3,5);
			m_play_timer = Random.Range(3.0f,7.0f);
		}
		m_play_timer -= Time.deltaTime;
		if(m_play_timer <= 0)
		{
			PlaySpark();
			m_play_timer = 0.1f;
		}
	}

	void PlaySpark()
	{
		if(m_spark_type == SparkType.Building)
			m_anim.Play("BuildingSpark");
		else if(m_spark_type == SparkType.Building1)
			m_anim.Play("BuildingSpark1");
		else if(m_spark_type == SparkType.Gun)
			m_anim.Play("GunSpark");
		else if(m_spark_type == SparkType.Gun2)
			m_anim.Play("GunSpark1");
		m_spark_count--;
	}
}
