using UnityEngine;
using System.Collections;

public class DrawTrajectory : MonoBehaviour {
	public Transform point;
	public Transform previousPoint;
	public Transform AimingPoint;
	
	public static bool drawPath = false;
	public static bool drawFullPath = false;
	public static int showPathCount = 11;
	private float deltaTime = 0.07f;
	private float maximumTime = 2.5f;
	private Vector3 ballPosition;
	private Transform[] pathPoint;
	//private Transform[] previousPathPoint;
	private int pathCount;
	public static Vector2 m_shoot_direction;	
	//LineRenderer m_l_renderer;
	private static DrawTrajectory _inst;
	public static DrawTrajectory Inst
	{	
		get{
			return _inst;
		}		
	}

	float vx;
	float vy;
	// Use this for initialization
	void Start () {
		_inst = this;
		drawPath = false;
		vx = m_shoot_direction.x;
		vy = m_shoot_direction.y;
		GameObject ball = GameObject.Find("BallSpawnPoint");
		if(ball != null)
			ballPosition = ball.transform.position;
		
		pathCount = (int)(maximumTime / deltaTime); 
		pathPoint = new Transform[(int) pathCount];

		
		for (int i = 0 ; i < pathPoint.Length ; i++) 
		{
			if (i == pathCount-1) 
			{
				pathPoint[i] = (Transform) Instantiate(AimingPoint, new Vector3(-100,-100,100), Quaternion.identity);
			}
			else 
			{
				pathPoint[i] = (Transform) Instantiate(point, new Vector3(-100,-100,100), Quaternion.identity);
			}
			
			
			pathPoint[i].parent = transform;
		}
		
		ballPosition = Vector3.zero;
	}

	void OnLevelWasLoaded()
	{
		if( pathPoint == null)
			return;
		for (int i = 0 ; i < pathPoint.Length ; i++) 
		{
			pathPoint[i].position = new Vector3(-100,-100,100);
			//previousPathPoint[i].position = new Vector3(-100,-100,100);			                      
		}
	}

	// Update is called once per frame
	void Update () {		
		if (drawPath) {
			GameObject ball = GameObject.Find("BallSpawnPoint");
		    ballPosition = ball.transform.position;
			transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
			
			vx = m_shoot_direction.x;
			vy = m_shoot_direction.y;
			PlotTrajectory(ballPosition, new Vector3(vx, vy, 0), deltaTime, maximumTime);
		}
		else if (pathPoint[0].GetComponent<Renderer>().enabled)
        {
            for (int i = 0; i < pathPoint.Length; i++)
                pathPoint[i].GetComponent<Renderer>().enabled = false;

        }
	}
	
	public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) 
	{
		for (int i=1;;i++) 
		{
			float t = timestep*i;
			if (t > maxTime) 
				break;
			
			Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);            
			if (drawPath) 
			{
				pathPoint[i-1].position = pos;
				if (!drawFullPath && showPathCount < i ) {
					pathPoint[i-1].GetComponent<Renderer>().enabled = false;
					pathPoint[i-2].localScale = Vector3.one * 0.6f;
				}
				else 
					pathPoint[i-1].GetComponent<Renderer>().enabled = true;
			}
		}
	}

	public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
		return start + (startVelocity * time) + Physics.gravity * time * time;
	}

	public void drawPreviousPath () {		
		vx = m_shoot_direction.x;
		vy = m_shoot_direction.y;		
		
		PlotTrajectory(new Vector3(ballPosition.x, ballPosition.y, 0.05f + transform.position.z),new Vector3(vx, vy, 0), deltaTime, maximumTime);
		foreach(Transform t in pathPoint)
		{
			t.GetComponent<Renderer>().enabled = false;
		}
		ballPosition = Vector3.zero;
	}

	void ToggleThrowVision(bool tf)
	{
		if(tf)
			showPathCount = 50;
		else
			showPathCount = 9 ;
	}
}
