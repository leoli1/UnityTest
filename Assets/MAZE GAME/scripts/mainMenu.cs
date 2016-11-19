using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {
	
	void Start () 
	{
	
	}

	void Update () 
	{
		
	}

	public void loadWorld(int w)
	{
		Application.LoadLevel("world"+w.ToString());
	}
	public void loadControllsScene()
	{
		Application.LoadLevel ("Controlls");
	}
}
