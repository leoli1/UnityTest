using UnityEngine;
using System.Collections;

public enum CameraTypes
{
	Following,
	playerChangable,
	Static,
	FollowingOrtho
}

public class cameraMoving : MonoBehaviour 
{
	// following:
	public float followigDistance = 8;
	public float followDaming = 0.5f;
	public float rotationDamping = 0.5f;
	public float normalAngle = 50;
	Vector3 relativePosition;

	// player Changable
	public float maxXRotation = 80;
	public float minXRotation = 20;
	float minDistance = 2;
	float maxDistance = 10;
	float distance = 8;
	float yRotation = 0;
	float xRotation = 50;

	// static:
	public Vector3 staticPosition;
	
	public GameObject player;
	public CameraTypes cameraType;
	void Start () 
	{
		relativePosition = transform.position - player.transform.position;
	}

	void Update () 
	{
		GetComponent<Camera>().orthographic = false;
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			switch (cameraType)
			{
			case CameraTypes.Following:
				cameraType = CameraTypes.playerChangable;
				break;
			case CameraTypes.playerChangable:
				cameraType = CameraTypes.Static;
				break;
			case CameraTypes.Static:
				cameraType = CameraTypes.FollowingOrtho;
				break;
			case CameraTypes.FollowingOrtho:
				cameraType = CameraTypes.Following;
				break;
			
			}
		}
		switch (cameraType) 
		{
		case CameraTypes.Following:
			Following();
			break;
		case CameraTypes.playerChangable:
			playerChangable();
			break;
		case CameraTypes.Static:
			Static();
			break;
		case CameraTypes.FollowingOrtho:
			FollowingOrtho();
			break;

		}
	}
	void Following()
	{
		Quaternion rot = Quaternion.Euler(new Vector3(normalAngle, player.transform.rotation.eulerAngles.y, 0));
		Vector3 idealPosition = rot*Vector3.forward*(-followigDistance)+player.transform.position;
		transform.position = Vector3.Lerp(transform.position, idealPosition, Time.timeScale * followDaming);
		Quaternion idealRotation = Quaternion.LookRotation(player.transform.position-transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, idealRotation, Time.timeScale * rotationDamping);
	}
	void playerChangable()
	{
		yRotation += Input.GetAxis ("Mouse X");
		xRotation -= Input.GetAxis ("Mouse Y");
		distance -= Input.GetAxis ("Mouse ScrollWheel");
		clampPS ();
		Quaternion rot = Quaternion.Euler(new Vector3(xRotation, yRotation, 0));
		Vector3 idealPosition = rot*Vector3.forward*(-distance)+player.transform.position;
		transform.position = Vector3.Lerp(transform.position, idealPosition, Time.timeScale * followDaming);
		Quaternion idealRotation = Quaternion.LookRotation(player.transform.position-transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, idealRotation, Time.timeScale*rotationDamping);

	}
	void clampPS()
	{
		xRotation = Mathf.Clamp (xRotation, minXRotation, maxXRotation);
		distance = Mathf.Clamp (distance, minDistance, maxDistance);
	}
	void Static()
	{
		transform.position = Vector3.Lerp (transform.position, staticPosition, Time.timeScale*0.3f);
		Quaternion idealRotationS = Quaternion.LookRotation(player.transform.position-transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, idealRotationS, Time.timeScale*0.3f);
	}
	void FollowingOrtho()
	{
		Following ();
		GetComponent<Camera>().orthographic = true;
	}
}
