using UnityEngine;
using System.Collections;

public class zoomCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float minFov = 10f;
	float maxFov = 70f;
	float speed = 10f;
	float fov = 0f;

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Plus) || Input.GetKey (KeyCode.KeypadPlus)) {
			fov = Camera.main.fieldOfView;
			fov -= speed*Time.deltaTime;
			fov = Mathf.Clamp (fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		}

		if (Input.GetKey (KeyCode.Minus) || Input.GetKey (KeyCode.KeypadMinus)) {
			fov = Camera.main.fieldOfView;
			fov += speed*Time.deltaTime;
			fov = Mathf.Clamp (fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		}

		fov = Camera.main.fieldOfView;
		fov += -Input.GetAxis("Mouse ScrollWheel") * speed;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

	}

	public void OnGUI()
	{
		if (Event.current.type == EventType.ScrollWheel){
			// do stuff with  Event.current.delta
			fov = Camera.main.fieldOfView;
			fov += Event.current.delta.y;
			fov = Mathf.Clamp (fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;
			Debug.Log (Event.current.delta);
		}
	}


}
