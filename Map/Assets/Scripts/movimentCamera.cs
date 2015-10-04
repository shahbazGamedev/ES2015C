using UnityEngine;
using System.Collections;

public class movimentCamera : MonoBehaviour {

	void Start () {
	}
	
	//int terrainHeight = Terrain.activeTerrain.terrainData.heightmapHeight;
	//int terrainWidth = Terrain.activeTerrain.terrainData.heightmapWidth;
	int terrainWidth = 500;
	int terrainHeight = 500;
	float CamSpeed = 1.00f;
	float percent = 0f;
	int MarginSize = 50;
	
	// Update is called once per frame
	void Update () {

		// Mouse movement
		Rect recdown = new Rect (0, 0, Screen.width, MarginSize);
		Rect recup = new Rect (0, Screen.height-MarginSize, Screen.width, MarginSize);
		Rect recleft = new Rect (0, 0, MarginSize, Screen.height);
		Rect recright = new Rect (Screen.width-MarginSize, 0, MarginSize, Screen.height);
		
		if (recdown.Contains (Input.mousePosition) && transform.position.z > -60) {
			

			percent = (MarginSize - Input.mousePosition.y)/MarginSize;
			transform.Translate (0, 0, -percent*CamSpeed, Space.World);
		}
		
		if (recup.Contains (Input.mousePosition) && transform.position.z < terrainHeight*0.85) {
			percent = 1-(Screen.height - Input.mousePosition.y)/MarginSize;
			transform.Translate (0, 0, percent*CamSpeed, Space.World);

		}
		
		if (recleft.Contains (Input.mousePosition) && transform.position.x > 0) {
			percent = (MarginSize - Input.mousePosition.x)/MarginSize;
			transform.Translate (-percent*CamSpeed, 0, 0, Space.World);
		}

		if (recright.Contains(Input.mousePosition) && transform.position.x < terrainWidth*0.85){
			percent = 1-(Screen.width - Input.mousePosition.x)/MarginSize;
			transform.Translate(percent*CamSpeed, 0, 0, Space.World);
		}

		// Arrows movement
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (0, 0, -CamSpeed*5 * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.UpArrow) && transform.position.z < Terrain.activeTerrain.terrainData.heightmapHeight*0.70) {
			transform.Translate (0, 0, CamSpeed*5 * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.position.x > 0) {
			transform.Translate (-CamSpeed*5 * Time.deltaTime, 0, 0, Space.World);
		}
		if (Input.GetKey (KeyCode.RightArrow) && transform.position.x < Terrain.activeTerrain.terrainData.heightmapHeight*0.85) {
			transform.Translate (CamSpeed*5 * Time.deltaTime, 0, 0, Space.World);
		}
	}
}
