using UnityEngine;
using System.Collections;

public class zoomCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float minFov = 15f;
	float maxFov = 70f;
	float speed = 5f;
	float fov = 0f;

	// Update is called once per frame
	void Update () {

		fov = Camera.main.fieldOfView;
		fov += Input.GetAxis ("Mouse ScrollWheel") * speed;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

	}

	public void OnGUI()
	{
		if (Event.current.type == EventType.ScrollWheel){
			// do stuff with  Event.current.delta
			fov = Camera.main.fieldOfView;
			fov += Event.current.delta.y;// / Time.deltaTime;
			fov = Mathf.Clamp (fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		}
	}

}
