using UnityEngine;
using System.Collections;

public class movement1 : MonoBehaviour {

	public float speed = 4;
	void Start () 
	{
	}

	void Update () 
	{
		GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal")*speed, 0, Input.GetAxis("Vertical")*speed));
	}
}
