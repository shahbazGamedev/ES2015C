using UnityEngine;
using System.Collections;

public class movimentCamera : MonoBehaviour {

	void Start () {
		
	}
	
	float CamSpeed = 1.00f;
	float percent = 0f;
	int MarginSize = 50;
	
	// Update is called once per frame
	void Update () {
		Rect recdown = new Rect (0, 0, Screen.width, MarginSize);
		Rect recup = new Rect (0, Screen.height-MarginSize, Screen.width, MarginSize);
		Rect recleft = new Rect (0, 0, MarginSize, Screen.height);
		Rect recright = new Rect (Screen.width-MarginSize, 0, MarginSize, Screen.height);
		
		if (recdown.Contains (Input.mousePosition)) {
			percent = (MarginSize - Input.mousePosition.y)/MarginSize;
			transform.Translate (0, 0, -percent*CamSpeed, Space.World);
		}
		
		if (recup.Contains (Input.mousePosition)) {
			percent = 1-(Screen.height - Input.mousePosition.y)/MarginSize;
			transform.Translate (0, 0, percent*CamSpeed, Space.World);
		}
		
		if (recleft.Contains (Input.mousePosition)) {
			percent = (MarginSize - Input.mousePosition.x)/MarginSize;
			transform.Translate (-percent*CamSpeed, 0, 0, Space.World);
		}

		if (recright.Contains(Input.mousePosition)){
			percent = 1-(Screen.width - Input.mousePosition.x)/MarginSize;
			transform.Translate(percent*CamSpeed, 0, 0, Space.World);
		}
	}
}
