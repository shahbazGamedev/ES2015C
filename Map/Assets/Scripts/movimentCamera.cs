﻿using UnityEngine;
using System.Collections;

public class movimentCamera : MonoBehaviour {

	void Awake(){
		CameraPoint = new GameObject ("CameraPoint");
	}

	void Start () {
		terrainHeight = Terrain.activeTerrain.terrainData.heightmapHeight;
		terrainWidth = Terrain.activeTerrain.terrainData.heightmapWidth;
		wMargin = Screen.width * marginPercent;
		hMargin = Screen.height * marginPercent;
		//Debug.Log(Screen.width.ToString("F2"));
		//Debug.Log (wMargin.ToString("F2"));
		recup = new Rect (0, 0, Screen.width, hMargin);
		recdown = new Rect (0, Screen.height-hMargin, Screen.width, hMargin);
		recleft = new Rect (0, 0, wMargin, Screen.height);
		recright = new Rect (Screen.width-wMargin, 0, wMargin, Screen.height);
		//cube = GameObject.Find ("Cube");
		//Instantiate (CameraPoint);
		//CameraPoint.Instantiate ();
		//CameraPoint = GameObject.Find("CameraPoint");
		CameraPoint.transform.position = new Vector3(192,0,220);
		Vector3 aux = CameraPoint.transform.position;
		aux.y = transform.position.y;
		aux.z = aux.z - 90;
		Camera.main.transform.position = aux;
		Camera.main.transform.LookAt (CameraPoint.transform.position);
	}

	void OnGUI() {
		if (marginVisible) {
			if (marginVisibleDown)
				drawRect (recdown);
			if (marginVisibleUp)
				drawRect (recup);
			if (marginVisibleLeft)
				drawRect (recleft);
			if (marginVisibleRight)
				drawRect (recright);
		}

	}
	
	void drawRect(Rect rect){
		Texture2D texture = new Texture2D(1, 1);
		Color color = Color.cyan;
		color.a = 0.5f;
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(rect, GUIContent.none);
		
	}

	public bool margin = false;
	public bool marginVisible = false;
	bool marginVisibleUp = false;
	bool marginVisibleDown = false;
	bool marginVisibleLeft = false;
	bool marginVisibleRight = false;
	public float marginPercent = 0.01f;
	Rect recdown;
	Rect recup;
	Rect recleft;
	Rect recright;
	float wMargin = 60;
	float hMargin = 60;
	int terrainWidth = 500;
	int terrainHeight = 500;
	public float CamSpeed = 8.00f;
	float percent = 0f;
	int MarginSize = 60;

	GameObject CameraPoint;
	//GameObject cube;

	bool isRotating = false;
	private Vector3 mouseOrigin;
	Vector3 auxiliar;

	float direccioZ;
	float direccioX;
	float direccioZ2;
	float direccioX2;

