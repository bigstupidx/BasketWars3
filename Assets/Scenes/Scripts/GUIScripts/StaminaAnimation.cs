using UnityEngine;
using System.Collections;

public class StaminaAnimation : MonoBehaviour {
	public UIProgressBar m_stamina_bar_progress;
	public UI2DSprite m_fill_bar;
	public TweenPosition m_panel_tween;
	TweenPosition m_tween_pos;

	void Start(){
		m_tween_pos = gameObject.GetComponent<TweenPosition>();
	}

	// Update is called once per frame
	public void SetBlip(float progress) {
		m_panel_tween.onFinishedForward.Clear();
		m_panel_tween.onFinishedForward.Add(new EventDelegate(this,"StartAnimation"));
		m_panel_tween.ResetToBeginning();
		m_panel_tween.PlayForward();
		m_stamina_bar_progress.value = progress;
		Vector3 left = m_fill_bar.worldCorners[0] + (m_fill_bar.worldCorners[0] - m_fill_bar.worldCorners[1])*0.5f;
		Vector3 right = m_fill_bar.worldCorners[3] + (m_fill_bar.worldCorners[3] - m_fill_bar.worldCorners[2])*0.5f;
		Vector3 distance_x = left - right;
		transform.position = (left - distance_x * m_stamina_bar_progress.value);
		transform.localPosition -= new Vector3(gameObject.GetComponent<UI2DSprite>().localCorners[0].x,gameObject.GetComponent<UI2DSprite>().localCorners[0].y * 1.8f,0);
	}

	public void StartAnimation(){
		m_panel_tween.onFinishedForward.Clear();
		m_panel_tween.onFinishedForward.Add(new EventDelegate(this,"LoadLevel"));
		m_tween_pos.from = transform.localPosition;
		m_tween_pos.to = transform.localPosition + Vector3.up * 60;
		m_tween_pos.PlayForward();
		gameObject.GetComponent<TweenAlpha>().PlayForward();
	}

	public void LoadLevel(){
		Application.LoadLevel("LevelLoader");
	}
}
