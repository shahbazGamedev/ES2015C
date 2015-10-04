using UnityEngine;
using System.Collections;
using Pathfinding;

public class ClickToMove : MonoBehaviour {

    public float speed;
    public CharacterController controller;
    private Vector3 targetPosition;
	public Seeker seeker;
	private Path path;
	private float nextWaypointDistance = 3.0f;
	private int currentWaypoint = 0;
	
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            locatePosition();
			getNewPath();
        }

        if (path==null){
			return;
		}
		
		if(currentWaypoint >= path.vectorPath.Count) {
			return;
		}
		
		moveToPosition();
	}

    void locatePosition() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out hit, 1000)) {
            targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }
	
	void getNewPath(){
		//Debug.Log("getting new Path!");
		seeker.StartPath(transform.position, targetPosition, OnPathComplete);
	}
	
	void OnPathComplete(Path newPath){
		if (!newPath.error){
			path=newPath;
			currentWaypoint=0;
		}
	}

    void moveToPosition() {
		Quaternion newRotation= Quaternion.LookRotation(targetPosition-transform.position);

        newRotation.x = 0f;
        newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
		
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir = dir*speed*Time.deltaTime;
		controller.SimpleMove(dir);

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance){
			currentWaypoint++;
			return;
		}
    }
}
