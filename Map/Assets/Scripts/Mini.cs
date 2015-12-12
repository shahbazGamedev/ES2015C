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

		itsMinimapCamera = GameObject.Find("MiniMap").GetComponent<Camera>();
        itsMainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraPoint = GameObject.Find ("CameraPoint");
		image = GameObject.Find("RawImage").GetComponent<RawImage>();
		canvas = GameObject.Find("HUD").GetComponent<Canvas>();
		Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height*canvas.scaleFactor);
		mp = new Vector2 (cameraPoint.transform.position.x / miniscale + origin.x, cameraPoint.transform.position.z / miniscale + origin.y);
        
    }

	void OnGUI()
	{

		Utils.DrawScreenRectBorder( new Rect( mp.x, Screen.height-mp.y, 25, 15 ), 2, new Color( 0.8f, 0.8f, 0.95f ) );
	}

    void Update()
    {
		if ( Input.GetMouseButtonDown(0) )
		{
			/*
			Ray2D ray;
			RaycastHit2D hit;
			GameObject objectAtMouse;
			//Vector2 r2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			//ray = r2;
			Renderer rend = hit.transform.GetComponent<Renderer>();

			MeshCollider meshCollider = hit.collider as MeshCollider;
			if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
				return;

			Vector2 pixelUV = hit.textureCoord;


			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Debug.Log(Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			{
				Vector2 hitPos = Camera.main.WorldToScreenPoint(hit.point);
				Debug.Log(hitPos);
				Debug.Log(GUIUtility.ScreenToGUIPoint(hitPos));
			}
			*/
		}

		/*
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...

            RaycastHit hit;
            //Debug.Log("Step1");
            Ray ray = itsMinimapCamera.ScreenPointToRay(Input.mousePosition);
            //Debug.Log("Step2");
            if (Physics.Raycast(ray, out hit)){

                float yPoint = itsMainCamera.transform.position.y;
                Vector3 auxiliar = hit.point;
                itsMainCamera.transform.position = auxiliar;
				auxiliar = itsMainCamera.transform.position;
				auxiliar.y = 0;
				cameraPoint.transform.position = auxiliar;
				auxiliar.y = yPoint;
				auxiliar.z = auxiliar.z - 90f*Mathf.Cos((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
				auxiliar.x = auxiliar.x - 90f*Mathf.Sin((Camera.main.transform.eulerAngles.y * Mathf.PI)/180);
				itsMainCamera.transform.position = auxiliar;

                //Debug.Log("Step3");
                //Debug.Log(hit.point);
            }  
       }
		//Vector3 aux = cameraPoint.transform.position;
		//aux.z = aux.z + 120f;
		//aux.x = aux.x - 50f;


		Vector3 p;
		p = itsMinimapCamera.WorldToScreenPoint(cameraPoint.transform.position);
		p.y += y;
		p.x -= x;
		mp = p;
		*/
		
		Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height*canvas.scaleFactor);
		mp = new Vector2 ((cameraPoint.transform.position.x / miniscale + origin.x)-x, (cameraPoint.transform.position.z / miniscale + origin.y)+y);

	}

	public void click(){
		float miniscale = Terrain.activeTerrain.terrainData.heightmapHeight / (image.GetPixelAdjustedRect ().height * canvas.scaleFactor);
		Vector2 origin = new Vector2 (image.transform.position.x - (image.GetPixelAdjustedRect ().width * canvas.scaleFactor) / 2, image.transform.position.y - (image.GetPixelAdjustedRect ().height * canvas.scaleFactor) / 2);
		Vector2 mouse = Input.mousePosition;
		Vector2 dif = mouse - origin;
		Debug.Log ("DIFERENCIA " + dif);
		Vector3 posicio = new Vector3 (dif.x*miniscale, 0, dif.y*miniscale);
		float yPoint = itsMainCamera.transform.position.y;
		Vector3 relacio = itsMainCamera.transform.position - cameraPoint.transform.position;
		cameraPoint.transform.position = posicio;
		itsMainCamera.transform.position = posicio + relacio;

		mp = new Vector2 ((cameraPoint.transform.position.x / miniscale + origin.x)-x, (cameraPoint.transform.position.z / miniscale + origin.y)+y);
	}

 }