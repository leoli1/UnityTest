using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {

	public GameObject healthbar;
	float MAXHEALTH = 100;
	public float health = 100;

	void Start () 
	{
		UpdateHealthbar ();
	}

	void Update () 
	{
		
	}
	void UpdateHealthbar()
	{
		if (!checkDead()) 
		{
			float healthPercent = health / 100;
			healthbar.transform.localScale = new Vector3 (Mathf.Lerp (0, 3, healthPercent), 1, 1);
			healthbar.GetComponent<Renderer>().material.color = Color.Lerp (Color.red, Color.green, healthPercent);
		}
	}
	public void subHealth(float damage)
	{
		health -= damage;
		UpdateHealthbar ();
		if (checkDead ())
						die ();
	}
	bool checkDead()
	{
		return health <= 0;
	}
	void die()
	{
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		Destroy (gameObject, 2);
		GetComponent<enemyLogic> ().enabled = false;
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<Animator> ().enabled = false;
		Destroy (healthbar);
	}
	void OnDestroy()
	{
		int floor = GetComponent<enemyLogic> ().myFloor;
		int min;
		if (floor < 3) 
		{
			min = 1;
		} else if (floor < 5) 
		{
			min = 2;
		} 
		else 
		{
			min = 3;
		}
		if (Random.Range (min, 4) == 3) 
		{
			Debug.Log("Missiles");
		}
	}
}
