using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour 
{
	public static playerHealth pH;
	public SkinnedMeshRenderer meshRenderer;
	public GameObject deadParticleSystem;
	public const float MAXHEALTH = 100;
	public float health;
	public bool isDead = false;
	float deadTimer = 5;
	void Start () 
	{
		health = MAXHEALTH;
		pH = this;
	}
	public void AddHealth(float h)
	{
		health += h;
		health = Mathf.Clamp (health, 0, 100);
	}
	void Update () 
	{
		if (isDead) 
		{
			deadTimer -= Time.deltaTime;
			if (deadTimer<=0)
				Application.LoadLevel(Application.loadedLevelName);
		}
		if (transform.position.y < -5) 
		{
			applyDamage(health);
		}
	}
	public void applyDamage(float damage)
	{
		health -= damage;
		if (checkDead()) 
		{
			dead ();

		}
	}
	void dead()
	{
		if (!isDead) 
		{
			Instantiate(deadParticleSystem, transform.position+Vector3.up/2, Quaternion.identity);
			meshRenderer.enabled = false;
		}
		isDead = true;

	}
	bool checkDead()
	{
		return health <= 0;
	}
	void OnGUI()
	{
		if (!isDead) 
		{
			GUI.Box (new Rect (5, 5, 120, 25), "Health: " + health + "%");
		} 
		else 
		{
			GUI.Box(new Rect((Screen.width-120)/2, (Screen.height-25)/2, 120, 25), "GAME OVER");
		}
	}
}
