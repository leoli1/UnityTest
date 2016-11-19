using UnityEngine;
using System.Collections;

public class thirdpersonController : MonoBehaviour {

	public float speed = 4;
	public float rotationSpeed = 3;
	public float maximumSpeed = 2;
	Animator animator;
	void Start () 
	{
		animator = GetComponent<Animator> ();

	}

	void Update () 
	{
		float forwardInput = Input.GetAxis ("Vertical");
		float forwardSpeed = forwardInput * speed * 3;
		float sideInput = Input.GetAxis ("Horizontal");
		animator.speed = 1;
		if (forwardInput != 0) 
		{
			animator.SetBool ("Running", true);
			animator.SetBool("Idle", false);
			animator.speed = Mathf.Abs(forwardSpeed)/7;
		} 
		else 
		{
			animator.SetBool("Running", false);
			if (!playerSystem.pS.shooting)
			{
				animator.SetBool("Idle", true);
				//animator.speed = 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.V)) 
		{
			playerSystem.pS.shooting = true;
			animator.SetBool("Idle", false);
			animator.SetBool("Shooting", true);
		}
		else if (Input.GetKeyUp (KeyCode.V)) 
		{
			playerSystem.pS.shooting = false;
			animator.SetBool("Shooting", false);
		}
		transform.Rotate (Vector3.up * rotationSpeed * 2 * sideInput*Time.deltaTime*30);
		//Debug.Log (new Vector3(0, rotationSpeed*2*sideInput, 0));
		//rigidbody.AddTorque (new Vector3(0, rotationSpeed*2*sideInput, 0));
		GetComponent<Rigidbody>().AddForce (transform.rotation * (Vector3.forward * forwardSpeed)*Time.deltaTime*30);
		checkSpeed ();
	}
	void checkSpeed()
	{
		Vector3 sideSpeed = GetComponent<Rigidbody>().velocity;
		sideSpeed.y = 0;
		if (sideSpeed.magnitude <= maximumSpeed) 
						return;
		Vector3 newVelo = sideSpeed.normalized*maximumSpeed+(Vector3.up*GetComponent<Rigidbody>().velocity.y);
		GetComponent<Rigidbody>().velocity = newVelo;
	
	}
}
