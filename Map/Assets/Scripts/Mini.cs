using UnityEngine;
 
public class Mini : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

	private Vector3 mp = new Vector3 (0,0,0);

    private Camera itsMinimapCamera;
    private Camera itsMainCamera;

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
        
    }

	void OnGUI()
	{

		Utils.DrawScreenRectBorder( new Rect( mp.x, Screen.height-mp.y, 25, 15 ), 2, new Color( 0.8f, 0.8f, 0.95f ) );
	}

    void Update()
    {
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

    }

 }