	// Update is called once per frame
	void Update () {
		direccioZ = Mathf.Cos((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
		direccioX = Mathf.Sin((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
		direccioZ2 = Mathf.Cos(((Camera.main.transform.eulerAngles.y+90) * Mathf.PI)/180);
		direccioX2 = Mathf.Sin(((Camera.main.transform.eulerAngles.y+90) * Mathf.PI)/180);

		float cspeed = CamSpeed*(Camera.main.fieldOfView+30)/100;
		// Mouse movement
		wMargin = Screen.width * marginPercent;
		hMargin = Screen.height * marginPercent;
		recup = new Rect (0, 0, Screen.width, hMargin);
		recdown = new Rect (0, Screen.height-hMargin, Screen.width, hMargin);
		recleft = new Rect (0, 0, wMargin, Screen.height);
		recright = new Rect (Screen.width-wMargin, 0, wMargin, Screen.height);

		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
			
			auxiliar = Camera.main.transform.position;

			auxiliar.z = auxiliar.z + direccioZ*90f*(Camera.main.fieldOfView+40+45-Camera.main.transform.eulerAngles.x*1.2f)/100;
			auxiliar.x = auxiliar.x + direccioX*90f*(Camera.main.fieldOfView+40+45-Camera.main.transform.eulerAngles.x*1.2f)/100;

			auxiliar.y = 0;

			//Debug.Log(auxiliar);

			//GameObject obj = GameObject.Find("Cube");
			//obj.transform.position = auxiliar;
		}

		if (!Input.GetMouseButton(2)) isRotating=false;
		if (isRotating){

			float turnSpeed = 4.0f;

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			float mov = -pos.y * turnSpeed;

			if (Camera.main.transform.eulerAngles.x+(mov) < 8 || Camera.main.transform.eulerAngles.x+mov > 90){
				mov = 0;
			}

			transform.RotateAround(CameraPoint.transform.position, transform.right, mov);
			transform.RotateAround(CameraPoint.transform.position, Vector3.up, -pos.x * turnSpeed);

			Vector3 aux;
			aux = Camera.main.transform.position;
			aux.y = 1.0f;
			if (Camera.main.transform.position.y < 1) Camera.main.transform.position = aux;


		}

		Vector3 posi = CameraPoint.transform.position;
		if (margin) {

			if (recup.Contains (Input.mousePosition)) {
			
				marginVisibleDown = true;
				percent = (hMargin - Input.mousePosition.y) / hMargin;
				transform.Translate (0, 0, -percent * cspeed, Space.World);
			} else
				marginVisibleDown = false;

		
			if (recdown.Contains (Input.mousePosition)) {
				percent = 1 - (Screen.height - Input.mousePosition.y) / hMargin;
				transform.Translate (0, 0, percent * cspeed, Space.World);
				marginVisibleUp = true;
			} else
				marginVisibleUp = false;
		
			if (recleft.Contains (Input.mousePosition)) {
				percent = (wMargin - Input.mousePosition.x) / wMargin;
				transform.Translate (-percent * cspeed, 0, 0, Space.World);
				marginVisibleLeft = true;
			} else
				marginVisibleLeft = false;
		
			if (recright.Contains (Input.mousePosition)) {
				percent = 1 - (Screen.width - Input.mousePosition.x) / wMargin;
				transform.Translate (percent * cspeed, 0, 0, Space.World);
				marginVisibleRight = true;
			} else
				marginVisibleRight = false;
		} else {

			if (recup.Contains (Input.mousePosition)) {

				Vector3 trans = new Vector3(-1 * cspeed*direccioX, 0, -1 * cspeed*direccioZ);
				if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
					transform.Translate (trans, Space.World);
					CameraPoint.transform.Translate(trans, Space.World);
				}
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			
			if (recdown.Contains (Input.mousePosition)) {
				Vector3 trans = new Vector3(1 * cspeed*direccioX, 0, 1 * cspeed*direccioZ);
				if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				}
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			if (recleft.Contains (Input.mousePosition)) {
				Vector3 trans = new Vector3(-1 * cspeed*direccioX2, 0, -1 * cspeed*direccioZ2);
				if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				}
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			if (recright.Contains (Input.mousePosition)) {
				Vector3 trans = new Vector3(1 * cspeed*direccioX2, 0, 1 * cspeed*direccioZ2);
				if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				}
				//cube.transform.position = CameraPoint.transform.position;
			}
		}
		// Arrows movement
		if (Input.GetKey (KeyCode.DownArrow)) {
			Vector3 trans = new Vector3(-CamSpeed*5 * Time.deltaTime*direccioX, 0, -CamSpeed*5 * Time.deltaTime*direccioZ);
			
			if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
			}
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			Vector3 trans = new Vector3(CamSpeed*5 * Time.deltaTime*direccioX, 0, CamSpeed*5 * Time.deltaTime*direccioZ);
			
			if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
			}
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 trans = new Vector3(-CamSpeed*5 * Time.deltaTime*direccioX2, 0, -CamSpeed*5 * Time.deltaTime*direccioZ2);
			if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
			}
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 trans = new Vector3(CamSpeed*5 * Time.deltaTime*direccioX2, 0, CamSpeed*5 * Time.deltaTime*direccioZ2);
			
			if (posi.x+trans.x > 0 && posi.x+trans.x < terrainWidth && posi.z+trans.z > 0 && posi.z+trans.z < terrainHeight){
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
			}
		}
	}
}