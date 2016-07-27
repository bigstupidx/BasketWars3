using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlideToSelect : MonoBehaviour 
{
	[SerializeField]
	public List<GameObject> m_slide_items = new List<GameObject>();
	public GameObject m_selected;
	GameObject m_last_selected;
	public Vector2 m_min_size;
	public Vector2 m_max_size;
	public Rect m_scroll_area;
	Vector3 m_old_touch_pos;
	Vector3 m_current_touch_pos;
	float m_max_distance = 200;
	public Color m_selected_color;
	public Color m_unselected_color;
	public float closest_obj;
	float m_lerp_value;
	void Start()
	{
		m_selected = m_slide_items[0];
		m_last_selected = m_selected;
	}
	
	void Update () 
	{
		closest_obj = 9999999;
#if !UNITY_EDITOR
		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Debug.Log("Touch started");
				m_old_touch_pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			}
			if(Input.GetTouch(0).phase  == TouchPhase.Moved)
			{
				if(m_scroll_area.Contains(Camera.main.ScreenToWorldPoint(Input.touches[0].position)))
				{
					m_current_touch_pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
					Vector3 delta = m_current_touch_pos - m_old_touch_pos;
					foreach(GameObject g in m_slide_items)
					{
						g.transform.position += Vector3.right * delta.x;
						if(Mathf.Abs(g.transform.localPosition.x) < closest_obj)
						{
							m_selected = g;
							closest_obj = Mathf.Abs(g.transform.localPosition.x);
						}
						UpdateSize(g);
					}
					if(m_selected != m_last_selected)
					{
						NGUITools.AdjustDepth(m_selected,1);
						NGUITools.AdjustDepth(m_last_selected,-1);
					}
					m_old_touch_pos = m_current_touch_pos;
					m_last_selected = m_selected;
				}
			}
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				float delta = m_selected.transform.localPosition.x;
				foreach(GameObject g in m_slide_items)
				{
					TweenPosition tween =  g.GetComponent<TweenPosition>();
					tween.SetStartToCurrentValue();
					tween.to = g.transform.localPosition + Vector3.left * delta;
					tween.ResetToBeginning();
					tween.enabled = true;
					tween.PlayForward();
				}
				m_old_touch_pos = Vector3.zero;
				m_current_touch_pos = Vector3.zero;
			}
		}
#endif
#if UNITY_EDITOR
		if(Input.GetMouseButton(0))
		{
			if(m_scroll_area.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
			{
				m_current_touch_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if(m_old_touch_pos == Vector3.zero)
					m_old_touch_pos = m_current_touch_pos;
				Vector3 delta = m_current_touch_pos - m_old_touch_pos;
				foreach(GameObject g in m_slide_items)
				{
					g.transform.position += Vector3.right * delta.x;
					if(Mathf.Abs(g.transform.localPosition.x) < closest_obj)
					{
						m_selected = g;
						closest_obj = Mathf.Abs(g.transform.localPosition.x);
					}
				}
				if(m_selected != m_last_selected)
				{
					NGUITools.AdjustDepth(m_selected,1);
					NGUITools.AdjustDepth(m_last_selected,-1);
				}
				m_old_touch_pos = m_current_touch_pos;
				m_last_selected = m_selected;
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			float delta = m_selected.transform.localPosition.x;
			foreach(GameObject g in m_slide_items)
			{
				TweenPosition tween =  g.GetComponent<TweenPosition>();
				tween.SetStartToCurrentValue();
				tween.to = g.transform.localPosition + Vector3.left * delta;
				tween.ResetToBeginning();
				tween.enabled = true;
				tween.PlayForward();
			}
			m_old_touch_pos = Vector3.zero;
			m_current_touch_pos = Vector3.zero;
		}
#endif
		foreach(GameObject g in m_slide_items)
		{
			UpdateSize(g);
		}
	}

	void UpdateSize(GameObject g)
	{
		float pos_diff = Mathf.Abs(g.transform.localPosition.x);

		UI2DSprite s = g.GetComponent<UI2DSprite>();
		s.width = (int)Mathf.Lerp(m_max_size.x,m_min_size.x,pos_diff/m_max_distance);
		s.height = (int)Mathf.Lerp(m_max_size.y,m_min_size.y,pos_diff/m_max_distance);
		Color myColor = Color.Lerp(m_selected_color,m_unselected_color,pos_diff/m_max_distance);
	
		s.color = myColor;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(new Vector3(m_scroll_area.x,m_scroll_area.y,0),new Vector3(m_scroll_area.x + m_scroll_area.width, m_scroll_area.y,0)); //Top Line
		Gizmos.DrawLine(new Vector3(m_scroll_area.x,m_scroll_area.y + m_scroll_area.height,0),new Vector3(m_scroll_area.x + m_scroll_area.width, m_scroll_area.y + m_scroll_area.height,0)); //Bottom Line
		Gizmos.DrawLine(new Vector3(m_scroll_area.x,m_scroll_area.y,0),new Vector3(m_scroll_area.x, m_scroll_area.y + m_scroll_area.height,0)); //Left Line
		Gizmos.DrawLine(new Vector3(m_scroll_area.x + m_scroll_area.width,m_scroll_area.y,0),new Vector3(m_scroll_area.x + m_scroll_area.width, m_scroll_area.y + m_scroll_area.height,0)); //Right Line
		if(Camera.main != null)
		{
			Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(p, 0.1F);
		}
		//Gizmos.DrawGUITexture(m_drag_area,m_debug_area);
	}
}
