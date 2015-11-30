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
	
	// Update is called once per frame
	void Update () {

		float cspeed = CamSpeed*(Camera.main.fieldOfView+30)/100;
		// Mouse movement
		wMargin = Screen.width * marginPercent;
		hMargin = Screen.height * marginPercent;
		recup = new Rect (0, 0, Screen.width, hMargin);
		recdown = new Rect (0, Screen.height-hMargin, Screen.width, hMargin);
		recleft = new Rect (0, 0, wMargin, Screen.height);
		recright = new Rect (Screen.width-wMargin, 0, wMargin, Screen.height);


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
				transform.Translate (0, 0, -1 * cspeed, Space.World);
			}
			
			
			if (recdown.Contains (Input.mousePosition) && transform.position.z < terrainHeight* 0.8) {
				transform.Translate (0, 0, 1 * cspeed, Space.World);
			}
			
			if (recleft.Contains (Input.mousePosition) && transform.position.x > 0) {
				transform.Translate (-1 * cspeed, 0, 0, Space.World);
			}
			
			if (recright.Contains (Input.mousePosition) && transform.position.x < terrainWidth * 0.95) {
				transform.Translate (1 * cspeed, 0, 0, Space.World);
			}
		}
		// Arrows movement
		if (Input.GetKey (KeyCode.DownArrow) && transform.position.z > -60) {
			transform.Translate (0, 0, -CamSpeed*5 * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.UpArrow) && transform.position.z < terrainHeight*0.8) {
			transform.Translate (0, 0, CamSpeed*5 * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.position.x > 0) {
			transform.Translate (-CamSpeed*5 * Time.deltaTime, 0, 0, Space.World);
		}
		if (Input.GetKey (KeyCode.RightArrow) && transform.position.x < terrainWidth*0.95) {
			transform.Translate (CamSpeed*5 * Time.deltaTime, 0, 0, Space.World);
		}
	}
}