using UnityEngine;
 
public class Mini : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    private Camera itsMinimapCamera;
    private Camera itsMainCamera;

    private float main_zoom;
    private Vector3 act_pos;
    private float aspect;
 
    
    void Start()
    {
    

        
        itsMinimapCamera = this.GetComponent<Camera>();
        

        itsMainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        initializeCameraPars();
        /*
        // moves camera to show the whole map
        if (Terrain.activeTerrain)
        {
            float diagonal = Mathf.Sqrt(Mathf.Pow(Terrain.activeTerrain.terrainData.size.x, 2) + Mathf.Pow(Terrain.activeTerrain.terrainData.size.y, 2));
            _camera.transform.position = new Vector3(Terrain.activeTerrain.terrainData.size.x * 0.5f, Terrain.activeTerrain.terrainData.size.x * 0.6f,Terrain.activeTerrain.terrainData.size.z * 0.5f);
            _camera.transform.rotation = Quaternion.Euler(90f, 135f,0); 
            _camera.orthographicSize = diagonal * 0.95f; // a hack
            _camera.farClipPlane = Terrain.activeTerrain.terrainData.size.x * 1.5f;
            _camera.clearFlags = CameraClearFlags.Depth;
            instantiateMask();
        }

        createMarker();

        rt = new RenderTexture(Screen.width, Screen.height, 3);
        _camera.targetTexture = rt;*/
    }

    private void initializeCameraPars()
    {
        main_zoom = itsMainCamera.orthographicSize;
        act_pos = itsMainCamera.transform.position;
        aspect = itsMainCamera.aspect;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            RaycastHit hit;
            Debug.Log("Step1");
            Ray ray = itsMinimapCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Step2");
            if (Physics.Raycast(ray, out hit)){
                /*
                 Vector3 temp = transform.position; // copy to an auxiliary variable...
                 temp.y = 7.0f; // modify the component you want in the variable...
                 transform.position = temp; // and save the modified value 
                 */
                float yPoint = itsMainCamera.transform.position.y;
                Vector3 auxiliar = hit.point;
                auxiliar.y = yPoint;
                itsMainCamera.transform.position = auxiliar;
                /*
                itsMainCamera.transform.position = hit.point;
                itsMainCamera.transform.position.x = hit.point.x;
                itsMainCamera.transform.position.y = yPoint;
                itsMainCamera.transform.position.z = hit.point.z;*/

                
                //itsMainCamera.transform.position = hit.point ;
                Debug.Log("Step3");
                // hit.point contains the point where the ray hits the
                // object named "MinimapBackground"
                Debug.Log(hit.point);
            }  
            //itsMainCamera.transform.position = Vector3.Lerp(itsMainCamera.transform.position, hit.point+, 0.1f);
       }
       
    }
 }