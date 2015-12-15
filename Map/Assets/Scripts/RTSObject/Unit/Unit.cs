using Pathfinding;
using UnityEngine;

public class Unit : RTSObject
{
	private CharacterController characterController;	// Referencia al component CharacterController.
	private Seeker seeker;					// Referencia al component Seeker.
	private Vector3 targetPosition;         // Indica el vector3 del objectiu
	private GameObject targetObject;        // Indica el objecte del objectiu

	private Quaternion aimRotation;
	private Path path;
	private float nextWaypointDistance = 3.0f;
	private int currentWaypoint = 0;
	public float visibility = 20f;

	protected AudioClip idleSound;
	protected AudioClip doSound;
	protected AudioClip walkingSound;
	protected AudioClip runningSound;

    protected bool moving = false;			// Indica si esta movent-se 
	protected bool running = false;			// Indica si esta corrent


	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		seeker = gameObject.AddComponent<Seeker> ();
		anim = gameObject.GetComponent<Animator>();
		rigbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		ent.IsUnit = true;
		ent.Range = visibility;

		// Calculem la dimensio del CharacterController
		FittedCharacterCollider();
		gameObject.layer = 11;
		gameObject.tag = "mility";
		// Asignem els components extres per al funcionament de la IA per a unitats
		//RDV = gameObject.AddComponent<SphereCollider> ();
		//RDV.radius = characterController.radius * 10;
		//RDA = gameObject.AddComponent<BoxCollider> ();
		baseMoveSpeed = 5;
	}

	protected override void Update ()
	{
		base.Update ();
        

        // If the unit is currently moving, call the function to update
        // the position in the path the unit is following
        if (path != null && currentWaypoint < path.vectorPath.Count)
        {
			moveToPosition ();
		}
        else if (aiming)
		{
			
		}
	}

	protected override void OnGUI ()
	{
		base.OnGUI ();
	}

	protected virtual void OnMouseDown() {
		if (owner && owner.human && !moving && !aiming && !attacking && idleSound && !audio.isPlaying)
		{
			audio.PlayOneShot (idleSound);
		}
	}

	/*** Metodes publics ***/

	/// <summary>
	/// Tells the unit to move to the given position, by generating and
	/// following a route to the desired position.
	/// </summary>
	/// <param name="target">The position we want the unit to move to.</param>
	protected override void SetNewPath (Vector3 target, bool isRunning)
	{
		// We're starting movement, so start the walking animation
		if (isRunning){
			running = true;
		} else {
			moving = true;
		}

		targetPosition = target;
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

    public void GoTo(Vector3 target, bool isRunning)
    {
        // We're starting movement, so start the walking animation
		if (isRunning){
			running = true;
		} else {
			moving = true;
		}

        targetPosition = target;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    /// <summary>
    /// Tell the unit to cancel the movement path to the given position.
    /// </summary>
    protected override void CancelPath()
    {
        moving = false;
		running=false;
        path = null;
    }

    /// <summary>
    /// Tell the unit to cancel the movement path to the given position.
    /// </summary>
    protected override bool HasPath()
    {
        return moving || running;
    }

    public GameObject FindClosest (string tag)
	{


        var nearestDistanceSqr = Mathf.Infinity;
        var taggedGameObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObj = null;
        // loop through each tagged object, remembering nearest one found
        foreach (GameObject obj in taggedGameObjects)
        {
            var objectPos = obj.transform.position;
            var distanceSqr = (objectPos - transform.position).sqrMagnitude;
            if (distanceSqr < nearestDistanceSqr) {
                nearestObj = obj;
                nearestDistanceSqr = distanceSqr;
            }
        }
        return nearestObj;
    }

	public void makeDoSound(){
		if (owner && owner.human && doSound && !audio.isPlaying) {
			audio.PlayOneShot (doSound);
		}
	}


	/*** Metodes interns accessibles per les subclasses ***/

	protected override void Animating ()
	{
		base.Animating ();
		anim.SetBool ("IsWalking", moving);
		if(currentlySelected && moving && walkingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (walkingSound);
		}
		anim.SetBool ("IsRunning", running);
		if(currentlySelected && running && runningSound && !audio.isPlaying)
		{
			audio.PlayOneShot (runningSound);
		}
	}

	protected virtual void chargeSounds(string objectName)
	{
		base.chargeSounds (objectName);
		idleSound = Resources.Load ("Sounds/" + objectName + "_Idle") as AudioClip;
		doSound = Resources.Load ("Sounds/" + objectName + "_Do") as AudioClip;
		walkingSound = Resources.Load ("Sounds/" + objectName + "_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/" + objectName + "_Run") as AudioClip;
	}
	
	// Metode per disparar
	protected override void UseWeapon()
	{
		base.UseWeapon();
	}
	
	// Metode per apuntar
	protected override void AimAtTarget()
	{
		base.AimAtTarget();
		aimRotation = Quaternion.LookRotation(target.transform.position - transform.position);
	}

	/*** Metodes privats ***/

	private void OnPathComplete (Path newPath)
	{
        // We need to be careful and check the 'moving' variable here, since
        // if we started calculating a path, and cancelled it quickly, we will still receive
        // the OnPathComplete() event even tough the movement was cancelled.
        // Note that the case where we start a new path, cancel it, and start a new one
        // quickly works correctly, since in this case the Seeker will cancel the first path
		if ((moving || running) && !newPath.error)
        {
			path = newPath;
			currentWaypoint = 0;
		}
    }


	private void moveToPosition ()
	{
		// Set the rotation of the model to point in the direction of the target
		Quaternion newRotation = Quaternion.LookRotation (targetPosition - transform.position);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);

		// Tell the controller to move in the straight-line direction
		// between the current position and the waypoint
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir = dir * GetMovementSpeed();
		characterController.SimpleMove (dir);

		// Have we reached the waypoint in the path?
		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			// Advance to the next waypoint
			currentWaypoint++;

			// If we have reached the last waypoint in the path,
			// then we need to stop the walking animation
			if (path.vectorPath.Count == currentWaypoint)
            {
                CancelPath();
			}
		}
	}

	// Calcula el characterController de la unitat
	private void FittedCharacterCollider ()
	{
		Transform transform = this.gameObject.transform;
		Quaternion rotation = transform.rotation;
		transform.rotation = Quaternion.identity;
		
		characterController = transform.GetComponent<CharacterController> ();
		
		if (characterController == null) {
			transform.gameObject.AddComponent<CharacterController> ();
			characterController = transform.GetComponent<CharacterController> ();
		}
		
		Bounds bounds = new Bounds (transform.position, Vector3.zero);
		
		ExtendBounds (transform, ref bounds);
		
		characterController.center = new Vector3((bounds.center.x - transform.position.x) / transform.localScale.x, (bounds.center.y - transform.position.y + 0.1f) / transform.localScale.y, (bounds.center.z - transform.position.z) / transform.localScale.z);
		characterController.radius = bounds.size.x / 2;
		characterController.height = bounds.size.y / transform.localScale.y;
		
		transform.rotation = rotation;
	}
}
