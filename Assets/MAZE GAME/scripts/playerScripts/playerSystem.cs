using UnityEngine;
using System.Collections;

public class playerSystem : MonoBehaviour {

	public static playerSystem pS;
	public int currentFloorNumber = 1;
	public GameObject currentFloor;
	public GameObject laser;
	public Transform laserStartPoint;
	public float shootingTimeDifference = 0.4f;
	float shootingTime = 0;
	public int money = 0;
	public bool shooting = false;
	public int missiles = 100;
	public bool pause = false;

	void Awake () 
	{
		pS = this;
		setNewFloor (1);
		Screen.lockCursor = true;
		Cursor.visible = false;
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			continueGame();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			pauseGame();
		}
		if (shooting) 
		{
			if (Time.time>shootingTime)
			{
				if (missiles>0)
				{
					Instantiate(laser, laserStartPoint.position, transform.rotation);
					shootingTime = Time.time + shootingTimeDifference;
					//missiles -= 1;
				}
			}
		}
	}
	void continueGame()
	{
		pause = false;
		Screen.lockCursor = true;
		Cursor.visible = false;
		Time.timeScale = 1;
	}
	void pauseGame()
	{
		pause = true;
		Screen.lockCursor = false;
		Cursor.visible = true;
		Time.timeScale = 0;
	}
	void OnCollisionEnter(Collision col)
	{
		GameObject GO = col.collider.gameObject;
		if (GO.tag == "HealthCube") 
		{
			playerHealth.pH.AddHealth(10);
			Destroy(GO);
		}
	}
	void OnTriggerEnter(Collider col)
	{
		string name = col.gameObject.name;
		if (name.StartsWith ("floor")) 
		{
			setNewFloor(int.Parse((name[5]).ToString()));
		}
	}
	void setNewFloor(int id)
	{
		currentFloorNumber = id;
		currentFloor = GameObject.Find ("floor" + currentFloorNumber.ToString ());
	}
	void OnGUI()
	{
		GUI.Box (new Rect (150, 5, 120, 25), "Missiles: " + missiles.ToString ());
	}
}
