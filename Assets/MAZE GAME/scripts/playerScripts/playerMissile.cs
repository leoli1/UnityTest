using UnityEngine;
using System.Collections;

public class playerMissile : MonoBehaviour {

	public float damageMultiplier = 0.4f;
	void Start () 
	{
		Destroy (gameObject, 5);
		GetComponent<Rigidbody>().AddForce (transform.rotation * Vector3.forward * 250+(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)))*2);
	}

	void OnCollisionEnter(Collision col)
	{
		string tag = col.gameObject.tag;
		if (tag == "Enemy") 
		{
			float damage = GetComponent<Rigidbody>().velocity.magnitude*damageMultiplier;
			col.gameObject.GetComponent<enemyHealth>().subHealth(damage);
		}
	}
}
