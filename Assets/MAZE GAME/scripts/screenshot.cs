using UnityEngine;
using System.Collections;
using System.IO;

public class screenshot : MonoBehaviour {

	int a = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			Application.CaptureScreenshot("screenShot"+a.ToString()+".png");
			a++;
			Debug.Log ("a");
		}
	}
}
