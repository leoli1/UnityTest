using UnityEngine;
using System.Collections;

public class missileScript : MonoBehaviour {

	public GameObject rocketExplosion;
	public float damage;
	public float speed = 35;
	GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<Rigidbody>().AddForce (transform.rotation * Vector3.back * speed*4);
	}

	void Update () 
	{
		GetComponent<Rigidbody>().AddForce (transform.rotation*Vector3.back*100);
		GetComponent<Rigidbody>().AddForce (Vector3.down * 4);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player") 
		{
			playerHealth.pH.applyDamage(damage);
		}
		destroy ();
	}
	void destroy()
	{
		//play destroy animation
		GameObject RE = (GameObject)Instantiate (rocketExplosion, transform.position, Quaternion.identity);
		Destroy (RE, 1);

		Destroy (gameObject);
	}
}
