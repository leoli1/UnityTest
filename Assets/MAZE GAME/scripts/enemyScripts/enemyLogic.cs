using UnityEngine;
using System.Collections;

public class enemyLogic : MonoBehaviour {
	
	public int myFloor;
	public float speed = 1;
	public bool ableToAttack;
	public float attackDistance = 3;
	public float attackDamage = 1;
	float attackingWaitTime = 1f;
	float attackTime;
	public GameObject[] missileStarts;
	Animator animator;
	public GameObject player;
	public GameObject missile;
	NavMeshAgent NMA;
	void Start () 
	{
		NMA = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		NMA.speed = speed;
	}

	void Update () 
	{
		if (myFloor == playerSystem.pS.currentFloorNumber) 
		{
			if (NMA.enabled)
			{
				Quaternion idealRotation = Quaternion.LookRotation (NMA.velocity.normalized);
				transform.rotation = Quaternion.Slerp (transform.rotation, idealRotation, 0.3f);
				NMA.SetDestination (player.transform.position);
				if (animator != null) 
				{
					animator.speed = NMA.speed;
					animator.SetBool ("Walking", true);
				}
			}
		}
		float distToPlayer = (player.transform.position - transform.position).magnitude;
		if (distToPlayer<1f) 
		{
			//NMA.enabled = false;
			if (animator != null) 
			{
				//animator.SetBool ("Walking", false);
			}
		} 
		else 
		{
			NMA.enabled = true;
		}
		if (ableToAttack) 
		{
			RaycastHit testPlayerHitInfo;
			if (Physics.Raycast(transform.position+Vector3.up/2, (player.transform.position-transform.position).normalized, out testPlayerHitInfo))
			{
				if (testPlayerHitInfo.collider.gameObject.tag=="Player")
				{
					Quaternion idealRotation2 = Quaternion.LookRotation((player.transform.position-transform.position).normalized);
					transform.rotation = Quaternion.Slerp(transform.rotation, idealRotation2, 0.4f);
				}
			}
			RaycastHit hitInfo;
			Vector3 dir = (player.transform.position+Vector3.up/2-transform.position).normalized;
			if (Physics.Raycast(transform.position+Vector3.up/2, transform.rotation*Vector3.forward, out hitInfo))
			{
				if (hitInfo.collider.gameObject==player)
				{
					if (Time.time>=attackTime)
					{
						if (hitInfo.distance<=attackDistance)
							attack();
						attackTime = Time.time+attackingWaitTime;
					}
				}
			}
		}
	}
	void attack()
	{
		for (int i=0; i<missileStarts.Length; i++)
		{
			GameObject instantMissile = (GameObject)Instantiate(missile, missileStarts[i].transform.position, transform.rotation);
			instantMissile.transform.Rotate(new Vector3(0, 180, 0));
			instantMissile.GetComponent<missileScript>().damage = attackDamage;
		}
	}
}
