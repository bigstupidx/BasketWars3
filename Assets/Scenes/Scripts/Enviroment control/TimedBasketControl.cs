using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TimedBasketControl : MonoBehaviour 
{
	public GameObject m_back_net;
	Animator m_anim;
	public bool m_is_folded = true;
	public float m_up_time;
	public float m_down_time;

	// Use this for initialization
	void Start () 
	{
		m_anim = GetComponent<Animator>();
		if(m_is_folded)
		{
			m_anim.Play("NetFoldedIdle");
			m_back_net.GetComponent<Renderer>().enabled = false;
		}
		Invoke("FoldNet",m_up_time);
	}

	public void RaiseNet()
	{
		m_anim.Play("NetUnfold");
		m_is_folded = false;
		Invoke("FoldNet",m_up_time);
	}

	public void FoldNet()
	{
		m_anim.Play("NetFrontFold");
		m_back_net.GetComponent<Renderer>().enabled = false;
		Invoke("RaiseNet",m_down_time);
	}

	public void Idle(){
		m_anim.Play("NetFrontIdle");
		m_back_net.GetComponent<Renderer>().enabled = true;
	}

}
