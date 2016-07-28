using UnityEngine;
using System.Collections;

public class SoliderMovement : MonoBehaviour {

    public int position;
    private BallController ball;

    void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
    }

    public void OnClick()
    {
        GameManager.s_Inst.m_soldier.transform.position += new Vector3(0, -(GameManager.s_Inst.soldier_position-position), 0);
        GameManager.s_Inst.soldier_position = position;
        ball.MoveBallToPivot();
    }
}
