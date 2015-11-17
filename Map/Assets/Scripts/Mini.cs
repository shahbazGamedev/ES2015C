using UnityEngine;
 
public class Mini : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    private Camera itsMinimapCamera;
    private Camera itsMainCamera;

    
    void Start()
    {

        itsMinimapCamera = this.GetComponent<Camera>();
        itsMainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            RaycastHit hit;
            Debug.Log("Step1");
            Ray ray = itsMinimapCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Step2");
            if (Physics.Raycast(ray, out hit)){

                float yPoint = itsMainCamera.transform.position.y;
                Vector3 auxiliar = hit.point;
                auxiliar.y = yPoint;
                itsMainCamera.transform.position = auxiliar;
                Debug.Log("Step3");
                Debug.Log(hit.point);
            }  
       }
    }

 }