using UnityEngine;
using UnityEngine.UI; 

public class Mini : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

	private Vector3 mp = new Vector3 (0,0,0);

    private Camera itsMinimapCamera;
    private Camera itsMainCamera;

	RawImage image;
	Canvas canvas;

	public float y = 7.5f;
	public float x = 12.5f;
	
	GameObject cameraPoint;

	RectTransform mini;
	RectTransform N;
	RectTransform S;
	RectTransform E;
	RectTransform W;

	float direccioX;
	float direccioZ;

	public static class Utils
	{
		static Texture2D _whiteTexture;
		public static Texture2D WhiteTexture
		{
			get
			{
				if( _whiteTexture == null )
				{
					_whiteTexture = new Texture2D( 1, 1 );
					_whiteTexture.SetPixel( 0, 0, Color.white );
					_whiteTexture.Apply();
				}
				
				return _whiteTexture;
			}
		}
		
		public static void DrawScreenRect( Rect rect, Color color )
		{
			GUI.color = color;
			GUI.DrawTexture( rect, WhiteTexture );
			GUI.color = Color.white;
		}

		public static void DrawScreenRectBorder( Rect rect, float thickness, Color color )
		{
			// Top
			Utils.DrawScreenRect( new Rect( rect.xMin, rect.yMin, rect.width, thickness ), color );
			// Left
			Utils.DrawScreenRect( new Rect( rect.xMin, rect.yMin, thickness, rect.height ), color );
			// Right
			Utils.DrawScreenRect( new Rect( rect.xMax - thickness, rect.yMin, thickness, rect.height ), color);
			// Bottom
			Utils.DrawScreenRect( new Rect( rect.xMin, rect.yMax - thickness, rect.width, thickness ), color );
		}
	}




    void Start()
    {
		direccioZ = Mathf.Cos((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
		direccioX = Mathf.Sin((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
		itsMinimapCamera = GameObject.Find("MiniMap").GetComponent<Camera>();
        itsMainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraPoint = GameObject.Find ("CameraPoint");
		image = GameObject.Find("HUDMiniMapImage").GetComponent<RawImage>();
		canvas = GameObject.Find("CanvasHUD").GetComponent<Canvas>();
		Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height*canvas.scaleFactor);
		mp = new Vector2 (cameraPoint.transform.position.x / miniscale + origin.x, cameraPoint.transform.position.z / miniscale + origin.y);
		mini = GameObject.Find ("HUDMiniMap").GetComponent<RectTransform> ();
		N = GameObject.Find ("N").GetComponent<RectTransform> ();
		S = GameObject.Find ("S").GetComponent<RectTransform> ();
		E = GameObject.Find ("E").GetComponent<RectTransform> ();
		W = GameObject.Find ("W").GetComponent<RectTransform> ();
    }

	void OnGUI()
	{

		Utils.DrawScreenRectBorder( new Rect( mp.x, Screen.height-mp.y, 25, 15 ), 2, new Color( 0.8f, 0.8f, 0.95f ) );
	}

    void Update()
    {

		Vector2 difCamPos = new Vector2 (cameraPoint.transform.position.x, cameraPoint.transform.position.z) - new Vector2 (Terrain.activeTerrain.terrainData.heightmapWidth / 2, Terrain.activeTerrain.terrainData.heightmapHeight / 2);
		float modCamPos = difCamPos.magnitude;
		float angle = Mathf.Acos (difCamPos.y / modCamPos)*Mathf.Sign(difCamPos.x);
		//Debug.Log (angle*180/Mathf.PI);
		direccioZ = Mathf.Cos (((-Camera.main.transform.eulerAngles.y * Mathf.PI) / 180) + angle);//*Mathf.Sign(dif.x);
		direccioX = Mathf.Sin(((-Camera.main.transform.eulerAngles.y * Mathf.PI)/180) +angle);
		Vector2 center = new Vector2(image.transform.position.x,image.transform.position.y);
		//Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height*canvas.scaleFactor);
		//Debug.Log ("dif "+difCamPos);
		//Debug.Log ("mod "+modCamPos);
		//Debug.Log ("X" + direccioX);
		//Debug.Log ("Z" + direccioZ);
		mp = new Vector2 ((modCamPos*direccioX / miniscale + center.x)-x, (modCamPos*direccioZ / miniscale + center.y)+y);
		//mp = new Vector2 ((cameraPoint.transform.position.x / miniscale + origin.x)-x, (cameraPoint.transform.position.z / miniscale + origin.y)+y);
		float camY = Camera.main.transform.eulerAngles.y;
		mini.rotation = Quaternion.Euler(0,0,camY);
		N.rotation = Quaternion.Euler(0,0,0);
		S.rotation = Quaternion.Euler(0,0,0);
		E.rotation = Quaternion.Euler(0,0,0);
		W.rotation = Quaternion.Euler(0,0,0);
	}

	public void click(){
		//Debug.Log ("X" + direccioX);
		//Debug.Log ("Z" + direccioZ);
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height * canvas.scaleFactor);
		//Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		Vector2 center = new Vector2(image.transform.position.x,image.transform.position.y);
		Vector2 mouse = Input.mousePosition;
		Vector2 dif = mouse - center;
		//Debug.Log (dif);
		float modul = dif.magnitude;
		float angle = Mathf.Acos (dif.y / modul)*Mathf.Sign(dif.x);
		//Debug.Log (angle*180/Mathf.PI);
		direccioZ = Mathf.Cos (((Camera.main.transform.eulerAngles.y) * Mathf.PI) / 180 + angle);//*Mathf.Sign(dif.x);
		direccioX = Mathf.Sin(((Camera.main.transform.eulerAngles.y) * Mathf.PI)/180+angle);//*Mathf.Sign(dif.x);
		Vector3 posicio = new Vector3 (modul*miniscale*direccioX+Terrain.activeTerrain.terrainData.heightmapWidth/2, 0, modul*miniscale*direccioZ+Terrain.activeTerrain.terrainData.heightmapHeight/2);
		float yPoint = itsMainCamera.transform.position.y;
		Vector3 relacio = itsMainCamera.transform.position - cameraPoint.transform.position;
		cameraPoint.transform.position = posicio;
		itsMainCamera.transform.position = posicio + relacio;

	} 
 }