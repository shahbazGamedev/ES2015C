using UnityEngine;
using System.Collections;

public class movimentCamera : MonoBehaviour {

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
		CameraPoint = new GameObject ("CameraPoint");
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

		if(Input.GetMouseButtonDown(1))
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

		if (!Input.GetMouseButton(1)) isRotating=false;
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


		if (margin) {

			if (recup.Contains (Input.mousePosition) && transform.position.z > -60) {
			
				marginVisibleDown = true;
				percent = (hMargin - Input.mousePosition.y) / hMargin;
				transform.Translate (0, 0, -percent * cspeed, Space.World);
			} else
				marginVisibleDown = false;

		
			if (recdown.Contains (Input.mousePosition) && transform.position.z < terrainHeight * 0.8) {
				percent = 1 - (Screen.height - Input.mousePosition.y) / hMargin;
				transform.Translate (0, 0, percent * cspeed, Space.World);
				marginVisibleUp = true;
			} else
				marginVisibleUp = false;
		
			if (recleft.Contains (Input.mousePosition) && transform.position.x > 0) {
				percent = (wMargin - Input.mousePosition.x) / wMargin;
				transform.Translate (-percent * cspeed, 0, 0, Space.World);
				marginVisibleLeft = true;
			} else
				marginVisibleLeft = false;
		
			if (recright.Contains (Input.mousePosition) && transform.position.x < terrainWidth * 0.95) {
				percent = 1 - (Screen.width - Input.mousePosition.x) / wMargin;
				transform.Translate (percent * cspeed, 0, 0, Space.World);
				marginVisibleRight = true;
			} else
				marginVisibleRight = false;
		} else {

			if (recup.Contains (Input.mousePosition) && transform.position.z > -60) {
				Vector3 trans = new Vector3(-1 * cspeed*direccioZ*direccioX, 0, -1 * cspeed*direccioZ);
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			
			if (recdown.Contains (Input.mousePosition) && transform.position.z < terrainHeight* 0.8) {
				Vector3 trans = new Vector3(1 * cspeed*direccioX, 0, 1 * cspeed*direccioZ);
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			if (recleft.Contains (Input.mousePosition) && transform.position.x > 0) {
				Vector3 trans = new Vector3(-1 * cspeed*direccioX2, 0, -1 * cspeed*direccioZ2);
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				//cube.transform.position = CameraPoint.transform.position;
			}
			
			if (recright.Contains (Input.mousePosition) && transform.position.x < terrainWidth * 0.95) {
				Vector3 trans = new Vector3(1 * cspeed*direccioX2, 0, 1 * cspeed*direccioZ2);
				transform.Translate (trans, Space.World);
				CameraPoint.transform.Translate(trans, Space.World);
				//cube.transform.position = CameraPoint.transform.position;
			}
		}
		// Arrows movement
		if (Input.GetKey (KeyCode.DownArrow) && transform.position.z > -60) {
			Vector3 trans = new Vector3(-CamSpeed*5 * Time.deltaTime*direccioX, 0, -CamSpeed*5 * Time.deltaTime*direccioZ);
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
		}
		if (Input.GetKey (KeyCode.UpArrow) && transform.position.z < terrainHeight*0.8) {
			Vector3 trans = new Vector3(CamSpeed*5 * Time.deltaTime*direccioX, 0, CamSpeed*5 * Time.deltaTime*direccioZ);
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.position.x > 0) {
			Vector3 trans = new Vector3(-CamSpeed*5 * Time.deltaTime*direccioX2, 0, -CamSpeed*5 * Time.deltaTime*direccioZ2);
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
		}
		if (Input.GetKey (KeyCode.RightArrow) && transform.position.x < terrainWidth*0.95) {
			Vector3 trans = new Vector3(CamSpeed*5 * Time.deltaTime*direccioX2, 0, CamSpeed*5 * Time.deltaTime*direccioZ2);
			CameraPoint.transform.Translate(trans, Space.World);
			//cube.transform.position = CameraPoint.transform.position;
			transform.Translate (trans, Space.World);
		}
	}
}